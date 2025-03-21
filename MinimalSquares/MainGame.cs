﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.MouseInput;

namespace MinimalSquares;

public class MainGame : Game
{
    public GraphicsDeviceManager GraphicsManager = null!;
    public BasicEffect Effect = null!;

    private MainView MainView { get; } = new();
    private KeyboardManager keyboardManager { get; } = new();
    private MouseController MouseController { get; } = new();
    private AxesPrinter AxesPrinter { get; } = new();
    private PointManager PointWriter { get; } = new();
    private FunctionManager FunctionManager { get; } = new();
    private FunctionsGraphic FunctionsGraphic { get; } = new();
    private PointPrinter PointPrinter { get; } = new();
    private DeltaPrinter DeltaPrinter { get; } = new();
    private DefaultKeybinds DefaultKeybinds { get; } = new();
    private BackgroundComponent BackgroundComponent { get; } = new();
    private MainInputHandler Move { get; } = new();
    private InternalManager InternalManager { get; } = new();
    private MovingKeyboard MovingKeyboard { get; } = new();
    private ReportManager InfoManager { get; } = new();

    public MainGame()
    {
        GraphicsManager = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        ComponentManager.Start(this);
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        ComponentManager.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            ComponentManager.Draw();
        }

        base.Draw(gameTime);
    }
}
