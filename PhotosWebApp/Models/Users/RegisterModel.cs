using System.ComponentModel.DataAnnotations;

namespace PhotosWebApp.Models.Users
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone No. is Required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Hobby is Required")]
        public string Hobby { get; set; }

        [Required(ErrorMessage = "Profile Picture is Required")]
        public string ProfilePicUrl { get; set; } //Try to take url
    }
}
