using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesDistributor.Abstract;
using FilesDistributor.EventArgs;

namespace FilesDistributor
{
    public class LocationsManager<TModel>
    {
        private readonly ILocationsWatcher<TModel> _locationsWatcher;
        private readonly IDistributor<TModel> _distributor;

        public LocationsManager(ILocationsWatcher<TModel> locationsWatcher, IDistributor<TModel> distributor)
        {
            _locationsWatcher = locationsWatcher;
            _distributor = distributor;

            _locationsWatcher.Created += OnCreatedEventHandler;
        }

        private async void OnCreatedEventHandler(object sender, CreatedEventArgs<TModel> args)
        {
            await _distributor.Move(args.CreatedItem);
        }
    }
}
