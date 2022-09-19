namespace OnTime_Demo.Entities
{
    public class JiraAccountModel
    {
        public string access_token { get; set; }
        public int expires_in_sec { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
}
