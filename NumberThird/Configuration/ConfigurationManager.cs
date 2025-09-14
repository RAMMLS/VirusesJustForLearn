using Microsoft.Extensions.Configuration;
using EthicalExploit.Logging;

namespace EthicalExploit.Configuration
{
    public class ConfigurationManager
    {
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _configurationRoot;

        public ConfigurationManager(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuration/AppSettings.json", optional: false, reloadOnChange: true);

            _configurationRoot = builder.Build();
        }

        public AppSettings AppSettings
        {
            get
            {
                try
                {
                    return _configurationRoot.GetSection("").Get<AppSettings>() ?? new AppSettings(); // Provide a default
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error loading configuration: {ex.Message}");
                    return new AppSettings(); // Return default on error
                }

            }
        }
    }

    public class AppSettings
    {
        public string Message { get; set; } = "Default Message";
        public string InputBlockerType { get; set; } = "None";
        public string ExitConditionType { get; set; } = "None";
        public string Password { get; set; } = "defaultpassword";
        public string KeyCombination { get; set; } = "Ctrl+Alt+Q";

    }
}

