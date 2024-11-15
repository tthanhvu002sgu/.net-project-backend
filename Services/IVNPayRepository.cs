using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IVNPayRepository
    {
        string CreatePaymentUrl(HttpContext context, VNPayRequest request);
        VNPayResponse PaymentExcute(IQueryCollection collection);
    }
}
