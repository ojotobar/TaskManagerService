namespace Entities.DTOs
{
    public class Query
    {
        public string Search { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
