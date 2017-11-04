using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileSystemMonitorConfig
{
    public class DirectoryElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)element).Path;
        }
    }
}
