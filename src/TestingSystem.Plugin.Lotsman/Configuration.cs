using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Plugin.Lotsman
{
    public class PluginConfiguration
    {
        public string DatabaseConnectionString { get; set; } = "Host=localhost;Database=postgres ;Username=postgres;Password=postgres";
        public bool AutoCreateTables { get; set; } = true;
        public string LotsmanApiUrl { get; set; } = "http://localhost:8080/api";
        public string LotsmanApiKey { get; set; } = "api-key-here";

        // Настройки синхронизации
        public bool SyncUsersOnStartup { get; set; } = true;
        public int SyncIntervalMinutes { get; set; } = 60;
    }
}
