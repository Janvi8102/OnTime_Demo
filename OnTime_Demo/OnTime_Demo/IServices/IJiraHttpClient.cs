namespace OnTime_Demo.IServices
{
    public interface IJiraHttpClient
    {
        Task<HttpResponseMessage> getAsync(string url);
    }
}
