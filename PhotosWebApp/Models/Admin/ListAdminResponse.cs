using PhotosWebApp.Models.Dashboard;

namespace PhotosWebApp.Models.Admin
{
    public class ListAdminResponse
    {

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public List<AllUser> data { get; set; }
    }
}
