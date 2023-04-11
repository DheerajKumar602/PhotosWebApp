using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotosWebApp.Models.Users;
using System.Collections.Generic;
using System.Reflection;

namespace PhotosWebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var list = new SelectList(CultureHelper.CountryList(), "Key", "Value");
            var sortList = list.OrderBy(p => p.Text).ToList();
            ViewBag.Countries = sortList;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            //write code to send otp
            return View();
        }

      //  [HttpPost]
        public IActionResult Logout()
        {
            //Delete Access Token From Database
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string Email)
        {
            //Validiate if Email Exists and Generate OTP and Redirect to reset Password Page else return no user Exists response
            TempData["Email"] = Email;
            return RedirectToAction("SetNewPassword");
        }

        [HttpGet]
        public IActionResult SetNewPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetNewPassword(string Email, string Otp, string NewPassword)
        {
            //Validaite OTP with EMail id If  valid Update Password Else GIve Invalid Otp Response or Relevant Response
            return View();
        }
    }
}
