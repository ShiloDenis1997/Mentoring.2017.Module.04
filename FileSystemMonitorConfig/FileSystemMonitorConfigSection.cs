using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace FileSystemMonitorConfig
{
    public class FileSystemMonitorConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "en-EN", IsRequired = false)]
        public CultureInfo Culture => (CultureInfo)this["culture"];

        [ConfigurationCollection(typeof(DirectoryElement), AddItemName = "directory")]
        [ConfigurationProperty("directories", IsRequired = false)]
        public DirectoryElementCollection Directories => (DirectoryElementCollection)this["directories"];

        [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
        [ConfigurationProperty("rules", IsRequired = true)]
        public RuleElementCollection Rules => (RuleElementCollection)this["rules"];
    }
}
