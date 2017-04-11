using System.Configuration;

namespace GraphApiSamples
{
    public class ConfigSettings
    {
        private ConfigSettings()
        {
            //Singleton
            TenantId = ConfigurationManager.AppSettings["TenantId"];
            TenantName = ConfigurationManager.AppSettings["TenantName"];
            ClientId = ConfigurationManager.AppSettings["ClientId"];
            ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        }

        public string TenantId { get; set; }
        public string TenantName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        private static ConfigSettings instance;

        public static ConfigSettings Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConfigSettings();
                return instance;
            }
        }
    }
}
