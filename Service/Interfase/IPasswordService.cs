namespace WebApplication.Service.Interfase
{
    public interface IPasswordService
    {
        public string Hash(string password);
        public bool Verify(string password, string hashedPassword);
    }
}
