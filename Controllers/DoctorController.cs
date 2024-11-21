using DoAn_API.Models;
using DoAn_API.Services;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller

    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly string _firebaseApiKey;
        private readonly string _firebaseBucket;
        private readonly string _firebaseAuthEmail;
        private readonly string _firebaseAuthPassword;
        public DoctorController(IDoctorRepository doctorRepository, IConfiguration configuration)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            // Lấy thông tin Firebase từ appsettings.json
            _firebaseApiKey = configuration["Firebase:ApiKey"];
            _firebaseBucket = configuration["Firebase:Bucket"];
            _firebaseAuthEmail = configuration["Firebase:AuthEmail"];
            _firebaseAuthPassword = configuration["Firebase:AuthPassword"];
        }
        [HttpGet]

        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorRepository.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error retrieving doctors: {ex.Message}");
            }
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetDoctorByEmail(string email)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorByEmail(email);
                if (doctor == null)
                {
                    return NotFound();
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-by-specialization")]
        public async Task<IActionResult> GetDoctorsBySpecializationName(string specializationName)
        {
            try
            {
                var doctors = await _doctorRepository.GetDoctorsBySpecializationAsync(specializationName);
                if (doctors == null || !doctors.Any())
                {
                    return NotFound("No doctors found for this specialization.");
                }

                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            try
            {
                // Xác thực Firebase
                var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(_firebaseAuthEmail, _firebaseAuthPassword);

                // Lấy stream từ file được upload
                using var stream = file.OpenReadStream();

                // Tạo token để hủy upload nếu cần
                var cancellation = new CancellationTokenSource();

                // Tạo tên file mới với UUID để tránh trùng
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

                // Upload file lên Firebase Storage
                var task = new FirebaseStorage(
                    _firebaseBucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("doctorImage") // Folder trên Firebase
                    .Child(uniqueFileName) // Tên file
                    .PutAsync(stream, cancellation.Token);

                // Lấy URL sau khi upload
                var downloadUrl = await task;

                // Trả về link của file đã upload
                return Ok(new { Url = downloadUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Upload failed", Error = ex.Message });
            }
        }
        [HttpGet("get-doctor-info-by-email")]
        public async Task<IActionResult> GetDoctorInfoByEmail(string email)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorInfoByEmail(email);
                if (doctor == null)
                {
                    return NotFound();
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDoctor(string email, [FromBody] DoctorVM doctorVM)
        {
            if (doctorVM == null || string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid data.");
            }

            var result = await _doctorRepository.UpdateDoctorAsync(email, doctorVM);

            if (!result)
            {
                return NotFound($"Doctor with email {email} not found.");
            }

            return Ok("Doctor information updated successfully.");
        }
        [HttpPut("update-availability")]
        public async Task<IActionResult> UpdateAvailability([FromQuery] string doctorEmail, [FromQuery] bool isAvailable)
        {
            var result = await _doctorRepository.UpdateAvailabilityAsync(doctorEmail, isAvailable);
            if (result)
            {
                return Ok(new { message = "Doctor availability updated successfully." });
            }
            return NotFound(new { message = "Doctor not found." });
        }
    }



}
