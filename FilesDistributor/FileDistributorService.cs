using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FilesDistributor.Abstract;
using FilesDistributor.Models;
using FileSystemMonitorConfig;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

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

            foreach (DirectoryElement directory in config.Directories)
            {
                directories.Add(directory.Path);
            }


            Console.WriteLine(config.Culture.DisplayName);
            try
            {
                ILogger logger = new Logger();
                IDistributor<FileModel> distributor = new FilesDistributor(logger);
                ILocationsWatcher<FileModel> watcher = new FilesWatcher(directories, logger);
                LocationsManager<FileModel> locationsManager = new LocationsManager<FileModel>(watcher, distributor);

                await Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token);
            }
            catch
            {
                Console.WriteLine("Catched");
            }
        }
    }
}
