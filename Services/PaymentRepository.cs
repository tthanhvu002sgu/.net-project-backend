using DoAn_API.Data;
using DoAn_API.Models;

namespace DoAn_API.Services
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyDbContext _context;


        public PaymentRepository(MyDbContext context)
        {
            _context = context;
        }

        public PaymentVM Add(PaymentVM payment)
        {
            var newPayment = new Payment
            {
                patientId = payment.patientId,
                appointmentId = payment.appointmentId,
                paymentId = _context.Payments.Count() + 1,
                price = payment.price,
                paymentMethod = payment.paymentMethod,
                paymentStatus = payment.paymentStatus


            };
            _context.Add(newPayment);
            _context.SaveChanges();
            return new PaymentVM
            {
                patientId = payment.patientId,
                patientEmail = payment.patientEmail,
                appointmentId = payment.appointmentId,
                paymentId = _context.Payments.Count() + 1,
                price = payment.price,
                paymentMethod = payment.paymentMethod,
                paymentStatus = payment.paymentStatus
            };
        }

        public void Delete(int id)
        {
            var payment = _context.Payments.SingleOrDefault(payment => payment.paymentId == id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }

        public List<PaymentVM> GetAll()
        {
            var payments = _context.Payments.Select(payment => new PaymentVM
            {
                patientId = payment.patientId,
                appointmentId = payment.appointmentId,
                paymentId = _context.Payments.Count() + 1,
                price = payment.price,
                paymentMethod = payment.paymentMethod,
                paymentStatus = payment.paymentStatus
            });
            return payments.ToList();
        }

        public PaymentVM GetPaymentVM(int id)
        {
            var payment = _context.Payments.SingleOrDefault(pay => pay.paymentId == id);
            if (payment != null)
            {
                return new PaymentVM
                {
                    patientId = payment.patientId,
                    appointmentId = payment.appointmentId,
                    paymentId = _context.Payments.Count() + 1,
                    price = payment.price,
                    paymentMethod = payment.paymentMethod,
                    paymentStatus = payment.paymentStatus
                };
            }
            return null;
        }


        public void Update(PaymentVM payment)
        {
            var pay = _context.Payments.SingleOrDefault(payment => payment.paymentId == payment.paymentId);
            if (pay != null)
            {

                pay.patientId = payment.patientId;
                pay.appointmentId = payment.appointmentId;
                pay.paymentId = _context.Payments.Count() + 1;
                pay.price = payment.price;
                pay.paymentMethod = payment.paymentMethod;
                pay.paymentStatus = payment.paymentStatus;
                _context.SaveChanges();
            }

        }

        public void UpdateStatus(string status, int id)
        {
            var payment = _context.Payments.SingleOrDefault(payment => payment.appointmentId == id);
            if (payment != null)
            {
                payment.paymentStatus = status;
                _context.SaveChanges();
            }
        }
    }
}
