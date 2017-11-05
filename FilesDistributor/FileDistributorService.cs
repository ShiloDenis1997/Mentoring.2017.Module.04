using System;
using System.Threading;
using System.Threading.Tasks;
using FilesDistributor.Abstract;
using FilesDistributor.Models;
using FileSystemMonitorConfig;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;

namespace FilesDistributor
{
    public class FileDistributorService
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            Console.CancelKeyPress += (o, e) => { source.Cancel(); };

            FileSystemMonitorConfigSection config =
                (FileSystemMonitorConfigSection)ConfigurationManager.GetSection("fileSystemSection");

            List<string> directories = new List<string>(config.Directories.Count);
            List<Rule> rules = new List<Rule>();

            foreach (DirectoryElement directory in config.Directories)
            {
                directories.Add(directory.Path);
            }

            foreach (RuleElement rule in config.Rules)
            {
                rules.Add(new Rule
                {
                    FilePattern = rule.FilePattern,
                    DestinationFolder = rule.DestinationFolder,
                    IsDateAppended = rule.IsDateAppended,
                    IsOrderAppended = rule.IsOrderAppended
                });
            }

            CultureInfo.DefaultThreadCurrentCulture = config.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = config.Culture;
            CultureInfo.CurrentUICulture = config.Culture;
            CultureInfo.CurrentCulture = config.Culture;

            Console.WriteLine(config.Culture.DisplayName);

            ILogger logger = new Logger();
            IDistributor<FileModel> distributor =
                new FilesDistributor(rules, config.Rules.DefaultDirectory, logger);
            ILocationsWatcher<FileModel> watcher =
                new FilesWatcher(directories, logger);
            LocationsManager<FileModel> locationsManager = 
                new LocationsManager<FileModel>(watcher, distributor);

            await Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token);
        }
    }
}
