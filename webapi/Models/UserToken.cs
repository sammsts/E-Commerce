namespace Ecommerce.API.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string? UserImgProfile { get; set; }
    }
}
