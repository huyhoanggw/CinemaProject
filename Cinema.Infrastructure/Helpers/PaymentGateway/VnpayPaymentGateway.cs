using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using Cinema.Infrastructure.Helpers.Vnpay;
using Microsoft.Extensions.Options;
using SeedWorks.Models.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Helpers.PaymentGateway
{
    public class VnpayPaymentGateway(IOptions<VnpayOptions> options) : IPaymentGateway
    {
        public PaymentMethod PaymentMethod => PaymentMethod.VnPay;
            VnpayOptions options = options.Value;
        public Task<PaymentGatewayResult> CreatePaymentAsync(PaymentGatewayRequest request, CancellationToken cancellationToken)
        {
            
            var parameters = new SortedDictionary<string, string>()
            {
                ["vnp_Version"] = "2.1.0" ,
                ["vnp_Command"] = "pay",
                ["vnp_TmnCode"] = options.vnp_TmnCode,
                ["vnp_Amount"] = ((long)request.Amount * 100).ToString(CultureInfo.InvariantCulture),
                ["vnp_CurrCode"] = "VND",
                ["vnp_TxnRef"] =
                request.OrderCode,
                ["vnp_OrderInfo"] =
                $"Thanh toan don hang {request.OrderCode}",
                ["vnp_OrderType"] = "other",

                ["vnp_Locale"] = "vn",
                ["vnp_ReturnUrl"] =
                request.ReturnUrl,

                ["vnp_IpAddr"] =
                request.clientIp,

                ["vnp_CreateDate"] =
                DateTime.Now.ToString("yyyyMMddHHmmss"),

                ["vnp_ExpireDate"] =
                DateTime.Now.AddMinutes(15)
                    .ToString("yyyyMMddHHmmss")

            };
            var queryString = BuildQueryString(parameters);
            var hash = ComputeHmacSha512(options.vnp_HashSecret,queryString);
            var paymenturl = $"{options.vnp_Url}" + $"?{queryString}" + $"&vnp_SecureHash={hash}";
            return Task.FromResult(new PaymentGatewayResult(true,
                null,
                paymenturl,
                 null) );
        }

        public Task<PaymentGatewayResult> VerifyPaymentAsync(SortedDictionary<string, string>  request, CancellationToken cancellationToken)
        {
            if (!request.TryGetValue("vnp_TxnRef",out var orderCode))
            {
                return Task.FromResult(
                    new PaymentGatewayResult(
                        false,
                        null,
                        null,
                        "Missing vnp_TxnRef"));
            }

            if (!request.TryGetValue("vnp_SecureHash", out var secureHash))
            {
                return Task.FromResult(
                    new PaymentGatewayResult(
                        false,
                        null,
                        null,
                        "Missing vnp_SecureHash"));
            }
            if(!request.TryGetValue("vnp_TransactionNo",out var TransactionId))
            {
                return Task.FromResult(
                                 new PaymentGatewayResult(
                                     false,
                                     null,
                                     null,
                                     "Missing vnp_TransactionNo"));
            }
            if(!request.TryGetValue("vnp_TransactionStatus", out var TransactionStatus))
            {
                return Task.FromResult(
                                 new PaymentGatewayResult(
                                     false,
                                     null,
                                     null,
                                     "Missing vnp_TransactionStatus"));
            }
            if(!request.TryGetValue("vnp_ResponseCode", out var ResponseCode))
            {
                return Task.FromResult(
                                 new PaymentGatewayResult(
                                     false,
                                     null,
                                     null,
                                     "Missing vnp_ResponseCode"));
            }

           
            var vnpayParams = new SortedDictionary<string, string>(request);
            var querystring = BuildQueryString(vnpayParams);
            vnpayParams.Remove("vnp_SecureHash");
            vnpayParams.Remove("vnp_SecureHashType");
            var expectedHash = ComputeHmacSha512(options.vnp_HashSecret, querystring);

            var validSignature =
                string.Equals(
                    expectedHash,
                    secureHash,
                    StringComparison.OrdinalIgnoreCase);
            if (!validSignature)
            {
                return Task.FromResult(new PaymentGatewayResult(false, null, null, "Invalid Vnpay signature "));
            }
            var success = TransactionStatus == "00" && ResponseCode == "00";
            return Task.FromResult(new PaymentGatewayResult(true, TransactionId, querystring, success ? "payment successfully" : "payment failed"));
        }
        private static string BuildQueryString(
       SortedDictionary<string, string> parameters)
        {
            return string.Join("&", parameters
        .Where(kv => !string.IsNullOrEmpty(kv.Value))
        .Select(kv => $"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}"));
        
        }
        private static string ComputeHmacSha512(
         string key,
         string data)
        {
            using var hmac =
                new HMACSHA512(
                    Encoding.UTF8.GetBytes(key));

            var hash =
                hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(data));

            return Convert
                .ToHexString(hash)
                .ToLowerInvariant();
        }

        public Task<bool> ValidateSignature(SortedDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
