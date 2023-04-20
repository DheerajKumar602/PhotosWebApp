using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotosWebApp.Models.Admin;
using PhotosWebApp.Models.Dashboard;
using System.Net.Http.Headers;

namespace PhotosWebApp.Controllers
{
    public class AdminController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        
        public AdminController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }
        public async Task<IActionResult> Index()
        {
           
            ListAdminResponse _responseApi = new ListAdminResponse();
            List<UserImages> _Images = new List<UserImages>();
            // string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5ld3VzZXIiLCJqdGkiOiI0ZWMzZDdlZi1mNDM3LTQwMWItOGVhZS1mYTA4NzNjNjhiYzEiLCJlbWFpbCI6Im5ld3VzZXJAZ21haWwuY29tIiwiaWF0IjoxNjgxODE5Mzc5LCJyb2xlIjoiVXNlciIsIm5iZiI6MTY4MTgxOTM3OSwiZXhwIjoxNjgxOTA1Nzc5fQ.b-LTuQwu5unXlmFDslQciYhhlKEtYqtLwtMyAYaqoI8";
            string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiNzUyN2QwYjYtNjFhYy00NTBkLTgyNmYtMzkxMjExOTRjMDRkIiwiZW1haWwiOiJhZG1pbkBnbWFpbC5jb20iLCJpYXQiOjE2ODE5NzI0ODgsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY4MTk3MjQ4OCwiZXhwIjoxNjgyMDU4ODg4fQ.JL2RBhRNMoazzu33GiqP-VlHgXjjFrVeeIluBLy0D-c";

            using (var httpClient = new HttpClient(_clientHandler))
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:7184/api/Admin/GetAllUsers"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    _responseApi = JsonConvert.DeserializeObject<ListAdminResponse>(apiResponse);
                    
                }
            }
            return View(_responseApi.data);
        }
    }
}
