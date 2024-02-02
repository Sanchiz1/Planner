using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants;

public static class Roles
{
    public static string Owner = nameof(Owner); 
    public static string Member = nameof(Member);
    public static string Viewer = nameof(Viewer);
}
