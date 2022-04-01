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

        public StupidBot(bool connect)
            : base(connect)
        {
            
        }

        public override void Move()
        {
            var element = chromeDriver.FindElement(sel.By.TagName("Body"));

            int fish = random.Next(4);
            switch (fish)
            {
                case 0:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                    break;

                case 1:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    break;

                case 2:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                    break;

                case 3:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                    break;
            }

            UpdateBoard();
        }

        public override void MoveLocal()
        {
            int fish = random.Next(4);
            switch (fish)
            {
                case 0:
                    board.Move(Direction.Up, true);
                    break;

                case 1:
                    board.Move(Direction.Down, true);
                    break;
                case 2:
                    board.Move(Direction.Left, true);
                    break;

                case 3:
                    board.Move(Direction.Right, true);
                    break;
            }

            if (board.score > highScore)
            {
                highScore = board.score;
            }
            if (board.gameOver)
            {
                gameNumber++;
                board.ResetBoard();
            }
        }
    }
}