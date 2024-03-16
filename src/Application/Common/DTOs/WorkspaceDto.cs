namespace Application.Common.DTOs;

public class WorkspaceDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsPublic { get; set; }
}