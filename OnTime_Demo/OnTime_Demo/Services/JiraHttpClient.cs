using OnTime_Demo.IServices;

namespace OnTime_Demo.Services
{
    public class JiraHttpClient : IJiraHttpClient
    {
        private readonly HttpClient client;
        public JiraHttpClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<HttpResponseMessage> getAsync(string url)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJraWQiOiJmZTM2ZThkMzZjMTA2N2RjYTgyNTg5MmEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI2MmViZGQzMjQzMmVmNDk0YzhjYTY0OGIiLCJuYmYiOjE2NjMzMTg1NDcsImlzcyI6Imh0dHBzOi8vYXRsYXNzaWFuLWFjY291bnQtcHJvZC5wdXMyLmF1dGgwLmNvbS8iLCJpYXQiOjE2NjMzMTg1NDcsImV4cCI6MTY2MzMyMjE0NywiYXVkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJqdGkiOiIxOTdjNjBiMi02ZGM0LTRiM2QtYWI0Yy04M2U0MmU4ZTYyNjQiLCJodHRwczovL2F0bGFzc2lhbi5jb20vdmVyaWZpZWQiOnRydWUsInZlcmlmaWVkIjoidHJ1ZSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS9zZXNzaW9uX2lkIjoiZDY3OTg4ODEtOGQ2MC00ODcxLTlkMzEtNDQxNWVkZWVkOTAwIiwiY2xpZW50X2lkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vcmVmcmVzaF9jaGFpbl9pZCI6IjlGYjBnNUFKR3Jhb21ubU1uS1VPWXRISFJpalJiUTliLTYyZWJkZDMyNDMyZWY0OTRjOGNhNjQ4Yi1jNzI1YTEwNC1lYmNmLTRmODktOGFkNS1mNTdiYzBkYmQ3NGIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vc3lzdGVtQWNjb3VudEVtYWlsIjoiMWMwNmY5NWYtODlkMy00MzJlLTg1OGMtODFlNjI1ZmZkOTYxQGNvbm5lY3QuYXRsYXNzaWFuLmNvbSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS91anQiOiJjMjgxMjIzOS04NDMzLTQyN2ItYTgwMC01NTBiMjJhMmNjOGYiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vdmVyaWZpZWQiOiJ0cnVlIiwiaHR0cHM6Ly9pZC5hdGxhc3NpYW4uY29tL2F0bF90b2tlbl90eXBlIjoiQUNDRVNTIiwic2NvcGUiOiJtYW5hZ2U6amlyYS1jb25maWd1cmF0aW9uIG1hbmFnZTpqaXJhLWRhdGEtcHJvdmlkZXIgbWFuYWdlOmppcmEtcHJvamVjdCBtYW5hZ2U6amlyYS13ZWJob29rIG9mZmxpbmVfYWNjZXNzIHJlYWQ6amlyYS11c2VyIHJlYWQ6amlyYS13b3JrIHdyaXRlOmppcmEtd29yayIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS8zbG8iOnRydWUsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9vYXV0aENsaWVudElkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vZW1haWxEb21haW4iOiJzaW1mb3Jtc29sdXRpb25zLmNvbSIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50RW1haWxEb21haW4iOiJjb25uZWN0LmF0bGFzc2lhbi5jb20iLCJodHRwczovL2F0bGFzc2lhbi5jb20vZmlyc3RQYXJ0eSI6ZmFsc2UsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50SWQiOiI2MzFiMTNhMzY4NTZiZGQ2MGFhMGQ5ZmQifQ.SlVA-8qHEElMeuYHF3tCiOLWMAFxkRdjSnygnVls52C8sMPjKASdrOcVq2HSmk-5HUPhGGFttIPw8Mo6HlR9SIiImJnrLeKd4BkSASEq4QGUFh0iVKHDBUfunVHR5NsNrZtP4VkCqnoecsK0SNyIHv8u5SN_Tvh-MXbXUzFTEf3GzKnqElFIlEmixmQOCN4IO_CBEVBAK40kQCYOkTRvSxjV7Iybr-oGasUhawUZDUjU8oNrGk5U_Z1e6v3KsLz5TBqMe_dbUceep56kYj6aLP1ICMl4HAC3sUVQkeA7wnQmd1WNYjCNqMM6t9deavNxT5VPSCHcZKyKuEM1wsQ_9w");
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
