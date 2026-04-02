using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IApiServiceManager _apiService;
        private readonly IMapper _mapper;

        public AuthController(IHttpClientFactory clientFactory, IApiServiceManager apiService, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _apiService = apiService;
            _mapper = mapper;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login() => View();


        [HttpGet, AllowAnonymous]
        public IActionResult VerifyOTP() {
            return View();
        }
        public async Task<IActionResult> sendOTP(string email)
        {
            var check = await _apiService.EmailService.checkEmailAsync(email);
            if (check)
            {

                var reult = await _apiService.EmailService.SendOTPAsync(email);
                if(reult)
                {
                    TempData["SuccessMessage"] = "Đã gửi OTP, có hiệu lực trong 5 phút.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Email đã tồn tại trong hệ thống.";
            }

            ViewBag.Email = email;
            return View("VerifyOTP");
        }
        public async Task<IActionResult> verifyOTP(VerifyOTP verifyOTP)
        {
            var result = await _apiService.EmailService.VerifyOTPAsync(verifyOTP);
            if (result)
            {
                TempData["SuccessMessage"] = "Xác thực thành công";
                TempData["EmailOTP"] = verifyOTP.Email;
                TempData["OtpVerified"] = true;
                return RedirectToAction("VerifyOTP");
            }
            TempData["ErrorMessage"] = "Mã OTP không chính xác hoặc đã hết hạn.";
            TempData["EmailOTP"] = verifyOTP.Email;
            return RedirectToAction("VerifyOTP");
        }


        [HttpGet("approve-permission/{id}")]
        public async Task<IActionResult> ApprovePermission(int id)
        {
            // Gửi yêu cầu PUT đến API nội bộ
            var result = await _apiService.UserService.updatePermissionUser(id);

            if (result)
            {
                var user = await _apiService.UserService.getUser(id);
                var send = await _apiService.EmailService.SendToUser(user.Email);
                if (send)
                {
                    return Content("Duyệt thành công!");
                }
            }

            return Content("Duyệt thất bại.");
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var loginmodel = new Models.Login
            {
                Email = email,
                Password = password
            };
            var response = await client.PostAsJsonAsync("/api/Auth/login", loginmodel);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Email hoặc mật khẩu không đúng.";
                return View();
            }

            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var token = json.RootElement.GetProperty("token").GetString();
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var claims = jwt.Claims.ToList();
            var userid= claims.FirstOrDefault(c => c.Type == "id")?.Value;
            //var role= claims.FirstOrDefault(c => c.Type == "role")?.Value;

            var user = await _apiService.UserService.getUser(int.Parse(userid));


            // Điều hướng theo vai trò
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            if (user.UserRoleId == 1)
            {
                // Lưu token vào Session
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("UserId", userid);
                if (user.FullName != null)
                {
                    HttpContext.Session.SetString("Name", user.FullName);
                }
                // Tạo ClaimsIdentity
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Admin");
            }
            else if(user.UserRoleId == 3)
            {
                // Lưu token vào Session
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("UserId", userid);
                if (user.FullName != null)
                {
                    HttpContext.Session.SetString("Name", user.FullName);
                }
                // Tạo ClaimsIdentity
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var info = await _apiService.UserDocumentService.getAsyncbyUserId(user.UserId);
                if (info)
                {
                    ViewBag.Message = "Vui lòng chờ duyệt, nếu qua 24h chưa thấy vui lòng liên hệ chúng tôi";
                    return View("Success");
                }
                return RedirectToAction("Register",new {id = user.UserId }); 
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterWithPassword(string email, string password,  string ConfirmPassword)
        {
            if (password != ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu không khớp.";
                TempData["OtpVerified"] = true;
                TempData["Email"] = email;
                return RedirectToAction("VerifyOTP");
            }
            var newUser = new Login
            {
                Email = email,
                Password = password,
            };
            var client = _clientFactory.CreateClient("MyApi");
            var response = await client.PostAsJsonAsync("Auth/register", newUser);
            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Đăng ký thất bại!";
                TempData["OtpVerified"] = true;
                TempData["Email"] = email;
                return RedirectToAction("VerifyOTP");
            }
            return RedirectToAction("Login");
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Register(int id)
        {
            var user = await _apiService.UserService.getUser(id);
            var document = new UserDocument
            {
                UserId = id,
                User = user,
            };
            return View(document);
        }
        [HttpPost]
        public async Task<IActionResult> AddDocument(int UserId, string CccdNumber, string TaxCode, IFormFile ImageBase64, string BankAccountNumber, string BankName,
            string name, string sdt, string address)
        {
            var u = new UserDocumentDTO
            {
                CccdNumber = CccdNumber,
                TaxCode = TaxCode,
                BankAccountNumber = BankAccountNumber,
                BankName = BankName,
                ImageBase64 = ConvertIFormFileToByteArray(ImageBase64),
                CreatedAt = DateTime.Now,
                UserId = UserId,
                Active = 0
            };

            var result = await _apiService.UserDocumentService.addAsync(u);
            if (result != null)
            {
                var usertemp = await _apiService.UserService.getUser(u.UserId);
                usertemp.PhoneNumber = sdt;
                usertemp.FullName = name;
                usertemp.Address = address;
                await _apiService.UserService.updateUser(usertemp);
                await _apiService.EmailService.SendToHost(result.UserId);
                ViewBag.Message = "Bạn đã gửi hồ sơ thành công. Chúng tôi sẽ sớm duyệt trong vòng 24h.";
                return View("Success");
            }
            ViewBag.ErrorMessage = "Gửi hồ sơ thất bại";
            return View("Error");
        }

        private byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }


        public async Task<IActionResult> Logout()
        {
            var client = _clientFactory.CreateClient("MyApi");

            var token = HttpContext.Session.GetString("JWToken"); // hoặc lấy từ cookie nếu bạn lưu token ở đó
            if (token != null)
            {
               
                HttpContext.Session.Remove("JWToken");
                await HttpContext.SignOutAsync();
            }

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
