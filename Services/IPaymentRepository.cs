using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IPaymentRepository
    {
        public List<PaymentVM> GetAll();
        PaymentVM GetPaymentVM(int id);
        PaymentVM Add(PaymentVM payment);
        void Update(PaymentVM payment);
        void UpdateStatus(String status, int id);
        void Delete(int id);
    }
}
