namespace OnTime_Demo.IServices
{
    public interface IJiraHttpClient
    {
        Task<HttpResponseMessage> getAsync(string url, string authToken);
        Task<HttpResponseMessage> postAsync(string url, Dictionary<object, object> requestBody, string authToken);
    }
}
