using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace GameLibrary
{
    public class ehBot : Bot
    {
        Random random = new Random();

        public ehBot()
            : base()
        {
            

        }

        public override void Move()
        {
            var element = chromeDriver.FindElement(sel.By.TagName("Body"));

            Direction move = Direction.Up;
            int[] points = new int[4];

            for (int i = 0; i < 4; i++)
            {
                points[i] = board.TestMoveForPoints(move);
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