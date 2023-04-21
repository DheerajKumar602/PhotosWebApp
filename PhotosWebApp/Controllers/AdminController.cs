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
        
        public AdminController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }
        public async Task<IActionResult> Index(string Token)
        {
           
            ListAdminResponse _responseApi = new ListAdminResponse();
            List<UserImages> _Images = new List<UserImages>();
             string accessToken = Token;

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
