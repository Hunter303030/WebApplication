namespace WebApplication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreate { get; set; }
        public string ImageUrl { get; set; }
        public int Role_Id { get; set; }

        public Role Role { get; set; }
    }
}
