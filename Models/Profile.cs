namespace WebApplication.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreate { get; set; }
        public string ImageUrl { get; set; }
    }
}
