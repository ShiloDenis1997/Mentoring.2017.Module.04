using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesDistributor.Models;

namespace FilesDistributor.EventArgs
{
    public class CreatedEventArgs<TModel> : System.EventArgs
    {
        public TModel CreatedItem { get; set; }
    }
}
