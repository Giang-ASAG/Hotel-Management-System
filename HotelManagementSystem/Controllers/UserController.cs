using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    //[Route("Admin/users")]
    public class UserController : Controller
    {
        private readonly IApiServiceManager _apiService;
        private readonly IMapper _mapper;

        public UserController(IApiServiceManager apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        // GET: Admin/users
        //[HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var document = await _apiService.UserDocumentService.getAllAsync();
            foreach (var item in document)
            {
                item.User = await _apiService.UserService.getUser(item.UserId);
            }
            ViewBag.Document = document;
            return View(await _apiService.UserService.getAllUser());
        }

        // GET: Admin/users/add
        //[HttpGet("add")]
        public IActionResult formAdd()
        {
            return View();
        }

        // POST: Admin/users/add
       // [HttpPost("add")]
        public async Task<IActionResult> addUser(UserDTO dTO)
        {
            Debug.WriteLine(dTO.UserId);
            Debug.WriteLine(dTO.FullName);
            Debug.WriteLine(dTO.PhoneNumber);
            Debug.WriteLine(dTO.Active);
            Debug.WriteLine(dTO.Address);
            Debug.WriteLine(dTO.UserRoleId);
            Debug.WriteLine(dTO.Email);
            var user = _mapper.Map<User>(dTO);
            user.CreationDate = DateTime.Now;
            var result = await _apiService.UserService.addUser(user);
            TempData["SuccessMessage"] = result ? "Thêm người dùng thành công!" : "Thêm người dùng thất bại";
            return RedirectToAction("Index");
        }

        // GET: Admin/users/edit/5
       // [HttpGet("edit/{id}")]
        public async Task<IActionResult> formEdit(int id)
        {
            var u = await _apiService.UserService.getUser(id);
            u.Password = "";
            return View(u);
        }

        // POST: Admin/users/edit
       // [HttpPost("edit")]
        public async Task<IActionResult> updateUser(UserDTO dTO)
        {
            var user = _mapper.Map<User>(dTO);
            var result = await _apiService.UserService.updateUser(user);
            TempData["SuccessMessage"] = result ? "Sửa người dùng thành công!" : "Sửa người dùng thất bại";
            return RedirectToAction("Index");
        }

        // POST: Admin/users/delete/5
       // [HttpPost("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var result = await _apiService.UserService.deleteUser(id);
            TempData["SuccessMessage"] = result ? "Xóa người dùng thành công!" : "Xóa người dùng thất bại";
            return RedirectToAction("Index");
        }

        //POST: Admin/users/set-to-partner/5
        //[HttpPost("set-to-partner/{id}")]
        public async Task<IActionResult> UpdatePermission(int id)
        {
            var result = await _apiService.UserService.updatePermissionUser(id);
            TempData["SuccessMessage"] = result ? "Sửa quyền người dùng thành công!" : "Sửa quyền người dùng thất bại";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Active(int id, bool active)
        {
            var result = await _apiService.UserService.Active(id,active);
            TempData["SuccessMessage"] = result ? "Thành công!" : "Thất bại";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> updateActive(int id, int num, string email)
        {
            var result = await _apiService.UserDocumentService.updateActive(id, num);
            await _apiService.UserService.Active(id, true);
            if (num > 1)
            {
                await _apiService.EmailService.SendToUserFail(email);
                TempData["SuccessMessage"] = result ? "Đã từ chối!" : "Từ chối thất bại";
                return RedirectToAction("Index");
            }
            await _apiService.EmailService.SendToUser(email);
            TempData["SuccessMessage"] = result ? "Duyệt thành công!" : "Duyệt thất bại";
            return RedirectToAction("Index");
        }
    }
}
