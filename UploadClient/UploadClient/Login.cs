namespace UploadClient
{
    public class Login : ILogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
