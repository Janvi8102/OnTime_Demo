using OnTime_Demo.Models;

namespace OnTime_Demo.IServices
{
    public interface IProject
    {
        Task<List<ProjectOutput>> GetProjects();
        Task<List<ProjectOutput>> MyProject();
    }
}
