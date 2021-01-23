namespace WebApp1.Services
{
    public class AuthMessageSenderOptions
    {
        // Defina SendGridUser e SendGridKey com a ferramenta Secret-Manager para não expor seus segredos aqui
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}