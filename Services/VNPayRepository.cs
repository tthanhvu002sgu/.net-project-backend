using DoAn_API.Helper;
using DoAn_API.Models;

namespace DoAn_API.Services
{
    public class VNPayRepository : IVNPayRepository
    {
        private readonly IConfiguration _config;

        public VNPayRepository(IConfiguration config)
        {
            _config = config;
        }
        public string CreatePaymentUrl(HttpContext context, VNPayRequest request)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VNPayLibrary();

            vnpay.AddRequestData("vnp_Version", _config["VNPAY:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VNPAY:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VNPAY:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (request.Amount * 100).ToString());

            vnpay.AddRequestData("vnp_CreateDate", request.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VNPAY:Locale"]);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + request.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VNPAY:ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl = vnpay.CreateRequestUrl(_config["VNPAY:BaseUrl"],
                _config["VNPAY:HashSecret"]);
            return paymentUrl;
        }

        public VNPayResponse PaymentExcute(IQueryCollection collection)
        {
            var vnpay = new VNPayLibrary();
            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddRequestData(key, value.ToString());
                }
            }
            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VNPAY:HashSecret"]);
            if (checkSignature)
            {
                return new VNPayResponse
                {
                    Success = false
                };

            }
            return new VNPayResponse
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderId = vnp_orderId.ToString(),
                OrderDescription = vnp_OrderInfo,
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }
    }
}
