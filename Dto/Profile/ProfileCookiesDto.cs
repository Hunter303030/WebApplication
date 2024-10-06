using WebApplication.Models;

namespace WebApplication.Dto.Profile
{
    public class ProfileCookiesDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string ImageUrl { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
