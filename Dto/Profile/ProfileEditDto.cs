namespace WebApplication.Dto.Profile
{
    public class ProfileEditDto
    {       
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
