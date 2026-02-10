namespace TechStore.Services
{
    public class SenhaService
    {
        public string EncriptaSenha(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerificaSenha(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
