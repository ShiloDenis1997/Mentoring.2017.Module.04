using System.Configuration;

namespace FileSystemMonitorConfig
{
    public class RuleElementCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDir", IsRequired = true)]
        public string DefaultDirectory => (string)this["defaultDir"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).FilePattern;
        }
    }
}
