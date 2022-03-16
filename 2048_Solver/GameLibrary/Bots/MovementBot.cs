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
        public MovementBot()
            : base()
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
                    board.Move(Direction.Up);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.UP);
                    UpdateBoard();
                    break;

                case Direction.Down:
                    board.Move(Direction.Down);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
                    UpdateBoard();
                    break;

                case Direction.Left:
                    board.Move(Direction.Left);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LEFT);
                    UpdateBoard();
                    break;

                case Direction.Right:
                    board.Move(Direction.Right);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
                    UpdateBoard();
                    break;
            }
        }
    }
}
