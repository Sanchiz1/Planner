using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Workspace : BaseEntity
{
    public string Name { get; private set; }
    public List<Task> Tasks { get; private set; }
    public Workspace(string name)
    {
        Name = name;
        Tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        if (Tasks.Contains(task)) return;
        Tasks.Add(task);
    }
}
