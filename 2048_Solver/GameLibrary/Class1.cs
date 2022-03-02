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
        public int[,] grid { get; /*private*/ set; }

        public int[,] prevGrid { get; /*private*/ set; }

        public Direction prevMove;

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
            //Saving the grid:
            prevGrid = grid;

            //Rotate the board so the move is left:
            switch (direction)
            {
                case Direction.Up:
                    //Rotate 90* counterclockwise
                    grid = Transpose(grid);
                    grid = ReverseColumns(grid);
                    break;

                case Direction.Down:
                    //Rotate 90* clockwise
                    grid = Transpose(grid);
                    grid = ReverseRows(grid);
                    break;

                case Direction.Left:
                    //Literally nothing
                    break;

                case Direction.Right:
                    //Rotate 108*
                    grid = ReverseRows(grid);
                    grid = ReverseColumns(grid);
                    break;
            }

            int[,] squishedGrid = new int[grid.GetLength(0), grid.GetLength(1)];

            //Squish all of the tiles to the left:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0, i = 0; x < grid.GetLength(1); x++, i++)
                {
                    while (x < grid.GetLength(1) - 1 && grid[y, x] == 0)
                    {
                        x++;
                    }
                    squishedGrid[y, i] = grid[y, x];
                }
            }
            grid = squishedGrid;

            //Do the combining:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 1; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == grid[y, x - 1] && grid[y, x] != 0)
                    {
                        Combine(new Point(x - 1, y), new Point(x, y));
                    }
                }
            }

            int[,] squishedGridV2 = new int[grid.GetLength(0), grid.GetLength(1)];

            //Squish them again:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0, i = 0; x < grid.GetLength(1); x++, i++)
                {
                    while (x < grid.GetLength(1) - 1 && grid[y, x] == 0)
                    {
                        x++;
                    }
                    squishedGridV2[y, i] = grid[y, x];
                }
            }
            grid = squishedGridV2;

            //Rotate the board back:
            switch (direction)
            {
                case Direction.Up:
                    //Rotate 90* clockwise
                    grid = Transpose(grid);
                    grid = ReverseRows(grid);
                    break;

                case Direction.Down:
                    //Rotate 90* counterclockwise
                    grid = Transpose(grid);
                    grid = ReverseColumns(grid);
                    break;

                case Direction.Left:
                    //Literally nothing
                    break;

                case Direction.Right:
                    //Rotate 108*
                    grid = ReverseRows(grid);
                    grid = ReverseColumns(grid);
                    break;
            }

            //Spawn a tile:
            //SpawnTile();

            //Save last move:
            prevMove = direction;
        }

        public int TestMoveForPoints(Direction direction)
        {
            int[,] gridBeforeMove = grid;

            int points = 0;

            //Rotate the board so the move is left:
            switch (direction)
            {
                case Direction.Up:
                    //Rotate 90* counterclockwise
                    grid = Transpose(grid);
                    grid = ReverseColumns(grid);
                    break;

                case Direction.Down:
                    //Rotate 90* clockwise
                    grid = Transpose(grid);
                    grid = ReverseRows(grid);
                    break;

                case Direction.Left:
                    //Literally nothing
                    break;

                case Direction.Right:
                    //Rotate 108*
                    grid = ReverseRows(grid);
                    grid = ReverseColumns(grid);
                    break;
            }

            int[,] squishedGrid = new int[grid.GetLength(0), grid.GetLength(1)];

            //Squish all of the tiles to the left:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0, i = 0; x < grid.GetLength(1); x++, i++)
                {
                    while (x < grid.GetLength(1) - 1 && grid[y, x] == 0)
                    {
                        x++;
                    }
                    squishedGrid[y, i] = grid[y, x];
                }
            }
            grid = squishedGrid;

            //Do the combining:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 1; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == grid[y, x - 1] && grid[y, x] != 0)
                    {
                        Combine(new Point(x - 1, y), new Point(x, y));
                        points += grid[y, x - 1];
                    }
                }
            }

            grid = gridBeforeMove;

            return points;
        }

        private int[,] Transpose(int[,] grid)
        {
            int[,] result = new int[grid.GetLength(1), grid.GetLength(0)];

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    result[x, y] = grid[y, x];
                }
            }

            return result;
        }

        private int[,] ReverseRows(int[,] grid)
        {
            int[,] result = new int[grid.GetLength(0), grid.GetLength(1)];

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    result[y, (grid.GetLength(1) - 1) - x] = grid[y, x];
                }
            }

            return result;
        }

        private int[,] ReverseColumns(int[,] grid)
        {
            int[,] result = new int[grid.GetLength(0), grid.GetLength(1)];

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    result[(grid.GetLength(0) - 1) - y, x] = grid[y, x];
                }
            }

            return result;
        }

        private void Combine(Point leftTile, Point rightTile)
        {
            if (grid[leftTile.y, leftTile.x] == grid[rightTile.y, rightTile.x])
            {
                grid[leftTile.y, leftTile.x] *= 2;
                grid[rightTile.y, rightTile.x] = 0;
            }
        }

        private void SpawnTile()
        {
            //Making a list:
            List<Point> availableSpaces = new List<Point>();

            //Getting all of the available spots:
            for (int y = 0; y < grid.GetLength(1); y++)     // for-loop!
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[y, x] == 0)
                    {
                        availableSpaces.Add(new Point(x, y));
                    }
                }
            }

            if (availableSpaces.Count == 0)
            {
                return;
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
