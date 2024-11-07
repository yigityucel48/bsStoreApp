namespace Entities.DTOs
{
    public record BookDto:ValidationBaseDto
    {
        public int Id { get; init; }
    }
}
