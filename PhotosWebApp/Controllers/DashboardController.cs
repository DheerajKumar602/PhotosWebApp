using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotosWebApp.Models.Dashboard;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace PhotosWebApp.Controllers
{
    public class DashboardController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        AllUsers _user = new AllUsers();

        public DashboardController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AllUsers _user = new AllUsers();
            ApiResponse _responseApi = new ApiResponse();
            List<UserImages> _Images = new List<UserImages>();
           // string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5ld3VzZXIiLCJqdGkiOiI0ZWMzZDdlZi1mNDM3LTQwMWItOGVhZS1mYTA4NzNjNjhiYzEiLCJlbWFpbCI6Im5ld3VzZXJAZ21haWwuY29tIiwiaWF0IjoxNjgxODE5Mzc5LCJyb2xlIjoiVXNlciIsIm5iZiI6MTY4MTgxOTM3OSwiZXhwIjoxNjgxOTA1Nzc5fQ.b-LTuQwu5unXlmFDslQciYhhlKEtYqtLwtMyAYaqoI8";
            string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imxhc3R1c2VyIiwianRpIjoiZGMwYmE5MDQtNWVkMy00NjQ1LWJmOGUtMjY4YjAzZWI1YjcyIiwiZW1haWwiOiJsYXN0dXNlckBnbWFpbC5jb20iLCJpYXQiOjE2ODE5OTQyNTksInJvbGUiOiJVc2VyIiwibmJmIjoxNjgxOTk0MjU5LCJleHAiOjE2ODIwODA2NTl9.GzVTCcuBhGA_4ryBYAdjFRtk0zjqVvYGTKII6jJUEbk";

            using (var httpClient = new HttpClient(_clientHandler))
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:7184/api/Protected/userDetails"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    _responseApi = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    _Images = await GetallImages(accessToken);
                }
            }
            ViewData["profileImage"] = _responseApi.data.ProfileImage;
            ViewData["username"] = _responseApi.data.Name;
            
            
            return View(_Images);
        }

        public async Task<List<UserImages>> GetallImages(string accessToken)
        {
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
            return View();
        }
        

        [HttpGet]
        public async Task<IActionResult> GetuserDetails()
        {
            try
            {
                AllUsers _user = new AllUsers();
                ApiResponse _responseApi = new ApiResponse();
                //string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5ld3VzZXIiLCJqdGkiOiI0ZWMzZDdlZi1mNDM3LTQwMWItOGVhZS1mYTA4NzNjNjhiYzEiLCJlbWFpbCI6Im5ld3VzZXJAZ21haWwuY29tIiwiaWF0IjoxNjgxODE5Mzc5LCJyb2xlIjoiVXNlciIsIm5iZiI6MTY4MTgxOTM3OSwiZXhwIjoxNjgxOTA1Nzc5fQ.b-LTuQwu5unXlmFDslQciYhhlKEtYqtLwtMyAYaqoI8";
                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imxhc3R1c2VyIiwianRpIjoiZGMwYmE5MDQtNWVkMy00NjQ1LWJmOGUtMjY4YjAzZWI1YjcyIiwiZW1haWwiOiJsYXN0dXNlckBnbWFpbC5jb20iLCJpYXQiOjE2ODE5OTQyNTksInJvbGUiOiJVc2VyIiwibmJmIjoxNjgxOTk0MjU5LCJleHAiOjE2ODIwODA2NTl9.GzVTCcuBhGA_4ryBYAdjFRtk0zjqVvYGTKII6jJUEbk";

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
    }
}
