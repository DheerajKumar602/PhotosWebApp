namespace PhotosWebApp.Models.Dashboard
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public AllUsers data { get; set; }
    }
}
