using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.Input.Keyboard
{
    [Flags]
    public enum InputType
    {
        None = 0,
        OnKeyDown = 1,
        OnKeyUp = 2,
        All = OnKeyDown | OnKeyUp,
    }
}
