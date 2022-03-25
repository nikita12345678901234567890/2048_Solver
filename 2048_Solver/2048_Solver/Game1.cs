using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using WindowsInput;

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

        InputSimulator inputSimulator;

        //RIP botMan


        Texture2D tile;
        SpriteFont font;


        Dictionary<int, Color> squareColors;

        TimeSpan prevTime = new TimeSpan(0);
        TimeSpan elapsedTime = new TimeSpan(0);
        TimeSpan moveDelay = TimeSpan.FromMilliseconds(500);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;


            LordOfTheBots.bigYEET = true;//this sets something in botLord so that it starts existing
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            inputSimulator = new InputSimulator();

            squareColors = new Dictionary<int, Color>();

            squareColors[0] = new Color(255, 255, 255);
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
            font = Content.Load<SpriteFont>("Font");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                Exit();

            if (elapsedTime - prevTime >= moveDelay)
            {
                LordOfTheBots.MoveStupid();
            }


            Console.WriteLine(LordOfTheBots.stupidBot.gameOver);

            elapsedTime += gameTime.ElapsedGameTime;

            prevState = ks;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //spriteBatch.DrawBoard(LordOfTheBots.stupidBot.board, tile, font, squareColors);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}