namespace Web.Models.Task;

public class CreateTaskDto
{
    public int WorkspaceId { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
