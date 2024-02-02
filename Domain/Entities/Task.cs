using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Task : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime? StartDate { get; private set; } = null;
    public DateTime EndDate { get; private set; }
    public List<Tag> Tags { get; private set; }

    public Task(string title, string description, DateTime? startDate, DateTime endDate) { 
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Tags = new List<Tag>();
    }

    public void AddTag(Tag tag)
    {
        if (Tags.Contains(tag)) return;
        Tags.Add(tag);
    }

    public void UpdateTask(string title, string description, DateTime? startDate, DateTime endDate)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }
}