using Newtonsoft.Json;
using OnTime_Demo.API;
using OnTime_Demo.Entities;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;

namespace OnTime_Demo.Services
{
    public class ProjectService : IProjectService
    {
        JiraCommonInput jiraCommonInput;
        private readonly IProjectApi _projectApi;
        private readonly IUserServices _userServices;
        
        public ProjectService(IProjectApi projectApi, IUserServices userServices)
        {
           _projectApi = projectApi;
            _userServices = userServices;
        }

        public void setAuthorizationToken(string authToken)
        {
            jiraCommonInput = new JiraCommonInput(authToken);
        }
        public async Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel)
        {
           List<ProjectOutput> myProject = await _projectApi.MyProject(jiramodel, jiraCommonInput);
           if(myProject == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                myProject = await _projectApi.MyProject(jiramodel, jiraCommonInput);
            }
            return myProject;
        }

        public async Task<CommentOutput> AddComment(string IssueKey, CommentInput content, JiraTokenModel jiramodel)
        {
            var response = new CommentOutput();
            CommentOutput addComment = await _projectApi.AddComment(IssueKey, content, jiraCommonInput);
            if (addComment == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                addComment = await _projectApi.AddComment(IssueKey,content, jiraCommonInput);
            }
            return addComment;
        }

        public async Task<IssueOutput> GetIsssue(string IssueKey, JiraTokenModel jiramodel)
        {
            IssueOutput myProject = await _projectApi.GetIssue( IssueKey,jiramodel, jiraCommonInput);
            if (myProject == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                myProject = await _projectApi.GetIssue(IssueKey, jiramodel, jiraCommonInput);
            }
            return myProject;
        }

        public async Task<AllIssue> GetAllIsssue(string ProjectKey, JiraTokenModel jiramodel)
        {
            AllIssue myProject = await _projectApi.GetAllIssue(ProjectKey, jiramodel, jiraCommonInput);
            if (myProject == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                myProject = await _projectApi.GetAllIssue(ProjectKey, jiramodel, jiraCommonInput);
            }
            return myProject;
        }

        public async Task<MyIssueOutput> GetMyIsssue(string AccountId, JiraTokenModel jiramodel)
        {
            MyIssueOutput myProject = await _projectApi.GetMyIssue(AccountId, jiramodel, jiraCommonInput);
            if (myProject == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                myProject = await _projectApi.GetMyIssue(AccountId, jiramodel, jiraCommonInput);
            }
            return myProject;
        }

        public async Task<MyIssueOutput> GetMyProjectIsssue(string ProjectKey, string AccountId, JiraTokenModel jiramodel)
        {
            MyIssueOutput myProject = await _projectApi.GetMyProjectIssue(ProjectKey, AccountId, jiramodel, jiraCommonInput);
            if (myProject == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                myProject = await _projectApi.GetMyProjectIssue(ProjectKey, AccountId, jiramodel, jiraCommonInput);
            }
            return myProject;
        }

        public async Task<WorkLogOutput> AddWorkLog(string IssueKey, WorkLogInput content, JiraTokenModel jiramodel)
        {
            var response = new WorkLogOutput();
            WorkLogOutput addComment = await _projectApi.AddWorkLog(IssueKey, content, jiraCommonInput);
            if (addComment == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                addComment = await _projectApi.AddWorkLog(IssueKey, content, jiraCommonInput);
            }
            return addComment;
        }
    }
}
