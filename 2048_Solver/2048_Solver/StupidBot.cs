using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2048_Solver
{
    public class StupidBot : Bot
    {

        KeyboardState prevState;

        public StupidBot()
        {
            chromeDriver = new sel.Chrome.ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = "https://play2048.co/"
            };
        }

        public override void Move()
        {
            var element = chromeDriver.FindElement(sel.By.ClassName("tile-container"));

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up))
            {
                Game1.game.Move(Direction.Up);
                UpdateBoard();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down))
            {
                Game1.game.Move(Direction.Down);
                UpdateBoard();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left))
            {
                Game1.game.Move(Direction.Left);
                UpdateBoard();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right))
            {
                Game1.game.Move(Direction.Right);
                UpdateBoard();
            }

            prevState = Keyboard.GetState();
        }
    }
}