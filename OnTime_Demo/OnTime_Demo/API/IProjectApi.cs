using OnTime_Demo.Entities;
using OnTime_Demo.Models;

namespace OnTime_Demo.API
{
    public interface IProjectApi
    {
        Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput);
        Task<CommentOutput> AddComment(string IssueKey, CommentInput content, JiraCommonInput jiraCommonInput);
        Task<IssueOutput> GetIssue(string IssueKey, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput);
        Task<AllIssue> GetAllIssue(string ProjectKey, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput);
        Task<WorkLogOutput> AddWorkLog(string IssueKey, WorkLogInput content, JiraCommonInput jiraCommonInput);
        Task<MyIssueOutput> GetMyIssue(string AccountId, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput);
        Task<MyIssueOutput> GetMyProjectIssue(string ProjectKey, string AccountId, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput);
    }
}
