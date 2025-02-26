using Microsoft.Xna.Framework;
using MinimalSquares.Components.Abstractions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.MouseInput;

namespace MinimalSquares.Components
{
    public static class ComponentManager
    {
        public static MainView MainView { get; private set; } = null!;
        public static KeyboardManager KeyboardManager { get; private set; } = null!;
        public static MouseController MouseController { get; private set; } = null!;

        public static List<IComponent> Components { get; } = new(10);

        public static List<IUpdatedComponent> UpdateComponents { get; } = new(10);
        public static SortedSet<IDrawableComponent> DrawableComponents { get; } = new(new DrawableCompare());
        private static Dictionary<uint, IComponent> ComponentsById { get; } = new(10);

        public static void AddComponent(IComponent component)
        {
            Components.Add(component);

            if (component is IUpdatedComponent updated)
                UpdateComponents.Add(updated);
            if (component is IDrawableComponent drawable)
                DrawableComponents.Add(drawable);

            ComponentsById.Add(component.Id, component);
        }

        public static bool RemoveComponent(IComponent component)
        {
            ComponentsById.Remove(component.Id);

            if (component is IUpdatedComponent updated)
                UpdateComponents.Remove(updated);
            if (component is IDrawableComponent drawable)
                DrawableComponents.Remove(drawable);

            return Components.Remove(component);
        }

        public static void Start(MainGame game)
        {
            MainView = Get<MainView>()!;
            KeyboardManager = Get<KeyboardManager>()!;
            MouseController = Get<MouseController>()!;

            foreach (IComponent item in Components)
            {
                item.Start(game);
            }
        }

        public static void Update(GameTime gameTime)
        {
            foreach (IUpdatedComponent item in UpdateComponents)
            {
                item.Update(gameTime);
            }
        }

        public static void Draw()
        {
            foreach (IDrawableComponent item in DrawableComponents)
            {
                item.Draw();
            }
        }

        public static bool TryGetById(uint id, [MaybeNullWhen(false)] out IComponent component)
        {
            return ComponentsById.TryGetValue(id, out component);
        }

        public static IComponent? GetById(uint id)
        {
            TryGetById(id, out IComponent? component);
            return component;
        }

        public static bool TryGetById<T>(uint id, [MaybeNullWhen(false)] out T? component)
            where T : IComponent
        {
            if (ComponentsById.TryGetValue(id, out IComponent? target) && target is T converted)
            {
                component = converted;
                return true;
            }

            component = default;

            return false;
        }

        public static T? GetById<T>(uint id)
            where T : IComponent
        {
            TryGetById(id, out T? component);
            return component;
        }

        public static bool TryGet<T>([MaybeNullWhen(false)] out T? component)
            where T : IComponent
        {
            foreach (IComponent temp in Components)
            {
                if (temp is T converted)
                {
                    component = converted;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public static T? Get<T>()
            where T : IComponent
        {
            TryGet(out T? component);
            return component;
        }
    }
}
