namespace OniCore.Persistence.Pagination
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IPagedList<T> pagedList = new PagedList<T>(source, pageIndex, pageSize);
            return await Task.FromResult(pagedList).ConfigureAwait(false);
        }
    }
}
