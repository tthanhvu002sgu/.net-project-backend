using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn_API.Services
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly MyDbContext _context;
        public SpecializationRepository(MyDbContext context)
        {
            _context = context;
        }

        public Task<IActionResult> AddSpecialization(SpecializationVM specialization)
        {
            var newSpecialization = new Specialization { specialization = specialization.specialization };
            _context.Specializations.Add(newSpecialization);
            _context.SaveChanges();
            return Task.FromResult<IActionResult>(new OkResult());
        }

        public bool DeleteSpecialization(string specializationName)
        {
            var specialization = _context.Specializations.FirstOrDefault(s => s.specialization == specializationName);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<List<SpecializationVM>> GetAllSpecializationsAsync()
        {
            var specializations = await _context.Specializations.Select(s => new SpecializationVM { specialization = s.specialization }).ToListAsync();
            return specializations;
        }

        public async Task<SpecializationVM> GetSpecializationByNameAsync(string specializationName)
        {
            var specialization = _context.Specializations.Where(s => s.specialization == specializationName).Select(s => new SpecializationVM { specialization = s.specialization }).FirstOrDefault();
            return specialization;
        }


    }
}
