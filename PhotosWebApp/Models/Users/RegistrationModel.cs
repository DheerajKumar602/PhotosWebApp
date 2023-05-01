using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotosWebApp.Models.Users
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public Hobby Hobby { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }
    }

    public enum Hobby
    {
        Sports,
        Reading,
        Movies,
        coding,
        singing,
    }
}
