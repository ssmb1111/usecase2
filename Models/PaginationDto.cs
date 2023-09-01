namespace UseCase2.Models
{
    public class PaginationDto
    {
        public int? Limit { get; set; } = 10; // default value
        public string? Offset { get; set; }
    }

}
