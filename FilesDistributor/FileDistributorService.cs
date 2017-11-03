using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FilesDistributor.Abstract;
using FilesDistributor.Models;

namespace FilesDistributor
{
    public class FileDistributorService
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            Console.CancelKeyPress += (o, e) => { source.Cancel(); };
            string currentLocation =
                @"C:\Users\Dzianis_Shyla\source\repos\Module4\FilesDistributor\bin\Debug";

            ILogger logger = new Logger();
            IDistributor<FileModel>  distributor = new FilesDistributor(logger);
            ILocationsWatcher<FileModel> watcher = new FilesWatcher(new []{currentLocation, @"C:\Users\Dzianis_Shyla\source\repos\Module4\FilesDistributor\bin" }, logger);
            LocationsManager<FileModel> locationsManager = new LocationsManager<FileModel>(watcher, distributor);

            await Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token);
        }
    }
}
