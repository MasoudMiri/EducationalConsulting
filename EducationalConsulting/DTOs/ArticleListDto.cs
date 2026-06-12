namespace EducationalConsulting.DTOs
{
    public class ArticleListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public int ViewCount { get; set; }
        public string CategoryName { get; set; }
    }
}