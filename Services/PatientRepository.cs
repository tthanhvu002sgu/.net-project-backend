using DoAn_API.Data;
using DoAn_API.Models;

namespace DoAn_API.Services
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyDbContext _context;
        public PatientRepository(MyDbContext context)
        {
            _context = context;
        }

        public PatientVM Add(PatientVM patient)
        {
            var newPatient = new Patient
            {
                email = patient.email,
                password = patient.password,
                image = patient.image,
                phoneNumber = patient.phoneNumber,
                fullName = patient.fullName,
                address = patient.address,
                gender = patient.gender,
                dob = patient.dob,
            };
            _context.Add(newPatient);
            _context.SaveChanges();
            return new PatientVM
            {
                userId = newPatient.userId,
                email = newPatient.email,
                password = newPatient.password,
                image = newPatient.image,
                phoneNumber = newPatient.phoneNumber,
                fullName = newPatient.fullName,
                address = newPatient.address,
                gender = newPatient.gender,
                dob = newPatient.dob,
            };
        }

        public void Delete(int id)
        {
            var patient = _context.Patients.SingleOrDefault(patient => patient.userId == id);
            if (patient != null)
            {
                if (!IsPatientUser(patient))
                {
                    return;
                };

                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        public List<PatientVM> GetAll()
        {
            var patients = _context.Patients.Select(patient => new PatientVM { userId = patient.userId, email = patient.email, password = patient.password, image = patient.image, phoneNumber = patient.phoneNumber, fullName = patient.fullName, address = patient.address, gender = patient.gender, dob = patient.dob });
            return patients.ToList();
        }

        public PatientVM GetPatientVM(int id)
        {
            var patient = _context.Patients.SingleOrDefault(patient => patient.userId == id);
            if (patient != null)
            {
                //if (!IsPatientUser(patient))
                //{
                //    return null;
                //};

                return new PatientVM
                {
                    userId = patient.userId,
                    email = patient.email,
                    password = patient.password,
                    image = patient.image,
                    phoneNumber = patient.phoneNumber,
                    fullName = patient.fullName,
                    address = patient.address,
                    gender = patient.gender,
                    dob = patient.dob
                };
            }
            else
            {
                return null;
            }
        }

        public void Update(PatientVM patient)
        {
            var type = _context.Patients.SingleOrDefault(patient => patient.userId == patient.userId);

            if (type != null)
            {
                if (!IsPatientUser(type))
                {
                    return;
                };

                type.email = patient.email;
                type.password = patient.password;
                type.image = patient.image;
                type.phoneNumber = patient.phoneNumber;
                type.fullName = patient.fullName;
                type.address = patient.address;
                type.gender = patient.gender;
                type.dob = patient.dob;
                _context.SaveChanges();
            }
        }

        public async Task CreatePatient(Patient patient)
        {
            // EF Core sẽ tự động thêm vào bảng Users trước, sau đó thêm vào bảng Patient
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public bool IsPatientUser(Patient patient)
        {
            if (patient.roles == null)
            {
                return false;
            }

            foreach (var role in patient.roles)
            {
                if (role.roleId != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
