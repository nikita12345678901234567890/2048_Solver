using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Text.RegularExpressions;
using WindowsInput;
using OpenQA.Selenium.Chrome;
using System;

namespace GameLibrary
{
    public abstract class Bot
    {
        public sel.IWebDriver chromeDriver;

        protected InputSimulator inputSimulator;

        public Board board;

        public bool gameOver = false;

        public Bot()
        {
            inputSimulator = new InputSimulator();

            board = new Board(4, 4);

            ChromeOptions options = new ChromeOptions()
            {
                PageLoadStrategy = sel.PageLoadStrategy.None
            };

            chromeDriver = new sel.Chrome.ChromeDriver(Directory.GetCurrentDirectory(), options)
            {
                Url = "https://play2048.co/"
            };

            UpdateBoard();
        }

        public abstract void Move();

        public void UpdateBoard()
        {
            sel.IWebElement element = default;

            try
            {
                element = chromeDriver.FindElement(sel.By.ClassName("tile-container"));
            }
            catch(Exception ex)
            {
                return;
            }

            var children = element.FindElements(sel.By.XPath(".//*"));

            List<string> names = new List<string>();
            List<Match> matches = new List<Match>();

            (int value, bool nEw)[,] tempGrid = new (int value, bool nEw)[board.grid.GetLength(0), board.grid.GetLength(1)];

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

            board.grid = tempGrid;

            gameOver = CheckGameOver();
        }

        public (int value, bool nEw)[,] GetBoard()
        {
            sel.IWebElement element = default;

            element = chromeDriver.FindElement(sel.By.ClassName("tile-container"));

            var children = element.FindElements(sel.By.XPath(".//*"));

            List<string> names = new List<string>();
            List<Match> matches = new List<Match>();

            (int value, bool nEw)[,] tempGrid = new (int value, bool nEw)[board.grid.GetLength(0), board.grid.GetLength(1)];

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
            for (int y = 0; y < board.grid.GetLength(0); y++)
            {
                for (int x = 0; x < board.grid.GetLength(1); x++)
                {
                    bool ok = board.grid[y, x].value == webBoard[y, x].value || (webBoard[y, x].nEw && (board.grid[y, x].value == 0 || board.grid[y, x].nEw));
                    if(!ok)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckGameOver()
        {
            sel.IWebElement element = default;

            element = chromeDriver.FindElement(sel.By.ClassName("game-container"));

            var children = element.FindElements(sel.By.XPath(".//*"));

            List<string> names = new List<string>();
            List<Match> matches = new List<Match>();

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
                var match = Regex.Match(names[i], @"game-message game-over");
                //If successful, add to list:
                if (match.Success)
                {
                    matches.Add(match);

                    return true;
                }
            }

            return false;
        }
    }
}