using DoAn_API.Models;
using DoAn_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IVNPayRepository _vnPayRepository;

        public PaymentController(IPaymentRepository paymentRepository, IVNPayRepository vNPayRepository)
        {
            _paymentRepository = paymentRepository;
            _vnPayRepository = vNPayRepository;
        }

        [HttpPost("cash")]
        public IActionResult PayWithCash([FromBody] PaymentVM payment)
        {
            if (payment == null)
            {
                return BadRequest("Invalid payment data");
            }
            payment.paymentMethod = "CAST";
            var result = _paymentRepository.Add(payment);

            return Ok(result);
        }

        [HttpGet("{paymentId}")]
        public IActionResult GetPayment(int paymentId)
        {
            var payment = _paymentRepository.GetPaymentVM(paymentId);
            if (payment == null)
            {
                return NotFound(new { message = "Payment not found" });

            }
            return Ok(payment);
        }

        [HttpDelete("{paymentId}")]
        public IActionResult DeletePayment(int paymentId)
        {
            _paymentRepository.Delete(paymentId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllPayment()
        {
            return Ok(_paymentRepository.GetAll());
        }

        [HttpPut("update/{paymentId}")]
        public IActionResult UpdatePayment([FromQuery] PaymentVM payment, int paymentId)
        {
            if (paymentId != payment.paymentId)
            {
                return BadRequest();
            }
            _paymentRepository.Update(payment);
            return NoContent();
        }


        [HttpPost]
        [Route("VNPay")]
        public IActionResult PayWithOnline(VNPayRequest model)
        {
            model.OrderId = new Random().Next(1000, 10000);
            model.CreatedDate = DateTime.Now;
            PaymentVM entity = new PaymentVM
            {
                paymentId = model.OrderId,
                patientId = model.userId,
                appointmentId = model.AppointmentId,
                price = (decimal)model.Amount,
                paymentMethod = "VNPAY",
                paymentStatus = "Chưa thanh toán"
            };
            _paymentRepository.Add(entity);
            var returnUrl = _vnPayRepository.CreatePaymentUrl(HttpContext, model);
            return Ok(returnUrl);
        }

        [HttpGet]
        [Route("PaymentCallBack")]
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayRepository.PaymentExcute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                return BadRequest("Payment fail" + "Code: " + response.VnPayResponseCode);
            }
            string id = response.PaymentId;
            int number;
            bool success = int.TryParse(id, out number);
            _paymentRepository.UpdateStatus("Đã thanh toán", number);
            return Ok();
        }


    }
}
