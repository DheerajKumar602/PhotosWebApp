using System.ComponentModel.DataAnnotations;

namespace PhotosWebApp.Models.Users
{
    public class LoginModel
    {

        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        //            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        //            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        //            ErrorMessage = "Email is not valid")]
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Password { get; set; }
    }
}
