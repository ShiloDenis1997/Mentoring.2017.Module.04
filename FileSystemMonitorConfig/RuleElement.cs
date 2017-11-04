using System.Configuration;

namespace FileSystemMonitorConfig
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("fileTemplate", IsRequired = true, IsKey = true)]
        public string FileTemplate => (string)this["fileTemplate"];

        [ConfigurationProperty("destFolder", IsRequired = true)]
        public string DestinationFolder => (string)this["destFolder"];

        [ConfigurationProperty("isOrderAppended", IsRequired = false, DefaultValue = false)]
        public bool IsOrderAppended => (bool)this["isOrderAppended"];

        [ConfigurationProperty("isDateAppended", IsRequired = false, DefaultValue = false)]
        public bool IsDateAppended => (bool)this["isDateAppended"];
    }
}
