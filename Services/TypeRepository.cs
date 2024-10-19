//using DoAn_API.Data;
//using DoAn_API.Models;
//using Type = DoAn_API.Data.Type;

//namespace DoAn_API.Services
//{
//    public class TypeRepository : ITypeRepository
//    {
//        private readonly MyDbContext _context;

       
//        public TypeRepository(MyDbContext context)
//        {
//            _context = context;        
//        }
//        public TypeVM Add(TypeVM typeVM)
//        {
//            var type = new Type
//            {
//                NameType = typeVM.NameType
//            };
//            _context.Add(type);
//            _context.SaveChanges();

//            return new TypeVM
//            {
//                IdType = type.IdType,
//                NameType = type.NameType
//            };
//        }

//        public void Delete(int id)
//        {
//            var type = _context.Types.SingleOrDefault(type => type.IdType == id);
//            if (type != null)
//            {
//                _context.Types.Remove(type);
//                _context.SaveChanges();
//            }
//        }

//        public List<TypeVM> GetAll()
//        {
//            var types = _context.Types.Select(type => new TypeVM
//            {
//                IdType = type.IdType,
//                NameType = type.NameType
//            });

//            return types.ToList();
//        }

//        public TypeVM GetById(int id)
//        {
//           var type = _context.Types.SingleOrDefault(type => type.IdType == id);
//            if (type != null) {
//                return new TypeVM
//                {
//                    IdType = type.IdType,
//                    NameType = type.NameType
//                };
//            }
//            else
//            {
//                return null;
//            }
//        }

//        public void Update(TypeVM typeVM)
//        {
//            var type = _context.Types.SingleOrDefault(type => type.IdType == typeVM.IdType);
//            if (type != null) { 
//                type.NameType = typeVM.NameType;
//                _context.SaveChanges();
//            }
//        }
//    }
//}
