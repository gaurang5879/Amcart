namespace AuthService.API.Models
{
    public class RegisterRequest
    {
        public string FullName { get; set; }  // ✅ This is the missing property
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
