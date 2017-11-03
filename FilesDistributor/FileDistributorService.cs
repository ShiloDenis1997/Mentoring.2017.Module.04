using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilesDistributor
{
    public class FileDistributorService
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            Console.CancelKeyPress += (o, e) => { source.Cancel(); };
            string currentLocation =
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileDistributorService)).Location) ?? throw new ArgumentNullException();
            FileSystemWatcher fileSystemWatcher =
                new FileSystemWatcher(currentLocation
                    )
                {
                    NotifyFilter = NotifyFilters.FileName
                };
            fileSystemWatcher.IncludeSubdirectories = false;
            Console.WriteLine(fileSystemWatcher.Path);
            fileSystemWatcher.Created += (sender, eventArgs) =>
            {
                Console.WriteLine("Created");
                var dirInfo = Directory.CreateDirectory(Path.Combine(currentLocation, "newFiles"));
                try
                {
                    File.Move(eventArgs.Name, Path.Combine(dirInfo.FullName, eventArgs.Name));
                }
                catch (IOException ioex)
                {
                    Console.WriteLine($"Cannot move file {eventArgs.Name} now");
                }
            };
            fileSystemWatcher.EnableRaisingEvents = true;
            await Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token);
        }
    }
}
