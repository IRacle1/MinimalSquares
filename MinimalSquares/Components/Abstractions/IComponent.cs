namespace MinimalSquares.Components.Abstractions
{
    public interface IComponent
    {
        public string Name { get; }
        public uint Id { get; }

        public void Start(MainGame game);
    }
}
