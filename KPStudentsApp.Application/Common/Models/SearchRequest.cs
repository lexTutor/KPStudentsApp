namespace KPStudentsApp.Application.Common.Models
{
    public class SearchRequest<T>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
