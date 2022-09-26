namespace OnTime_Demo.Entities
{
    public class ConfigurationInfoForZohoRefreshToken
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }

    public class JiraRefreshToken
    {
        public string refresh_token { get; set; }      
    }
}
