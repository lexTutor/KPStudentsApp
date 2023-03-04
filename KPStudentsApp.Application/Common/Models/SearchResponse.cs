﻿namespace KPStudentsApp.Application.Common.Models
{
    public class SearchResponse<T>
    {
        public IList<T> Data { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public long TotalCount { get; set; }
        public long PageCount { get; internal set; }
    }
}
