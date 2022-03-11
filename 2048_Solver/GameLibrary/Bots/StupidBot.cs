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

namespace _2048_Solver
{
    public class StupidBot : Bot
    {
        Random random = new Random();

        public StupidBot()
            : base()
        {
            
        }

        public override void Move()
        {
            var element = chromeDriver.FindElement(sel.By.TagName("Body"));

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
    }
}