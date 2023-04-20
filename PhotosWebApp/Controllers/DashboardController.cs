using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotosWebApp.Models.Dashboard;
using System.Net.Http.Headers;

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
            string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imxhc3R1c2VyIiwianRpIjoiMTdiZDVkZmEtNDRkNS00NjMyLWE1MDktM2IxZGI2NDBlZGRkIiwiZW1haWwiOiJsYXN0dXNlckBnbWFpbC5jb20iLCJpYXQiOjE2ODE5MDU3NzQsInJvbGUiOiJVc2VyIiwibmJmIjoxNjgxOTA1Nzc0LCJleHAiOjE2ODE5OTIxNzR9.oViuNyssCUTMjOeCdLjG7lmagYynHVp1Fy6z7dbpt2Y";

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
        public async Task<IActionResult> GetuserDetails()
        {
            try
            {
                AllUsers _user = new AllUsers();
                ApiResponse _responseApi = new ApiResponse();
                //string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5ld3VzZXIiLCJqdGkiOiI0ZWMzZDdlZi1mNDM3LTQwMWItOGVhZS1mYTA4NzNjNjhiYzEiLCJlbWFpbCI6Im5ld3VzZXJAZ21haWwuY29tIiwiaWF0IjoxNjgxODE5Mzc5LCJyb2xlIjoiVXNlciIsIm5iZiI6MTY4MTgxOTM3OSwiZXhwIjoxNjgxOTA1Nzc5fQ.b-LTuQwu5unXlmFDslQciYhhlKEtYqtLwtMyAYaqoI8";
                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imxhc3R1c2VyIiwianRpIjoiMTdiZDVkZmEtNDRkNS00NjMyLWE1MDktM2IxZGI2NDBlZGRkIiwiZW1haWwiOiJsYXN0dXNlckBnbWFpbC5jb20iLCJpYXQiOjE2ODE5MDU3NzQsInJvbGUiOiJVc2VyIiwibmJmIjoxNjgxOTA1Nzc0LCJleHAiOjE2ODE5OTIxNzR9.oViuNyssCUTMjOeCdLjG7lmagYynHVp1Fy6z7dbpt2Y";

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
