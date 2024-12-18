using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Components.Abstractions
{
    public interface IComponent
    {
        public string Name { get; }
        public uint Id { get; }

        public void Start(MainGame game);
    }
}
