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
    class ehBot : Bot
    {
        Random random = new Random();

        public ehBot()
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

            Direction move = Direction.Up;
            int[] points = new int[4];

            for (int i = 0; i < 4; i++)
            {
                points[i] = Game1.game.TestMoveForPoints(move);
                move++;
            }

            int largest = 0;
            for (int i = 0; i < 4; i++)
            {
                if (points[i] > points[largest])
                {
                    largest = i;
                }
            }

            if (points[largest] == 0)
            {
                largest = random.Next(4);
            }


            switch (largest)
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

            IEnumerable<int> second = GetBoard().Cast<int>();
            if (!(Game1.game.grid.Cast<int>().SequenceEqual(second)))
            {
                throw new Exception("Board not match!");
            }
            
            UpdateBoard();
        }
    }
}