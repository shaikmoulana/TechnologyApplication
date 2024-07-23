using DataServices.Models;

namespace TechnologyAPI.Services
{
    public class TechnologyService:ITechnologyService
    {
        private readonly ITechnologyRepository _repository;

        public TechnologyService(ITechnologyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Technologies>> GetAllTechnologies()
        {
            return await _repository.GetAllTechnologies();
        }

        public async Task<Technologies> GetTechnologyById(string id)
        {
            return await _repository.GetTechnologyById(id);
        }

        public async Task<Technologies> AddTechnology(Technologies technology)
        {
            /*technology.DepartmentId=technology.DepartmentId;*/
            technology.Name = technology.Name;
            technology.CreatedDate = DateTime.Now;
            technology.IsActive = true;
            technology.CreatedBy = "SYSTEM";
            technology.UpdatedDate = technology.UpdatedDate;
            technology.UpdatedBy = technology.UpdatedBy;
            return await _repository.AddTechnology(technology);
        }

        public async Task<Technologies> UpdateTechnology(Technologies technology)
        {
            // Retrieve the existing technology from the database
            var existingTechnology = await _repository.GetTechnologyById(technology.Id);
            if (existingTechnology == null)
            {
                throw new ArgumentException($"Technology with ID {technology.Id} not found.");
            }

            // Update properties with the new values
            existingTechnology.Name = technology.Name;
            existingTechnology.DepartmentId = technology.DepartmentId;
            existingTechnology.IsActive = technology.IsActive;
            existingTechnology.UpdatedBy = technology.UpdatedBy;
            existingTechnology.UpdatedDate = technology.UpdatedDate;

            // Call repository to update the technology
            return await _repository.UpdateTechnology(existingTechnology);
        }


        public async Task<bool> DeleteTechnology(string id)
        {
            // Check if the technology exists
            var existingTechnology = await _repository.GetTechnologyById(id);
            if (existingTechnology == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.DeleteTechnology(id);
        }
    }
}
