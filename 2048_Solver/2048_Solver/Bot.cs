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

        public int[,] prevGrid;

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

            int[,] tempGrid = new int[Game1.game.grid.GetLength(0), Game1.game.grid.GetLength(1)];

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

                    tempGrid[yPos, xPos] = value;
                }
            }

            if (prevGrid == tempGrid)
            {
                return;
            }

            prevGrid = tempGrid;
            Game1.game.grid = tempGrid;
        }

        public int[,] GetBoard()
        {
            sel.IWebElement element = default;

            element = chromeDriver.FindElement(sel.By.ClassName("tile-container"));

            var children = element.FindElements(sel.By.XPath(".//*"));

            List<string> names = new List<string>();
            List<Match> matches = new List<Match>();

            int[,] tempGrid = new int[Game1.game.grid.GetLength(0), Game1.game.grid.GetLength(1)];

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
                var match = Regex.Match(names[i], @"tile tile-(\d+) tile-position-(\d)-(\d)(?! ?tile-new).*");
                //If successful, add to list:
                if (match.Success)
                {
                    matches.Add(match);

                    int value = int.Parse(match.Groups[1].Value);
                    int xPos = int.Parse(match.Groups[2].Value) - 1;
                    int yPos = int.Parse(match.Groups[3].Value) - 1;

                    tempGrid[yPos, xPos] = value;
                }
            }
            return tempGrid;
        }
    }
}