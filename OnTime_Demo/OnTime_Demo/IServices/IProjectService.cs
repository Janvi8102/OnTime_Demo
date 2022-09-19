using OnTime_Demo.Entities;
using OnTime_Demo.Models;

namespace OnTime_Demo.IServices
{
    public interface IProjectService
    {
        Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel);
        void setAuthorizationToken(string authToken);
    }
}
