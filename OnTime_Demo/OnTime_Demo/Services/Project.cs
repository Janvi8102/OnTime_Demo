using Newtonsoft.Json;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;

namespace OnTime_Demo.Services
{
    public class Project : IProject
    {
        private readonly HttpClient _client;
        private readonly IJiraHttpClient _jiraclient;
        public Project(HttpClient client, IJiraHttpClient jiraclient)
        {
            _client = client;
            _jiraclient = jiraclient;
        }

        public async Task<List<ProjectOutput>> GetProjects()
        {
            var result = new List<ProjectOutput>();
            string url = "https://api.atlassian.com/ex/jira/c69523f7-cd4d-40d6-bd88-a2b386222c45/rest/api/3/project";
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJraWQiOiJmZTM2ZThkMzZjMTA2N2RjYTgyNTg5MmEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI2MmViZGQzMjQzMmVmNDk0YzhjYTY0OGIiLCJuYmYiOjE2NjMzMTIxNzAsImlzcyI6Imh0dHBzOi8vYXRsYXNzaWFuLWFjY291bnQtcHJvZC5wdXMyLmF1dGgwLmNvbS8iLCJpYXQiOjE2NjMzMTIxNzAsImV4cCI6MTY2MzMxNTc3MCwiYXVkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJqdGkiOiIxMDUxZTY4ZC1kZjYyLTQxNjctYWExZS1mMmRiZWIwMTY4NmIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vdmVyaWZpZWQiOnRydWUsInZlcmlmaWVkIjoidHJ1ZSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS9zZXNzaW9uX2lkIjoiZDY3OTg4ODEtOGQ2MC00ODcxLTlkMzEtNDQxNWVkZWVkOTAwIiwiY2xpZW50X2lkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vcmVmcmVzaF9jaGFpbl9pZCI6IjlGYjBnNUFKR3Jhb21ubU1uS1VPWXRISFJpalJiUTliLTYyZWJkZDMyNDMyZWY0OTRjOGNhNjQ4Yi1jNzI1YTEwNC1lYmNmLTRmODktOGFkNS1mNTdiYzBkYmQ3NGIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vc3lzdGVtQWNjb3VudEVtYWlsIjoiMWMwNmY5NWYtODlkMy00MzJlLTg1OGMtODFlNjI1ZmZkOTYxQGNvbm5lY3QuYXRsYXNzaWFuLmNvbSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS91anQiOiJjMjgxMjIzOS04NDMzLTQyN2ItYTgwMC01NTBiMjJhMmNjOGYiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vdmVyaWZpZWQiOiJ0cnVlIiwiaHR0cHM6Ly9pZC5hdGxhc3NpYW4uY29tL2F0bF90b2tlbl90eXBlIjoiQUNDRVNTIiwic2NvcGUiOiJtYW5hZ2U6amlyYS1jb25maWd1cmF0aW9uIG1hbmFnZTpqaXJhLWRhdGEtcHJvdmlkZXIgbWFuYWdlOmppcmEtcHJvamVjdCBtYW5hZ2U6amlyYS13ZWJob29rIG9mZmxpbmVfYWNjZXNzIHJlYWQ6amlyYS11c2VyIHJlYWQ6amlyYS13b3JrIHdyaXRlOmppcmEtd29yayIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS8zbG8iOnRydWUsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9vYXV0aENsaWVudElkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vZW1haWxEb21haW4iOiJzaW1mb3Jtc29sdXRpb25zLmNvbSIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50RW1haWxEb21haW4iOiJjb25uZWN0LmF0bGFzc2lhbi5jb20iLCJodHRwczovL2F0bGFzc2lhbi5jb20vZmlyc3RQYXJ0eSI6ZmFsc2UsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50SWQiOiI2MzFiMTNhMzY4NTZiZGQ2MGFhMGQ5ZmQifQ.cMBKxg6NuM3IzE6pKGiLLR3VXCYQqUfKP1ixi09x3IrzJ5txduIUZkYUzdB7shTgnQ6490ideyhLarMIun03wZbk2TateJMbASsWXTVOZAwZooZuXJEZuSWyx4Zn3bkIxGN36THASpmlL2jDngHlMSlj50f-5P0_7JdAZBws5ecQZW4efp9-MCi9MkEdbyKlgGd0OVabVvqPLkhdArhV9RE9K2OdrlMUONndEZqH7kcn5sql35BCqp63_k2bnBY7q7SGPUZmEuXgWd2TZ6XzwrsbnrV3K1bxWwXNx76jUR-ELczlGAVp8VmEWuxZ9snrCXbzsD6-p0_dCtDIB2Fi8w");

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
               result = JsonConvert.DeserializeObject<List<ProjectOutput>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        public async Task<List<ProjectOutput>> MyProject()
        {
            var data = new List<ProjectOutput>();
            string url = "https://api.atlassian.com/ex/jira/c69523f7-cd4d-40d6-bd88-a2b386222c45/rest/api/3/project";
            var result = await _jiraclient.getAsync(url);
            if (result.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<List<ProjectOutput>>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return data;
        }
    }
}
