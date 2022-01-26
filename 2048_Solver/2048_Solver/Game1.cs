using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;

namespace _2048_Solver
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Class1 game;

        Texture2D tile;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            game = new Class1(4, 4);

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

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                game.Move(Direction.Up);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                game.Move(Direction.Down);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                game.Move(Direction.Left);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                game.Move(Direction.Right);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin();

            for (int x = 0; x < game.grid.GetLength(1); x++)
            {
                for (int y = 0; y < game.grid.GetLength(0); y++)
                {
                    spriteBatch.Draw(tile, new Vector2(0, 0), Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
