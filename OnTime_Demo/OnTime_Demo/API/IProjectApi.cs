using OnTime_Demo.Entities;
using OnTime_Demo.Models;

namespace OnTime_Demo.API
{
    public interface IProjectApi
    {
        Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput);
    }
}
