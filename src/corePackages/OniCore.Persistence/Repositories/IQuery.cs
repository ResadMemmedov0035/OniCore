namespace OniCore.Persistence.Repositories
{
    public interface IQuery<T>
    {
        IQueryable<T> Source { get; }
    }
}
