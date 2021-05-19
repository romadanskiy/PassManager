namespace PasswordManager.ViewModels
{
    public class ExportImportCredentialViewModel
    {
        public string Source { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ExportImportCredentialViewModel()
        { }

        public ExportImportCredentialViewModel(string source, string login, string password)
        {
            Source = source;
            Login = login;
            Password = password;
        }
    }
}