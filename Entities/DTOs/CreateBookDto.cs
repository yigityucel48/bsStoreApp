namespace Entities.DTOs
{
    public record CreateBookDto : ValidationBaseDto
    {
        public int Id { get; init; }
    }
}
