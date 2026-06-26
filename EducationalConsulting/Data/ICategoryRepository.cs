using EducationalConsulting.Models;

namespace EducationalConsulting.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetByNameAsync(string name);
        bool CategoryExists(int id);
    }
}