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
        private readonly IProjectService _project;
        private readonly IUserServices _userServices;

        public ProjectController(IProjectService project, IUserServices userServices)
        {
            _project = project;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("GetAllProject")]
        public async Task<IActionResult> MyProject([FromHeader] string UserId)
         {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            List<ProjectOutput> result = new List<ProjectOutput>();
            result = await _project.MyProject(jiramodel);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment([FromHeader] string UserId, [FromQuery] string IssueKey, [FromBody] CommentInput content)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            var response = new CommentOutput();
            response = await _project.AddComment(IssueKey, content, jiramodel);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetIssue")]
        public async Task<IActionResult> GetIssue([FromHeader] string UserId, [FromQuery] string IssueKey)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            IssueOutput result = new IssueOutput();
            result = await _project.GetIsssue(IssueKey,jiramodel);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProjectIssues")]
        public async Task<IActionResult> GetAllIssue([FromHeader] string UserId, [FromQuery] string ProjectKey)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            AllIssue result = new AllIssue();
            result = await _project.GetAllIsssue(ProjectKey, jiramodel);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetMyIssue")]
        public async Task<IActionResult> GetMyIssue([FromHeader] string UserId, [FromQuery] string AccountId)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            MyIssueOutput result = new MyIssueOutput();
            result = await _project.GetMyIsssue(AccountId,jiramodel);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetMyProjectIssue")]
        public async Task<IActionResult> GetMyProjectIssue([FromHeader] string UserId, [FromQuery] string ProjectKey, [FromQuery] string AccountId)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            MyIssueOutput result = new MyIssueOutput();
            result = await _project.GetMyProjectIsssue(ProjectKey,AccountId,jiramodel);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddWorkLog")]
        public async Task<IActionResult> AddWorkLog([FromHeader] string UserId, [FromBody] WorkLogInput content, [FromQuery] string IssueKey)
        {
            JiraTokenModel jiramodel = _userServices.GetJiraTokens(Convert.ToInt32(UserId));
            _project.setAuthorizationToken(jiramodel.JiraAuthToken);
            var response = new WorkLogOutput();
            response = await _project.AddWorkLog(IssueKey,content, jiramodel);
            return Ok(response);
        }
    }
}
