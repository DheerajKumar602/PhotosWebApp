namespace PhotosWebApp.Models.Users
{
    public class SetNewPasswordModel
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }
}
