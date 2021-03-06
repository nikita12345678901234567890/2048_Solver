using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameLibrary
{
    public class MovementBot : Bot
    {
        public MovementBot(bool connect)
            : base(connect)
        {
            
        }

        public override void Move()
        {
            UpdateBoard();
        }

        public void Move(Direction move)
        {
            var element = chromeDriver.FindElement(sel.By.TagName("Body"));

            switch (move)
            {
                case Direction.Up:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                    UpdateBoard();
                    break;

                case Direction.Down:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    UpdateBoard();
                    break;

                case Direction.Left:
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                    UpdateBoard();
                    break;

                case Direction.Right:
                    element.SendKeys(OpenQA.Selenium.Keys.Right);
                    UpdateBoard();
                    break;
            }
        }

        public override void MoveLocal()
        { 
            //bigYEET
        }
    }
}
