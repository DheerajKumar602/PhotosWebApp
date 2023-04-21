﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Identity;

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
                if (int.Parse(RespJson["statusCode"].ToString()) == 200 && RespJson["data"]["token"].ToString() != "") //Checking if user logged in & Token is not null
                {
                    TempData.Clear();
                    Token = RespJson["data"]["token"].ToString();
                    TempData["token"] = RespJson["data"]["token"].ToString();
                    TempData["refreshToken"] = RespJson["data"]["refreshToken"].ToString();
                    role = RespJson["data"]["role"].ToString();
                    //////////////////////////////////////////////////////////////////////////////////////////Pending  /////////////////
                    ///
                    TempData["message"] = "Login Success but Dashboard Redirection not yet implemented";

                    //Redirect to Users dashboard  & Add
                    if (role == "User")
                    {
                        return RedirectToAction("Index", "Dashboard", new { Token });
                    }
                    else if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin", new { Token });
                    }
                    TempData["message"] = RespJson["message"];
                    return View();
                }

                else
                {
                    //ReturnView With Invalid ID/PasswordScreen
                    TempData["message"] = RespJson["message"];
                    return View();
                }
            }

            catch(Exception ex)
            { //handle Exception Here , Like Api down or json Parsing Erorrs etc.
                TempData["message"] = "Error Occured. Looks Like Api down or Something Went Wrong.";
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
        public IActionResult Register(RegisterModel registerModel)
        {
            //write code to send otp
            return RedirectToAction("RegistrationVerifyOtp");
           
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

        [HttpPost]
        public IActionResult RegistrationVerifyOtp(string email)
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationVerifyOtp()
        {
            return View();
        }
    }
}
