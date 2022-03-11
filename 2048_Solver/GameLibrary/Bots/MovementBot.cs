using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2048_Solver
{
    public class MovementBot : Bot
    {
        public MovementBot()
            : base()
        {
            
        }

        public override void Move(Direction move)
        {
            var element = chromeDriver.FindElement(sel.By.TagName("Body"));

            switch (move)
            {
                case Direction.Up:
                    board.Move(Direction.Up);//this not really nessesary
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                    UpdateBoard();
                    break;

                case Direction.Down:
                    board.Move(Direction.Down);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    UpdateBoard();
                    break;

                case Direction.Left:
                    board.Move(Direction.Left);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                    UpdateBoard();
                    break;

                case Direction.Right:
                    board.Move(Direction.Right);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                    UpdateBoard();
                    break;
            }
        }
    }
}
