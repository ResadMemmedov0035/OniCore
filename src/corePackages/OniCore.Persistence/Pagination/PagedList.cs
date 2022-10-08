namespace OniCore.Persistence.Pagination
{
    public class PagedList<T> : IPagedList<T>
    {
        #region Ctors

        public PagedList() 
        {
            Items = new List<T>();
        }

        public PagedList(IEnumerable<T> enumerable, int pageIndex, int pageSize)
            : this(enumerable.Skip(pageIndex * pageSize).Take(pageSize).ToList(), pageIndex, pageSize)
        {
        }

        public PagedList(IQueryable<T> queryable, int pageIndex, int pageSize)
            : this(queryable.Skip(pageIndex * pageSize).Take(pageSize).ToList(), pageIndex, pageSize)
        {
        }

        private PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = items.Count;
            TotalPages = (int)Math.Ceiling(TotalItems / (double)pageSize);
            Items = items;
            HasPrevious = PageIndex > 0;
            HasNext = PageIndex < TotalPages - 1;
        }
        #endregion

        #region Props

        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public IList<T> Items { get; init; }

        public bool HasPrevious { get; init; }

        public bool HasNext { get; init; }
        #endregion
    }
}
