using System;
using System.IO;
using System.Web.Script.Serialization;

namespace Reminder
{
    public class ConfigManager
    {
        private static readonly string ConfigDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SitFit"
        );
        private static readonly string ConfigPath = Path.Combine(ConfigDir, "config.json");
        private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        public class Config
        {
            public string version { get; set; }
            public Settings settings { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Settings
        {
            public int workMinutes { get; set; }
            public int restMinutes { get; set; }
            public bool inputBlockingEnabled { get; set; }
        }

        public class Statistics
        {
            public int totalSessions { get; set; }
            public int totalStandups { get; set; }
            public int totalWorkMinutes { get; set; }
        }

        public static Config LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string json = File.ReadAllText(ConfigPath);
                    var config = serializer.Deserialize<Config>(json);
                    if (config != null) return config;
                }
            }
            catch
            {
                // Fall through to default
            }
            return new Config { version = "1.0", settings = new Settings { workMinutes = 20, restMinutes = 3, inputBlockingEnabled = false }, statistics = new Statistics { totalSessions = 0, totalStandups = 0, totalWorkMinutes = 0 } };
        }

        public static void SaveConfig(Config config)
        {
            try
            {
                if (!Directory.Exists(ConfigDir))
                    Directory.CreateDirectory(ConfigDir);
                string json = serializer.Serialize(config);
                File.WriteAllText(ConfigPath, json);
            }
            catch
            {
                // Silently fail - non-critical
            }
        }

        public static void UpdateSettings(int workMinutes, int restMinutes, bool inputBlockingEnabled)
        {
            var config = LoadConfig();
            config.settings.workMinutes = workMinutes;
            config.settings.restMinutes = restMinutes;
            config.settings.inputBlockingEnabled = inputBlockingEnabled;
            SaveConfig(config);
        }

        public static void IncrementStatistics(int workMinutesCompleted)
        {
            var config = LoadConfig();
            config.statistics.totalSessions++;
            config.statistics.totalStandups++;
            config.statistics.totalWorkMinutes += workMinutesCompleted;
            SaveConfig(config);
        }
    }
}
