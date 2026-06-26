namespace EducationalConsulting.DTOs
{
    public class ArticleDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }        
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ViewCount { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }          
    }
}