using Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModels;

public class TaskViewModel
{
    public TaskDto Task { get; set; }
    public List<TagDto> Tags { get; set; }
}
