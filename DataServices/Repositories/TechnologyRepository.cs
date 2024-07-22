using DataServices.Data;
using DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyAPI.Services;

namespace DataServices.Repositories
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly DataBaseContext _context;

        public TechnologyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technologies>> GetAllTechnologies()
        {
            return await _context.TblTechnology.ToListAsync();
        }

        public async Task<Technologies> GetTechnologyById(string id)
        {
            return await _context.TblTechnology.FindAsync(id);
        }

        public async Task<Technologies> AddTechnology(Technologies technology)
        {
            _context.TblTechnology.Add(technology);
            await _context.SaveChangesAsync();
            return technology;
        }

        public async Task<Technologies> UpdateTechnology(Technologies technology)
        {
            _context.Entry(technology).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return technology;
        }

        public async Task<bool> DeleteTechnology(string id)
        {
            var technology = await _context.TblTechnology.FindAsync(id);
            if (technology == null)
            {
                return false;
            }

            _context.TblTechnology.Remove(technology);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
