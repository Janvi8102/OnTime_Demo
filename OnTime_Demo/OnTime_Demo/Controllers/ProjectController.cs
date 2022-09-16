using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTime_Demo.Entities;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;
using System.Data;

namespace OnTime_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly IProjectApi _api; 

        public ProjectController(IDbConnection connection, IProjectApi api)
        {
            _connection = connection;
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string query = @"select * FROM ""User""";
            var taskTimeSheetRecords = await _connection.QueryAsync<JiraTokenModel>(query);
            return Ok(taskTimeSheetRecords);
        }

        [HttpGet]
        [Route("MyTasks")]
        public async Task<IActionResult> GetProject()
        {
            var result = new List<ProjectOutput>();
            result =  await _api.RecentProject();
            return Ok(result);
        }
    }
}
