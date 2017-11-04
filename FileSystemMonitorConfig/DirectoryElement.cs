﻿using System.Configuration;

namespace FileSystemMonitorConfig
{
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string Path => (string)this["path"];
    }
}
