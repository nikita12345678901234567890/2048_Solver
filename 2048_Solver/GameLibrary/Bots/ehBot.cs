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

            (int value, bool nEw)[,] second = GetBoard();

            if (second != null)
            {
                if (!BoardMatch())
                {
                    Console.WriteLine("Current grid:");
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            if (board.grid[y, x].value != 0)
                            {
                                Console.Write(board.grid[y, x].value.ToString() + " ");
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
                            if (board.prevGrid[y, x].value != 0)
                            {
                                Console.Write(board.prevGrid[y, x].value.ToString() + " ");
                            }
                            else
                            {
                                Console.Write(". ");
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("PrevMove = " + board.prevMove);

                    Console.WriteLine("Their grid:");
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            if (second[y, x].value != 0)
                            {
                                Console.Write(second[y, x].value.ToString() + " ");
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
                            if (updatedBorad[y, x].value != 0)
                            {
                                Console.Write(updatedBorad[y, x].value.ToString() + " ");
                            }
                            else
                            {
                                Console.Write(". ");
                            }
                        }
                        Console.WriteLine();
                    }
                    //End of chunk of code.

                    IEnumerable<(int value, bool nEw)> updatedEnumerable = second.Cast<(int value, bool nEw)>();
                    if (board.grid.Cast<(int value, bool nEw)>().SequenceEqual(updatedEnumerable))
                    {
                        UpdateBoard();
                        return;
                    }

                    throw new Exception("Board not match!");
                }
            }
            
            UpdateBoard();
        }
    }
}