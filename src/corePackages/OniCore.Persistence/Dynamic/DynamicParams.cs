namespace OniCore.Persistence.Dynamic
{
    public class DynamicParams
    {
        public IEnumerable<Sort>? Sorts { get; set; }
        public Filter? Filter { get; set; }
    }
}
