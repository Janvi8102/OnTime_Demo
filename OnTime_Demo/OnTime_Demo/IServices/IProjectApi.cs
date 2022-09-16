using OnTime_Demo.Models;

namespace OnTime_Demo.IServices
{
    public interface IProjectApi
    {
        Task<List<ProjectOutput>> RecentProject();
    }
}
