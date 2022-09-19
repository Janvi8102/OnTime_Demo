namespace OnTime_Demo.Entities
{
    public class JiraCommonInput
    {
        public string AuthToken { get; set; }


        public JiraCommonInput(string authToken)
        {
            AuthToken = authToken;
        }  
    }
}
