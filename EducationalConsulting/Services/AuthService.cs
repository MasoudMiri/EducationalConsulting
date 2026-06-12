using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.Services
{
    public class AuthService : IAuthService
    {
        private const string SessionKey = "AdminLoggedIn";

        public bool IsAdminLoggedIn(ISession session)
        {
            return session.GetString(SessionKey) == "true";
        }

        public void SetAdminLogin(ISession session)
        {
            session.SetString(SessionKey, "true");
        }

        public void Logout(ISession session)
        {
            session.Remove(SessionKey);
        }

        public bool ValidateAdminLogin(string username, string password)
        {
            // TODO: بعداً از دیتابیس بخون
            return username == "admin" && password == "123456";
        }
    }
}