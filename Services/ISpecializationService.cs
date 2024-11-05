using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface ISpecializationService
    {
        List<SpecializationVM> GetAll(); 
        SpecializationVM GetSpecializationVM(int id);
        SpecializationVM Add(SpecializationVM specialization); 
        void Update(SpecializationVM specialization); 
        void Delete(int id); 
    }
}

