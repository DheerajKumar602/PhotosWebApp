using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotosWebApp.Models.Users;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System;
using PhotosWebApp.Models;
using System.Net;
using NuGet.Protocol;
using System.Security.Policy;
using static NuGet.Packaging.PackagingConstants;
using System.IO;
using NuGet.Common;
using static System.Net.WebRequestMethods;

namespace PhotosWebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Token = TempData["token"] as string;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            var client = new RestClient($@"{enums.apiUrl}/api/Authorization/Login");
            var request = new RestRequest();
            object jsonDataBody = JsonConvert.SerializeObject(loginModel);
            request.AddJsonBody(jsonDataBody);

            try
            {
                var response = client.Post(request);
                string Resp = response.Content.ToString();
                JObject RespJson = JObject.Parse(Resp);
                string Token;
                string role;

                if (int.Parse(RespJson["statusCode"].ToString()) == 200) //Checking if user logged in & Token is not null
                {
                    TempData.Clear();
                    Token = RespJson["data"]["token"].ToString();
                    TempData["token"] = RespJson["data"]["token"].ToString();
                    TempData["refreshToken"] = RespJson["data"]["refreshToken"].ToString();
                    role = RespJson["data"]["role"].ToString();
                    TempData["message"] = "";

                    TempData.Keep();
                    //Redirect to Users dashboard  & Add
                    if (role == "User")
                    {
                        return RedirectToAction("Home", "Dashboard", new { Token });
                    }
                    else if (role == "Admin")
                    {
                        return RedirectToAction("Home", "Admin", new { Token });
                    }
                    TempData["message"] = RespJson["message"];
                    TempData.Keep();
                    return View();
                }

                if (int.Parse(RespJson["statusCode"].ToString()) == 202) //Checking if user logged in & Token is not null
                {
                    TempData.Clear();
                    TempData["message"] = RespJson["data"].ToString();
                    TempData.Keep();
                    return RedirectToAction("RegistrationVerifyOtp");
                }

                else
                {
                    //ReturnView With Invalid ID/PasswordScreen
                    TempData["message"] = RespJson["message"]; TempData.Keep();
                    return View();
                }
            }

            catch (Exception ex)
            { //handle Exception Here , Like Api down or json Parsing Erorrs etc.
                TempData["message"] = "Error Occured. Looks Like Api down or Something Went Wrong.";
                TempData.Keep();
                return View();
            }

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
        public async Task<IActionResult> Register(RegistrationModel registrationModel, string EnableRedirect)
        {

            if (EnableRedirect == "true")
            {
                TempData["email"] = registrationModel.Email;
                TempData.Keep();
                return RedirectToAction("RegistrationVerifyOtp");
            }
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
            var client = new RestClient($@"{enums.apiUrl}/api/Authorization/GenerateOtp");
            var request = new RestRequest();
            var jsonData = new
            {
                email = Email
            };
            object jsonDataBody = JsonConvert.SerializeObject(jsonData);
            request.AddJsonBody(jsonDataBody);

            try
            {
                var response = client.Post(request);
                string Resp = response.Content.ToString();
                JObject RespJson = JObject.Parse(Resp);
                string Token;
                string role;
                if (int.Parse(RespJson["statusCode"].ToString()) == 200) //Checking if user logged in & Token is not null
                {
                    TempData["Email"] = Email;
                    TempData.Keep();
                    return RedirectToAction("SetNewPassword", TempData);
                }

                TempData["message"] = RespJson["message"].ToString();
                TempData.Keep();
                return View();

            }

            catch (Exception ex)
            { //handle Exception Here , Like Api down or json Parsing Erorrs etc.
                TempData["message"] = "Error Occured. Looks Like Api down or Something Went Wrong.";
                TempData.Keep();
                return View();
            }

            //Validiate if Email Exists and Generate OTP and Redirect to reset Password Page else return no user Exists response

        }

        [HttpGet]
        public IActionResult SetNewPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetNewPassword(string Email, int Otp, string NewPassword, string ConfirmPassword)
        {
            //Validaite OTP with EMail id If  valid Update Password Else GIve Invalid Otp Response or Relevant Response
            var client = new RestClient($@"{enums.apiUrl}/api/Authorization/ForgotPassword");
            var request = new RestRequest();
            var jsonData = new
            {
                email = Email,
                otp = Otp,
                newPassword = NewPassword,
                confirmNewPassword = ConfirmPassword
            };
            object jsonDataBody = JsonConvert.SerializeObject(jsonData);
            request.AddJsonBody(jsonDataBody);

            try
            {
                var response = client.Post(request);
                string Resp = response.Content.ToString();
                JObject RespJson = JObject.Parse(Resp);

                if (int.Parse(RespJson["statusCode"].ToString()) == 200) //Checking if user logged in & Token is not null
                {
                    TempData["message"] = RespJson["message"].ToString();
                    TempData.Keep();

                    return RedirectToAction("Login");
                }

                TempData["message"] = RespJson["message"].ToString();
                TempData.Keep();
                return View();
            }

            catch (Exception ex)
            { //handle Exception Here , Like Api down or json Parsing Erorrs etc.
                TempData["message"] = "Error Occured. Looks Like Api down or Something Went Wrong.";
                TempData.Keep();
                return View();
            }

            return View();
        }

        [HttpPost]
        public IActionResult RegistrationVerifyOtp(string email, string otp)
        {
            var client = new RestClient($@"{enums.apiUrl}/api/Authorization/VerifyRegistration");
            var request = new RestRequest();
            var jsonData = new
            {
                email = email,
                otp = otp
            };
            object jsonDataBody = JsonConvert.SerializeObject(jsonData);
            request.AddJsonBody(jsonDataBody);

            try
            {
                var response = client.Post(request);
                string Resp = response.Content.ToString();
                JObject RespJson = JObject.Parse(Resp);

                if (int.Parse(RespJson["statusCode"].ToString()) == 200)
                {
                    TempData["message"] = RespJson["message"].ToString();
                    TempData.Keep();

                    return RedirectToAction("Login");
                }

                TempData["message"] = RespJson["message"].ToString();
                TempData.Keep();
                return View();
            }

            catch (Exception ex)
            { //handle Exception Here , Like Api down or json Parsing Erorrs etc.
                TempData["message"] = "Error Occured. Looks Like Api down or Something Went Wrong.";
                TempData.Keep();
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationVerifyOtp(string email)
        {
            TempData["Email"] = email;
            TempData.Keep();
            return View();
        }
    }
}