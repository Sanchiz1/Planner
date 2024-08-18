using Application.Common.DTOs;

namespace Application.Common.ViewModels;

public class TaskViewModel
{
    public TaskDto Task { get; set; }
    public List<TagDto> Tags { get; set; }
}
