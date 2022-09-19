using OnTime_Demo.Entities;

namespace OnTime_Demo.IServices
{
    public interface IUserServices
    {
        JiraTokenModel GetJiraTokens(int userId);
        Task<JiraTokenModel> RefreshingJiraToken(JiraTokenModel jiramodel);
        Task<JiraAccountModel> JiraAccountRefreshToken(string refreshUrl);
    }
}
