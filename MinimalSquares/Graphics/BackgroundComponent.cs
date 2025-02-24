using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Graphics
{
    internal class BackgroundComponent : BaseComponent, IDrawableComponent
    {
        private MainView view = null!;

        public int Order { get; } = int.MinValue;

        public override void Start(MainGame game)
        {
            view = ComponentManager.Get<MainView>()!;
            base.Start(game);
        }

        public void Draw()
        {
            targetGame.GraphicsDevice.Clear(view.BackgroundColor);
        }

        public void UpdateVertex() { }
    }
}
