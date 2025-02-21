using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Components;
using MinimalSquares.Graphics;
using Microsoft.Xna.Framework;

namespace MinimalSquares.Generic
{
    public class PointManager : BaseComponent
    {
        public List<Vector2> Points { get; } = new List<Vector2>();

        public event Action? OnPointsUpdate;

        public override void Start(MainGame game)
        {
            base.Start(game);
        }

        public void SetNewPoint(Vector2 point, bool updateVertex)
        {
            Points.Add(point);

            if (updateVertex)
                TriggerUpdate();
        }

        public void TriggerUpdate()
        {
            OnPointsUpdate?.Invoke();
        }
    }
}
