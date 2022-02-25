﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;

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

        public static Class1 game;

        public static Class1 testGame;

        MovementBot playerBot;
        StupidBot randomBot;
        ehBot ehBot;

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

        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            game = new Class1(4, 4);

            //playerBot = new MovementBot();
            //randomBot = new StupidBot();
            ehBot = new ehBot();


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

        /*
        tile tile-(\d+) tile-position-(\d)-(\d)

        tile tile-2 tile-position-2-1 tile-new
        tile-inner
        tile tile-4 tile-position-2-2 tile-new
        tile-inner
        */

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //playerBot.Move();

            if (Keyboard.GetState().IsKeyDown(Keys.M) && prevState.IsKeyUp(Keys.M))//(elapsedTime - prevTime >= moveDelay)
            {
                //randomBot.Move();
                ehBot.Move();
                prevTime = elapsedTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && prevState.IsKeyUp(Keys.W))
            {
                testGame.grid = new int[,] {
                    { 8, 4, 0, 0 },
                    { 4, 0, 0, 0 },
                    { 2, 0, 0, 0 },
                    { 2, 0, 0, 0 }
                };

                testGame.Move(Direction.Up);
            }

            elapsedTime += gameTime.ElapsedGameTime;

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
                    spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale, color: squareColors[game.grid[y, x]], scale: Vector2.One * scale);
                    if (game.grid[y, x] != 0)
                    {
                        var size = font.MeasureString(game.grid[y, x].ToString());

                        //spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale, color: squareColors[game.grid[y, x]], scale: Vector2.One * scale);
                        spriteBatch.DrawString(font, game.grid[y, x].ToString(), new Vector2((x * tile.Width) + tile.Width / 2, (y * tile.Height) + tile.Height / 2) * scale - size / 2, Color.Black);
                    }
                }
            }


            for (int x = 0; x < testGame.grid.GetLength(1); x++)
            {
                for (int y = 0; y < testGame.grid.GetLength(0); y++)
                {
                    spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale, color: squareColors[testGame.grid[y, x]], scale: Vector2.One * scale);
                    if (testGame.grid[y, x] != 0)
                    {
                        var size = font.MeasureString(testGame.grid[y, x].ToString());

                        //spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale, color: squareColors[game.grid[y, x]], scale: Vector2.One * scale);
                        spriteBatch.DrawString(font, testGame.grid[y, x].ToString(), new Vector2((x * tile.Width) + tile.Width / 2, (y * tile.Height) + tile.Height / 2) * scale - size / 2, Color.Black);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}