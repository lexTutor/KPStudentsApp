using AutoMapper;
using KPStudentsApp.Application.Common.Models;

namespace KPStudentsApp.Application.Common
{
    public static class PaginationHelper
    {
        public static SearchResponse<TRes> Paginate<TReq, TRes>(IQueryable<TReq> request, IMapper mapper, int page, int pageSize, long total)
        {
            page = page <= 1 ? 1 : page;
            var skipTake = SkipTake(pageSize, page);

            var result = new SearchResponse<TRes>
            {
                Data = mapper.Map<IList<TRes>>(request.Skip(skipTake.Item1)).Take(skipTake.Item2).ToList(),
                Page = page,
                PageSize = pageSize == 0 ? 10 : pageSize,
                TotalCount = total,
                PageCount = PageCount(total, skipTake.Item2)
            };

            return result;
        }

        public static SearchResponse<TRes> Paginate<TRes>(IList<TRes> items, int page, int pageSize, long total)
        {
            page = page <= 1 ? 1 : page;
            var skipTake = SkipTake(pageSize, page);

            var result = new SearchResponse<TRes>
            {
                Data = items,
                Page = page,
                PageSize = pageSize == 0 ? 10 : pageSize,
                TotalCount = total,
                PageCount = PageCount(total, skipTake.Item2)
            };

            return result;
        }

        public static (int, int) SkipTake(int pageSize, int page)
        {
            int take = pageSize > 0 ? pageSize : 10;
            int skip = page == 0 ? 0 : (page - 1) * take;

            return (skip, take);
        }

        public static long PageCount(long total, int take) => total % take == 0 ? total / take : (total / take) + 1;
    }
}
