using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// بررسی می‌کند که آیا کاربر ادمین لاگین هست یا نه
        /// </summary>
        bool IsAdminLoggedIn(ISession session);

        /// <summary>
        /// ذخیره وضعیت لاگین در Session
        /// </summary>
        void SetAdminLogin(ISession session);

        /// <summary>
        /// خروج از حساب کاربری (حذف Session)
        /// </summary>
        void Logout(ISession session);

        /// <summary>
        /// بررسی اعتبار نام کاربری و رمز عبور
        /// </summary>
        bool ValidateAdminLogin(string username, string password);

        /// <summary>
        /// عملیات کامل لاگین (اعتبارسنجی + ذخیره در Session)
        /// </summary>
        Task<bool> LoginAsync(string username, string password, ISession session);
    }
}