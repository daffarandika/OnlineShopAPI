namespace OnlineShopAPI.Models
{
    public class UserUpload
    {
        public string Username { get; set; }

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
