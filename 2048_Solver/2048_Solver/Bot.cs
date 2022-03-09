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
    public abstract class Bot
    {
        public sel.IWebDriver chromeDriver;

        public Bot()
        {
            
        }

        public abstract void Move();

        public void UpdateBoard()
        {
            sel.IWebElement element = default;

            element = chromeDriver.FindElement(sel.By.ClassName("tile-container"));
            
            var children = element.FindElements(sel.By.XPath(".//*"));

            List<string> names = new List<string>();
            List<Match> matches = new List<Match>();

            (int value, bool nEw)[,] tempGrid = new (int value, bool nEw)[Game1.game.grid.GetLength(0), Game1.game.grid.GetLength(1)];

            for (int i = 0; i < children.Count; i++)
            {
                int attempts = 0;
                while (attempts < 25)
                {
                    try
                    {
                        names.Add(children[i].GetAttribute("class"));
                        break;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException e)
                    {
                        children = element.FindElements(sel.By.XPath(".//*"));
                        names.Clear();
                    }
                    attempts++;
                }
                var match = Regex.Match(names[i], @"tile tile-(\d+) tile-position-(\d)-(\d)");
                //If successful, add to list:
                if (match.Success)
                {
                    matches.Add(match);

                    int value = int.Parse(match.Groups[1].Value);
                    int xPos = int.Parse(match.Groups[2].Value) - 1;
                    int yPos = int.Parse(match.Groups[3].Value) - 1;
                    
                    tempGrid[yPos, xPos] = (value, names[i].Contains("tile-new"));
                }
            }

            Game1.game.grid = tempGrid;
        }

        public (int value, bool nEw)[,] GetBoard()
        {
            sel.IWebElement element = default;

            element = chromeDriver.FindElement(sel.By.ClassName("tile-container"));

            var children = element.FindElements(sel.By.XPath(".//*"));

            List<string> names = new List<string>();
            List<Match> matches = new List<Match>();

            (int value, bool nEw)[,] tempGrid = new (int value, bool nEw)[Game1.game.grid.GetLength(0), Game1.game.grid.GetLength(1)];

            for (int i = 0; i < children.Count; i++)
            {
                int attempts = 0;
                while (attempts < 25)
                {
                    try
                    {
                        names.Add(children[i].GetAttribute("class"));
                        break;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException e)
                    {
                        children = element.FindElements(sel.By.XPath(".//*"));
                        names.Clear();
                    }
                    attempts++;
                }
                var match = Regex.Match(names[i], @"tile tile-(\d+) tile-position-(\d)-(\d)");
                //If successful, add to list:
                if (match.Success)
                {
                    matches.Add(match);

                    int value = int.Parse(match.Groups[1].Value);
                    int xPos = int.Parse(match.Groups[2].Value) - 1;
                    int yPos = int.Parse(match.Groups[3].Value) - 1;

                    tempGrid[yPos, xPos] = (value, names[i].Contains("tile-new"));
                }
            }

            return tempGrid;
        }

        public bool BoardMatch()
        {
            var webBoard = GetBoard();
            for (int y = 0; y < Game1.game.grid.GetLength(0); y++)
            {
                for (int x = 0; x < Game1.game.grid.GetLength(1); x++)
                {
                    bool ok = Game1.game.grid[y, x].value == webBoard[y, x].value || (webBoard[y, x].nEw && (Game1.game.grid[y, x].value == 0 || Game1.game.grid[y, x].nEw));
                    if(!ok)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}