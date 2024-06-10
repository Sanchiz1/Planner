namespace Web.Models.Task;

public class UpdateTaskDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
