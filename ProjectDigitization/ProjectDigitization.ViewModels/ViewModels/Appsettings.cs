using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.ViewModels.ViewModels
{
    public class Appsettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public QueueSettings QueueSettings { get; set; }
        public Serilog Serilog { get; set; }
    }
    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }

    }
    public class QueueSettings
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Exchange { get; set; }
        public string Key { get; set; }
        public string Queue { get; set; }
        public int Port { get; set; }
        public int PreFetchCount { get; set; }
        public bool UseSsl { get; set; }
    }
    public class Serilog
    {
        public string[] Using { get; set; }
        public Minimumlevel MinimumLevel { get; set; }
        public Writeto[] WriteTo { get; set; }
        public string[] Enrich { get; set; }
    }
    public class  Minimumlevel
    {
        public string Default { get; set; }
        public Override Override { get; set; }
    }
    public class Override
    {
        public string Microsoft { get; set; }
        public string System { get; set; }
    }
    public class Writeto
    {
        public string Name { get; set; }
        public Args Args { get; set; }
    }
    public class Args
    {
        public string Path { get; set; }
        public string rollingInterval { get; set; }
        public bool rollOnFileSizeLimit { get; set; }
        public string fileSizeLimitBytes { get; set; }
        public bool shared { get; set; }

    }
}
