using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface ITypeRepository
    {
        public List<TypeVM> GetAll();
        TypeVM GetById(int id);
        TypeVM Add(TypeVM type);
        void Update(TypeVM type);
        void Delete(int id);
    }
}
