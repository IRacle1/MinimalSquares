using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public interface IDynamicKeyEvent : IKeyEvent
    {
        public void Update(Keys key);
    }
}
