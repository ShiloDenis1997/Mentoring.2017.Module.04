using System;
using System.IO;
using System.Threading.Tasks;
using FilesDistributor.Abstract;
using FilesDistributor.Models;
using System.Collections.Generic;
using System.Linq;

using Strings = FilesDistributor.Resources.Strings;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FilesDistributor
{
    public class FilesDistributor : IDistributor<FileModel>
    {
        private readonly ILogger _logger;
        private List<Rule> _rules;
        private string _defaultFolder;

        public FilesDistributor(IEnumerable<Rule> rules, string defaultFolder, ILogger logger = null)
        {
            _rules = rules.ToList();
            _logger = logger;
            _defaultFolder = defaultFolder;
        }

        public async Task Move(FileModel item)
        {
            try
            {
                await Task.Delay(100);
                await Task.Run(() => MoveFile(item));
            }
            catch (IOException ioex)
            {
                _logger?.Log($"Cannot move file {item.Name} now. Error: {ioex.Message}");
            }
        }

        private void MoveFile(FileModel item)
        {
            string from = item.FullName;
            foreach (Rule rule in _rules)
            {
                var match = Regex.Match(item.Name, rule.FilePattern);

                if (match.Success && match.Length == item.Name.Length)
                {
                    rule.MatchesCount++;
                    string to = FormDestinationPath(item, rule);
                    _logger?.Log(Strings.RuleMatch);
                    Directory.CreateDirectory(rule.DestinationFolder);
                    File.Move(from, to);
                    _logger?.Log(string.Format(Strings.FileMovedTemplate, item.FullName, to));
                    return;
                }
            }

            string destination = Path.Combine(_defaultFolder, item.Name);
            _logger?.Log(Strings.RuleNoMatch);
            Directory.CreateDirectory(_defaultFolder);
            File.Move(from, destination);
            _logger?.Log(string.Format(Strings.FileMovedTemplate, item.FullName, destination));
        }

        private string FormDestinationPath(FileModel file, Rule rule)
        {
            string extension = Path.GetExtension(file.Name);
            string filename = Path.GetFileNameWithoutExtension(file.Name);
            string destination = Path.Combine(rule.DestinationFolder, filename);
            if (rule.IsDateAppended)
            {
                var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                dateTimeFormat.DateSeparator = ".";
                destination = $"{destination}_" +
                    $"{DateTime.Now.ToLocalTime().ToString(dateTimeFormat.ShortDatePattern)}";
            }

            if (rule.IsOrderAppended)
            {
                destination += $"_{rule.MatchesCount}";
            }

            destination += extension;
            return destination;
        }
    }
}
