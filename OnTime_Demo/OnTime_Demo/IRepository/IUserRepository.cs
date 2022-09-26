using OnTime_Demo.Entities;

namespace OnTime_Demo.IRepository
{
    public interface IUserRepository
    {
        List<ConfigurationInfoModel> GetConfigurationInfo();
        JiraTokenModel GetJiraToken(int userId);
        Task<bool> UpdateJiraTokens(JiraTokenModel jiramodel);
        JiraRefreshToken GetJiraRefreshToken();
    }
}
