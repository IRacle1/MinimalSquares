using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Components
{
    public class BaseComponent : IComponent
    {
        public BaseComponent()
        {
            ComponentManager.AddComponent(this);
        }

        private static uint idGen = 0;

        public string Name => GetType().Name;

        public uint Id { get; } = idGen++;

        protected MainGame targetGame = null!;

        public virtual void Start(MainGame game)
        {
            targetGame = game;
        }

        public virtual void Remove()
        {
            ComponentManager.RemoveComponent(this);
        }
    }
}
