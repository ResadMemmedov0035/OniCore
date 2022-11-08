namespace OniCore.Persistence.Pagination
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return new PagedList<T>(query, pageIndex, pageSize);
        }

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, PageParams @params)
        {
            return new PagedList<T>(query, @params.Index, @params.Size);
        }

        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, PageParams @params)
        {
            return new PagedList<T>(source, @params.Index, @params.Size);
        }
    }
}
