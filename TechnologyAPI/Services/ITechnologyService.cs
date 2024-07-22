using DataServices.Models;

namespace TechnologyAPI.Services
{
    public interface ITechnologyService
    {
        Task<IEnumerable<Technologies>> GetAllTechnologies();
        Task<Technologies> GetTechnologyById(string id);
        Task<Technologies> AddTechnology(Technologies technology);
        Task<Technologies> UpdateTechnology(Technologies technology);
        Task<bool> DeleteTechnology(string id);
    }
}