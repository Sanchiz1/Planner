using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Tag : BaseEntity
{
    public int TaskId { get; set; }
    public string Title {  get; set; } = string.Empty;
}
