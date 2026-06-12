namespace EducationalConsulting.Services
{
    public interface IAuthService
    {
        bool IsAdminLoggedIn(ISession session);
        void SetAdminLogin(ISession session);
        void Logout(ISession session);
        bool ValidateAdminLogin(string username, string password);
    }
}