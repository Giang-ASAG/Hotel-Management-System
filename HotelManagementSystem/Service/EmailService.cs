using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpFactory;
        public EmailService(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }
        public async Task<bool> checkEmailAsync(string email)
        {
            var client = _httpFactory.CreateClient("MyApi");
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Auth/check-Email/{email}",content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendOTPAsync(string email)
        {
            var client = _httpFactory.CreateClient("MyApi");
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"Auth/send-otp?email={email}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendToHost(int id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            var response = await client.PostAsync($"Auth/sendtoHost?id={id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendToUser(string email)
        {
            var client = _httpFactory.CreateClient("MyApi");
            var response = await client.PostAsync($"Auth/sendtoUser?email={email}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendToUserFail(string email)
        {
            var client = _httpFactory.CreateClient("MyApi");
            var response = await client.PostAsync($"Auth/sendtoUserFail?email={email}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyOTPAsync(VerifyOTP verifyOTP)
        {
            var client = _httpFactory.CreateClient("MyApi");
            var content = new StringContent(JsonSerializer.Serialize(verifyOTP), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"Auth/verify-otp", content);
            return response.IsSuccessStatusCode;
        }
    }
}
