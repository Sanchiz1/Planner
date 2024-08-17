using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models;
public class Token
{
    public required string Value { get; set; }
    public DateTime Issued { get; set; }
    public DateTime Expires { get; set; }
}
