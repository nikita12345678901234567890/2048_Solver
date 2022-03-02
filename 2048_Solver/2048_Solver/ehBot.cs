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
                    Game1.game.Move(Direction.Up);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                    break;

                case 1:
                    Game1.game.Move(Direction.Down);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    break;

                case 2:
                    Game1.game.Move(Direction.Left);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                    break;

                case 3:
                    Game1.game.Move(Direction.Right);
                    element.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                    break;
            }

            int[,] second = GetBoard();

            if (second != null)
            {
                IEnumerable<int> secondEnumerable = second.Cast<int>();
                if (!(Game1.game.grid.Cast<int>().SequenceEqual(secondEnumerable)))
                {
                    Console.WriteLine("Current grid:");
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            if (Game1.game.grid[y, x] != 0)
                            {
                                Console.Write(Game1.game.grid[y, x].ToString() + " ");
                            }
                            else
                            {
                                Console.Write(". ");
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("Prev grid:");
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            if (Game1.game.prevGrid[y, x] != 0)
                            {
                                Console.Write(Game1.game.prevGrid[y, x].ToString() + " ");
                            }
                            else
                            {
                                Console.Write(". ");
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("PrevMove = " + Game1.game.prevMove);

                    Console.WriteLine("Their grid:");
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            if (second[y, x] != 0)
                            {
                                Console.Write(second[y, x].ToString() + " ");
                            }
                            else
                            {
                                Console.Write(". ");
                            }
                        }
                        Console.WriteLine();
                    }

                    //Do not delete this chunk of code under any circumstances!!! Stuff dies!!!
                    Console.WriteLine("Their grid but updated:");
                    var updatedBorad = GetBoard();
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            if (updatedBorad[y, x] != 0)
                            {
                                Console.Write(updatedBorad[y, x].ToString() + " ");
                            }
                            else
                            {
                                Console.Write(". ");
                            }
                        }
                        Console.WriteLine();
                    }
                    //End of chunk of code.

                    throw new Exception("Board not match!");
                }
            }
            
            UpdateBoard();
        }
    }
}