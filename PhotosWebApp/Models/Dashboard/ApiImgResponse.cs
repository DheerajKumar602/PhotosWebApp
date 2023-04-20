namespace PhotosWebApp.Models.Dashboard
{
    public class ApiImgResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public List<UserImages> data { get; set; }
    }
}
