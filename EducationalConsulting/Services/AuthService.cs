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
            
            return username == "admin" && password == "123456";
        }

        // متد جدید برای لاگین (یکجا)
        public async Task<bool> LoginAsync(string username, string password, ISession session)
        {
            if (ValidateAdminLogin(username, password))
            {
                SetAdminLogin(session);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}