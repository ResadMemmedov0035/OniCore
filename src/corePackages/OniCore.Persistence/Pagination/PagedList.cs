namespace OniCore.Persistence.Pagination
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList()
        {
            // TODO: this will be removed
        }

        public PagedList(IQueryable<T> query, int pageIndex = 0, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = query.Count();
            TotalPages = (int)Math.Ceiling(TotalItems / (double)pageSize);
            Items = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public PagedList(IEnumerable<T> source, int pageIndex = 0, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = source.Count();
            TotalPages = (int)Math.Ceiling(TotalItems / (double)pageSize);
            Items = source.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public int PageIndex { get; set; }

        public int PageSize { get; }

        public int TotalItems { get; }

        public int TotalPages { get; }

        public IList<T> Items { get; }

        public bool HasPrevious => PageIndex > 0;

        public bool HasNext => PageIndex < TotalPages - 1;
    }
}
