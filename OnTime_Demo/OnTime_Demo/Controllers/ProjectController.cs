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
        private readonly IProjectService _project;
        private readonly IUserServices _userServices;

        public ProjectController(IDbConnection connection, IProjectService project, IUserServices userServices)
        {
            _connection = connection;
            _project = project;
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string query = @"select * FROM ""User""";
            var taskTimeSheetRecords = await _connection.QueryAsync<JiraTokenModel>(query);
            return Ok(taskTimeSheetRecords);
        }

        [HttpGet]
        [Route("MyProject")]
        public async Task<IActionResult> MyProject([FromHeader] string UserId)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            List<ProjectOutput> result = new List<ProjectOutput>();
            result = await _project.MyProject(jiramodel);
            return Ok(result);
        }
    }
}
