using OnTime_Demo.Entities;
using OnTime_Demo.Models;

namespace OnTime_Demo.IServices
{
    public interface IProjectService
    {
        Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel);
        void setAuthorizationToken(string authToken);
        Task<CommentOutput> AddComment(string IssueKey, CommentInput content, JiraTokenModel jiramodel);
        Task<IssueOutput> GetIsssue(string IssueKey, JiraTokenModel jiramodel);
        Task<AllIssue> GetAllIsssue(string ProjectKey, JiraTokenModel jiramodel);
        Task<WorkLogOutput> AddWorkLog(string IssueId, WorkLogInput content, JiraTokenModel jiramodel);
        Task<MyIssueOutput> GetMyIsssue(string AccountId, JiraTokenModel jiramodel);
        Task<MyIssueOutput> GetMyProjectIsssue(string ProjectKey, string AccountId, JiraTokenModel jiramodel);
    }
}
