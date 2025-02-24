using Microsoft.Xna.Framework;
using MinimalSquares.Components;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public (double[] x, double[] y) GetAsArrays()
        {
            double[] arrX = Points.Select(i => (double)i.X).ToArray();
            double[] arrY = Points.Select(i => (double)i.Y).ToArray();

            return (arrX, arrY);
        }

        public void Add(Vector2 point)
        {
            Points.Add(point);
            TriggerUpdate();
        }

        public void AddRange(IEnumerable<Vector2> enumerable)
        {
            Points.AddRange(enumerable);
            TriggerUpdate();
        }

        public void Clear()
        {
            Points.Clear();
            TriggerUpdate();
        }

        public void RemoveLast()
        {
            if (Points.Count == 0)
                return;

            Points.RemoveAt(Points.Count - 1);
            TriggerUpdate();
        }

        public void TriggerUpdate()
        {
            OnPointsUpdate?.Invoke();
        }
    }
}
