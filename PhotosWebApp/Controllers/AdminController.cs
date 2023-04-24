using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotosWebApp.Models;
using PhotosWebApp.Models.Admin;
using PhotosWebApp.Models.Dashboard;
using System.Net.Http.Headers;

namespace PhotosWebApp.Controllers
{
    public class AdminController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        static string Usertoken;
        public AdminController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }
        [HttpGet]
        public async Task<IActionResult> Home(string token)
        {
            Usertoken = token;
            return RedirectToAction("Dashboard");
        }
        public async Task<IActionResult> Dashboard()
        {
           
            ListAdminResponse _responseApi = new ListAdminResponse();
            List<UserImages> _Images = new List<UserImages>();
             string accessToken = Usertoken;

            using (var httpClient = new HttpClient(_clientHandler))
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync($"{enums.apiUrl}/api/Admin/GetAllUsers"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    _responseApi = JsonConvert.DeserializeObject<ListAdminResponse>(apiResponse);
                    
                }
            }
            return View(_responseApi.data);
        }
    }
}
