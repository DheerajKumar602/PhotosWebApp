namespace PhotosWebApp.Models
{
    public class enums
    {
         public static string apiUrl = "https://localhost:7282";
        // public static string apiUrl = "http://192.168.0.231:5251";
    }

    public class route
    {
        public static string getAlluser = $"{enums.apiUrl}/api/v1/Admin/GetAllUsers";
        public static string VerifyRegistration = $"{enums.apiUrl}/api/v1/Authorization/VerifyRegistration";
        public static string ForgotPassword = $"{enums.apiUrl}/api/v1/Authorization/ForgotPassword";
        public static string GenerateOtp = $"{enums.apiUrl}/api/v1/Authorization/GenerateOtp";
        public static string Login = $"{enums.apiUrl}/api/v1/Authorization/Login";
        public static string userDetails = $"{enums.apiUrl}/api/v1/User/userDetails";
        public static string ShowImages = $"{enums.apiUrl}/api/v1/User/ShowImages";
        public static string DeleteImage = $"{enums.apiUrl}/api/v1/User/DeleteImage";
        public static string AddImage = $"{enums.apiUrl}/api/v1/User/AddImage";
        public static string RegisterUser = $"{enums.apiUrl}/api/v1/Authorization/RegisterUser";
    }

    public enum Hobbies
    {
        Programming,
        Travel,
        Playing,
        Singing,
        Dancing
    }
}
