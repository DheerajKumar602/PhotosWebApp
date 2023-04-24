using System.ComponentModel.DataAnnotations;

namespace PhotosWebApp.Models.Dashboard
{
    public class AddImg
    {
        public int Id { get; set; }
        [Required]
        public IFormFile formFile { get; set; }
    }
}
