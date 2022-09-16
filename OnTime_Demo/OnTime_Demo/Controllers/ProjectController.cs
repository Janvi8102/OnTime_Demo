using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTime_Demo.Entities;
using OnTime_Demo.Models;
using System.Data;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using OnTime_Demo.IServices;

namespace OnTime_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly IProject _project;

        public ProjectController(IDbConnection connection, IProject project)
        {
            _connection = connection;
            _project = project;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string query = @"select * FROM ""User""";
            var taskTimeSheetRecords = await _connection.QueryAsync<JiraTokenModel>(query);
            return Ok(taskTimeSheetRecords);
        }

        [HttpGet]
        [Route("GetProject")]
        public async Task<IActionResult> GetProject()
        {
            List<ProjectOutput> result = new List<ProjectOutput>();
            result = await _project.GetProjects();
            return Ok(result);
        }


        [HttpGet]
        [Route("MyProject")]
        public async Task<IActionResult> MyProject()
        {
            List<ProjectOutput> result = new List<ProjectOutput>();
            result = await _project.MyProject();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetMyProject")]
        public IActionResult GetMyProject()
        {
            
            string apiUrl = "https://api.atlassian.com/ex/jira/c69523f7-cd4d-40d6-bd88-a2b386222c45/rest/api/3/project";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJraWQiOiJmZTM2ZThkMzZjMTA2N2RjYTgyNTg5MmEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI2MmViZGQzMjQzMmVmNDk0YzhjYTY0OGIiLCJuYmYiOjE2NjMzMDY1MDQsImlzcyI6Imh0dHBzOi8vYXRsYXNzaWFuLWFjY291bnQtcHJvZC5wdXMyLmF1dGgwLmNvbS8iLCJpYXQiOjE2NjMzMDY1MDQsImV4cCI6MTY2MzMxMDEwNCwiYXVkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJqdGkiOiIzZWY5N2E5Yi04ZjE2LTQ1ZDQtODQ4OS04MWU0YTQyYWU5MjUiLCJodHRwczovL2F0bGFzc2lhbi5jb20vdmVyaWZpZWQiOnRydWUsInZlcmlmaWVkIjoidHJ1ZSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS9zZXNzaW9uX2lkIjoiZDY3OTg4ODEtOGQ2MC00ODcxLTlkMzEtNDQxNWVkZWVkOTAwIiwiY2xpZW50X2lkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vcmVmcmVzaF9jaGFpbl9pZCI6IjlGYjBnNUFKR3Jhb21ubU1uS1VPWXRISFJpalJiUTliLTYyZWJkZDMyNDMyZWY0OTRjOGNhNjQ4Yi1jNzI1YTEwNC1lYmNmLTRmODktOGFkNS1mNTdiYzBkYmQ3NGIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vc3lzdGVtQWNjb3VudEVtYWlsIjoiMWMwNmY5NWYtODlkMy00MzJlLTg1OGMtODFlNjI1ZmZkOTYxQGNvbm5lY3QuYXRsYXNzaWFuLmNvbSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS91anQiOiJjMjgxMjIzOS04NDMzLTQyN2ItYTgwMC01NTBiMjJhMmNjOGYiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vdmVyaWZpZWQiOiJ0cnVlIiwiaHR0cHM6Ly9pZC5hdGxhc3NpYW4uY29tL2F0bF90b2tlbl90eXBlIjoiQUNDRVNTIiwic2NvcGUiOiJtYW5hZ2U6amlyYS1jb25maWd1cmF0aW9uIG1hbmFnZTpqaXJhLWRhdGEtcHJvdmlkZXIgbWFuYWdlOmppcmEtcHJvamVjdCBtYW5hZ2U6amlyYS13ZWJob29rIG9mZmxpbmVfYWNjZXNzIHJlYWQ6amlyYS11c2VyIHJlYWQ6amlyYS13b3JrIHdyaXRlOmppcmEtd29yayIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS8zbG8iOnRydWUsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9vYXV0aENsaWVudElkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vZW1haWxEb21haW4iOiJzaW1mb3Jtc29sdXRpb25zLmNvbSIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50RW1haWxEb21haW4iOiJjb25uZWN0LmF0bGFzc2lhbi5jb20iLCJodHRwczovL2F0bGFzc2lhbi5jb20vZmlyc3RQYXJ0eSI6ZmFsc2UsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50SWQiOiI2MzFiMTNhMzY4NTZiZGQ2MGFhMGQ5ZmQifQ.ZYtwPKrNmlcS4-Jyb8g5dJLe_D0iU4rEG0iDjy6Nz1d56uDDrolPcDiDaYDe5MvFpQN6g-zfVrMBXeg5ao0ci8kNgSMFz2F536N7Phcv4oQQaXKDy87gBeufCJFwHTTYosM6OjLaYYVF0RnpSj2tp2e6EJGGADWnSHdU4B90seRmPVaCrbEt2sdKPcObH4rxiR8KiaM0iLg8HUysD2-WebdcxeeUtu5N32uYVHAvJUQI-xqZ-de0ZUtUXdd8Gcp0EoEqGL1xZUhOivkqnLADD_973_cXt9gZVeeN4Mq2S0uP2PPtx4qDU_yngjCBCkryfRsF0R1kZowDp-yMQQvzNQ");
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var customers = JsonConvert.DeserializeObject<List<ProjectOutput>>(response.Content.ReadAsStringAsync().Result);
                return Ok(customers);
            }
            else
                return BadRequest(response.Content.ReadAsStringAsync().Result);
            

        }
    }
}
