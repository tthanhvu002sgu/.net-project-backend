using DoAn_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Services
{
    public interface ISpecializationRepository
    {
        public Task<IActionResult> AddSpecialization(SpecializationVM specialization);
        public Task<List<SpecializationVM>> GetAllSpecializationsAsync();
        public Task<SpecializationVM> GetSpecializationByNameAsync(string specializationName);
        public bool DeleteSpecialization(string specializationName);
    }
}
