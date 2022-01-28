using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using System.Collections.Generic;

namespace _2048_Solver
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState prevState;

        Class1 game;

        Texture2D tile;

        Dictionary<int, Color> squareColors;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            game = new Class1(4, 4);

            squareColors = new Dictionary<int, Color>();

            squareColors[2] = new Color(238, 228, 218);
            squareColors[4] = new Color(237, 224, 200);
            squareColors[8] = new Color(242, 177, 121);
            squareColors[16] = new Color(245, 149, 99);
            squareColors[32] = new Color(246, 124, 95);
            squareColors[64] = new Color(246, 94, 59);
            squareColors[128] = new Color(237, 207, 114);
            squareColors[256] = new Color(237, 204, 97);
            squareColors[512] = new Color(237, 200, 80);
            squareColors[1024] = new Color(237, 197, 63);
            squareColors[2048] = new Color(0, 0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tile = Content.Load<Texture2D>("Tile");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up))
            {
                game.Move(Direction.Up);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down))
            {
                game.Move(Direction.Down);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left))
            {
                game.Move(Direction.Left);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right))
            {
                game.Move(Direction.Right);
            }


            prevState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            int scale = 20;
            for (int x = 0; x < game.grid.GetLength(1); x++)
            {
                for (int y = 0; y < game.grid.GetLength(0); y++)
                {
                    if(game.grid[y, x] != 0)
                    {
                        spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale, color: squareColors[game.grid[y, x]], scale: Vector2.One * scale);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
