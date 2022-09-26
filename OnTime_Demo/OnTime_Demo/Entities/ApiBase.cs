namespace OnTime_Demo.Entities
{
    public abstract class ApiBase
    {
        public static string baseurl = "https://api.atlassian.com/ex/jira/c69523f7-cd4d-40d6-bd88-a2b386222c45";

        public string getBaseUrl()
        {
            return baseurl;
        }
    }
}
