namespace OnTime_Demo.IServices
{
    public interface IJiraHttpClient
    {
        Task<HttpResponseMessage> getAsync(string url, Dictionary<object, object> parameters, string authToken);
    }
}
