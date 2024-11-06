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

            };
            _context.Add(newPatient);
            _context.SaveChanges();
            return new PatientVM
            {

            };
        }

        public void Delete(int id)
        {
            var patient = _context.Patients.SingleOrDefault(patient => patient.patientId == id);
            if (patient != null)
            {


                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        //public List<PatientVM> GetAll()
        //{

        //    var patients = _context.Patients.Select(patient => new PatientVM { userId = patient.patientId, email = patient.email, password = patient.password, image = patient.image, phoneNumber = patient.phoneNumber, fullName = patient.fullName, address = patient.address, gender = patient.gender, dob = patient.dob });
        //    return patients.ToList();
        //}

        //public PatientVM GetPatientVM(int id)
        //{
        //    var patient = _context.Patients.SingleOrDefault(patient => patient.patientId == id);
        //    if (patient != null)
        //    {
        //        //if (!IsPatientUser(patient))
        //        //{
        //        //    return null;
        //        //};

        //        return new PatientVM
        //        {
        //            userId = patient.userId,
        //            email = patient.email,
        //            password = patient.password,
        //            image = patient.image,
        //            phoneNumber = patient.phoneNumber,
        //            fullName = patient.fullName,
        //            address = patient.address,
        //            gender = patient.gender,
        //            dob = patient.dob
        //        };
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public void Update(PatientVM patient)
        //{
        //    var type = _context.Patients.SingleOrDefault(patient => patient.userId == patient.userId);

        //    if (type != null)
        //    {


        //        type.email = patient.email;
        //        type.password = patient.password;
        //        type.image = patient.image;
        //        type.phoneNumber = patient.phoneNumber;
        //        type.fullName = patient.fullName;
        //        type.address = patient.address;
        //        type.gender = patient.gender;
        //        type.dob = patient.dob;
        //        _context.SaveChanges();
        //    }
        //}


        public List<PatientVM> GetAll()
        {
            throw new NotImplementedException();
        }



        public void Update(PatientVM patient)
        {
            throw new NotImplementedException();
        }

        public PatientVM GetPatientVM(int id)
        {
            throw new NotImplementedException();
        }
    }
}
