using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.Graphics
{
    [Flags]
    public enum RenderRequestType
    {
        None = 0,
        Function = 1,
        Points = 2,
        General = 4,
        Static = Function | Points,
        All = Static | General,
    }
}
