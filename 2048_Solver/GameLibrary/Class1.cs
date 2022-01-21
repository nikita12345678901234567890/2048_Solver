using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Class1
    {
        int gridWidth;
        int gridHeight;
        public int[,] grid { get; private set; }

        Random random = new Random();

        public Class1(int gridWidth, int gridHeight)
        {
            grid = new int[gridHeight, gridWidth];

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    grid[y, x] = 0;
                }
            }
        }

        public void Move(Direction direction)
        {
            //Rotate the board so the move is up:


            //Do the move:
            for (int y = 0; y < gridHeight - 1; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if (grid[y, x] == grid[y + 1, x])
                    {
                        Combine(new Point(x, y), new Point(x, y + 1));
                    }
                }
            }

            //Rotate the board back:

        }

        private void Combine(Point topTile, Point bottomTile)
        {
            if (grid[topTile.y, topTile.x] == grid[bottomTile.y, bottomTile.x])
            {
                grid[topTile.y, topTile.x] *= 2;
                grid[bottomTile.y, bottomTile.x] = 0;
            }
        }

        private void SpawnTile()
        {
            List<Point> availableSpaces = new List<Point>();

            //Getting all of the available spots:
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if (grid[y, x] == 0)
                    {
                        availableSpaces.Add(new Point(x, y));
                    }
                }
            }

            //Getting a random number:
            int number = random.Next(0, availableSpaces.Count);

            //Choosing a spot:
            Point ChosenSpot = availableSpaces[number];

            if (number % 3 == 0)
            {
                grid[ChosenSpot.y, ChosenSpot.x] = 4;
            }
            else
            {
                grid[ChosenSpot.y, ChosenSpot.x] = 2;
            }
        }
    }
}
