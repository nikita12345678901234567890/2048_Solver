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
            UpdateBoard();
        }

        public override void Move()
        {
            var element = chromeDriver.FindElement(sel.By.TagName("Body"));

            if (Keyboard.GetState().IsKeyDown(Keys.M) && prevState.IsKeyUp(Keys.M))
            {
                Game1.game.Move(Direction.Up);//this not really nessesary
                element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                UpdateBoard();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down))
            {
                Game1.game.Move(Direction.Down);
                element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                UpdateBoard();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left))
            {
                Game1.game.Move(Direction.Left);
                element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                UpdateBoard();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right))
            {
                Game1.game.Move(Direction.Right);
                element.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                UpdateBoard();
            }

            prevState = Keyboard.GetState();
        }

        /*
         var element = chromeDriver.FindElement(sel.By.TagName("Body"));

            if (Keyboard.GetState().IsKeyDown(Keys.M) && prevState.IsKeyUp(Keys.M))
            {
                int fish = random.Next(4);
                switch (fish)
                {
                    case 0:
                        Game1.game.Move(Direction.Up);//this not really nessesary
                        element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                        break;

                    case 1:
                        Game1.game.Move(Direction.Down);//this not really nessesary
                        element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                        break;

                    case 2:
                        Game1.game.Move(Direction.Left);//this not really nessesary
                        element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                        break;

                    case 3:
                        Game1.game.Move(Direction.Right);//this not really nessesary
                        element.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                        break;
                }
                
                UpdateBoard();
            }

            prevState = Keyboard.GetState();
         */
    }
}