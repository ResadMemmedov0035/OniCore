namespace OniCore.Persistence.Dynamic
{
    public class Filter
    {
        public string Field { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Logic { get; set; }
        public IEnumerable<Filter>? Filters { get; set; }
    }
}
