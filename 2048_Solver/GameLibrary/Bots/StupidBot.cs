using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace GameLibrary
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
                    board.Move(Direction.Up);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                    break;

                case 1:
                    board.Move(Direction.Down);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    break;

                case 2:
                    board.Move(Direction.Left);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                    break;

                case 3:
                    board.Move(Direction.Right);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                    break;
            }

            UpdateBoard();
        }
    }
}