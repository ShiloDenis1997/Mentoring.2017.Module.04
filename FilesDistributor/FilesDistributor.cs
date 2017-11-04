using System;
using System.IO;
using System.Threading.Tasks;
using FilesDistributor.Abstract;
using FilesDistributor.Models;

namespace FilesDistributor
{
    public class FilesDistributor : IDistributor<FileModel>
    {
        private readonly ILogger _logger;

        public FilesDistributor(ILogger logger = null)
        {
            _logger = logger;
        }

        public async Task Move(FileModel item)
        {
            var dirInfo = Directory.CreateDirectory(Path.Combine(@"E:\mentoringD1D2\solutions", "newFiles"));
            try
            {
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    File.Move(item.FullName, Path.Combine(dirInfo.FullName, item.Name));
                });
            }
            catch (IOException ioex)
            {
                _logger.Log($"Cannot move file {item.Name} now. Error: {ioex.Message}");
            }
        }
    }
}
