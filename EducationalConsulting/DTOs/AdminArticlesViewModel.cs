namespace EducationalConsulting.DTOs
{
    public class AdminArticlesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
    }
}