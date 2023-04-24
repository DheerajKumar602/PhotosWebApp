using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using PhotosWebApp.Models.Dashboard;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace PhotosWebApp.Controllers
{
    public class DashboardController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        AllUsers _user = new AllUsers();
        static string Usertoken;

        public DashboardController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
                  
        }

        [HttpGet]
        public async Task<IActionResult> Home(string token)
        {
            Usertoken = token;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AllUsers _user = new AllUsers();
            ApiResponse _responseApi = new ApiResponse();
            List<UserImages> _Images = new List<UserImages>();
            string accessToken = Usertoken;
            
            using (var httpClient = new HttpClient(_clientHandler))
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:7184/api/Protected/userDetails"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    _responseApi = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    _Images = await GetallImages();
                }
            }
            ViewData["profileImage"] = _responseApi.data.ProfileImage;
            ViewData["username"] = _responseApi.data.Name;
            ViewData["Token"] = Usertoken;
            
            return View(_Images);
        }

        public async Task<List<UserImages>> GetallImages()
        {
            var accessToken = Usertoken;
            List<UserImages> _userImages = new List<UserImages>();
            ApiImgResponse _responseImgApi = new ApiImgResponse();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:7184/api/Protected/ShowImages"))
                {
                    var apiImgResponse = await response.Content.ReadAsStringAsync();
                    _responseImgApi = JsonConvert.DeserializeObject<ApiImgResponse>(apiImgResponse);
                }
            }
            return _responseImgApi.data;
        }

        [HttpGet]
        public IActionResult AddImage()
        {
            ViewData["token"] = Usertoken;
            return View();
        }
        

        [HttpGet]
        public async Task<IActionResult> GetuserDetails()
        {
            try
            {
                AllUsers _user = new AllUsers();
                ApiResponse _responseApi = new ApiResponse();
                string accessToken = Usertoken;

                using (var httpClient = new HttpClient(_clientHandler))
                {

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    using (var response = await httpClient.GetAsync("https://localhost:7184/api/Protected/userDetails"))
                    {
                         var apiResponse = await response.Content.ReadAsStringAsync();
                        _responseApi = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    }
                }
                ViewData["profileImage"] = _responseApi.data.ProfileImage;
                ViewData["username"] = _responseApi.data.Name;
                return View(_responseApi.data);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult deleteImage(int Id,string file) 
        {
            ViewData["id"] = Id;
            ViewData["file"] = file;
            return View();
        }
        [HttpGet]
        public IActionResult finalDelete() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> finalDelete(int Id)
        {
            try
            {
                string accessToken = Usertoken;

                using (var httpClient = new HttpClient(_clientHandler))
                {
                    string data = JsonConvert.SerializeObject(Id);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await httpClient.PostAsync("https://localhost:7184/api/Protected/DeleteImage",content);
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
