namespace EducationalConsulting.DTOs
{
    public class PaginationDto
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}