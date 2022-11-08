namespace OniCore.Persistence.Pagination
{
    /// <summary>
    /// Implementation of IPagedList<T>. This class provides binding/mapping object for PagedList<T>.
    /// </summary>
    public class PagedListDTO<T> : IPagedList<T>
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public IList<T> Items { get; init; } = null!;

        public bool HasPrevious { get; init; }

        public bool HasNext { get; init; }
    }
}
