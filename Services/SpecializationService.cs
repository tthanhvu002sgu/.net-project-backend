using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAn_API.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly MyDbContext _context;

        public SpecializationService(MyDbContext context)
        {
            _context = context;
        }

        public List<SpecializationVM> GetAll()
        {
            return _context.Specializations
                .Select(s => new SpecializationVM
                {
                    specializationId = s.specializationId.ToString(),
                    specialization = s.specialization,
                    // Map other necessary properties if needed
                }).ToList();
        }

        public SpecializationVM GetSpecializationVM(int id)
        {
            var specialization = _context.Specializations
                .SingleOrDefault(s => s.specializationId == id);

            if (specialization != null)
            {
                return new SpecializationVM
                {
                    specializationId = specialization.specializationId.ToString(),
                    specialization = specialization.specialization,
                    // Map other necessary properties if needed
                };
            }
            return null; // or handle not found case appropriately
        }

        public SpecializationVM Add(SpecializationVM specializationVM)
        {
            var newSpecialization = new Specialization
            {
                specializationId = int.Parse(specializationVM.specializationId),
                specialization = specializationVM.specialization,
                // Map other necessary properties if needed
            };

            _context.Specializations.Add(newSpecialization);
            _context.SaveChanges();

            return new SpecializationVM
            {
                specializationId = newSpecialization.specializationId.ToString(),
                specialization = newSpecialization.specialization,
                // Map other necessary properties if needed
            };
        }

        public void Update(SpecializationVM specializationVM)
        {
            var existingSpecialization = _context.Specializations
                .SingleOrDefault(s => s.specializationId == int.Parse(specializationVM.specializationId));

            if (existingSpecialization != null)
            {
                existingSpecialization.specialization = specializationVM.specialization;
                // Update other properties as needed
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var specialization = _context.Specializations.SingleOrDefault(s => s.specializationId == id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                _context.SaveChanges();
            }
        }
    }
}
