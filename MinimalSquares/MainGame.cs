using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.MouseInput;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinimalSquares;

public class MainGame : Game
{
    VertexPositionColor[] axes = new VertexPositionColor[]
    {
        new() { Color = Color.White, Position = new Vector3(-10000f, 0f, 0f) },
        new() { Color = Color.White, Position = new Vector3(10000f, 0f, 0f) },
        new() { Color = Color.White, Position = new Vector3(0f, -10000f, 0f) },
        new() { Color = Color.White, Position = new Vector3(0f, 10000f, 0f) }
    };

    public GraphicsDeviceManager GraphicsManager = null!;
    public BasicEffect Effect = null!;

    private ViewInitializing ViewSettings { get; } = new();
    private KeyboardManager keyboardManager { get; } = new();
    private MouseController MouseController { get; } = new();
    private AxesPrinter AxesPrinter { get; } = new();
    private PointManager PointWriter { get; } = new();
    private FunctionManager FunctionManager { get; } = new();
    private MainGraphicController MainGraphicController { get; } = new();

    public MainGame()
    {
        GraphicsManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        ComponentManager.Start(this);
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        ComponentManager.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            ComponentManager.Draw();
        }
        base.Draw(gameTime);
    }
}