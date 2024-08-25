using Domain.Common;

namespace Domain.Entities;

public class Tag : BaseEntity
{
    public int TaskId { get; private set; }
    public string Title { get; private set; }
    public Tag(int taskId, string title)
    {
        TaskId = taskId;
        Title = title;
    }
}
