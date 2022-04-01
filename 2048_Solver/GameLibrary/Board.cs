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

    public class Board
    {
        public (int value, bool nEw)[,] grid { get; /*private*/ set; }

        public (int value, bool nEw)[,] prevGrid { get; /*private*/ set; }

        public Direction prevMove;

        Random random = new Random();

        public int score = 0;

        public bool gameOver = false;

        public Board(int gridWidth, int gridHeight)
        {
            grid = new (int value, bool nEw)[gridHeight, gridWidth];

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    grid[y, x].value = 0;
                }
            }

            SpawnTile();
            SpawnTile();
        }

        public void ResetBoard()
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x].value = 0;
                }
            }

            SpawnTile();
            SpawnTile();

            score = 0;

            gameOver = false;
        }

        public void Move(Direction direction, bool spawn)
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

            (int value, bool nEw)[,] squishedGrid = new (int value, bool nEw)[grid.GetLength(0), grid.GetLength(1)];

            //Squish all of the tiles to the left:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0, i = 0; x < grid.GetLength(1); x++, i++)
                {
                    while (x < grid.GetLength(1) - 1 && grid[y, x].value == 0)
                    {
                        x++;
                    }
                    squishedGrid[y, i] = grid[y, x];
                    squishedGrid[y, i].nEw = false;//This very important!!!
                }
            }
            grid = squishedGrid;

            //Do the combining:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 1; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == grid[y, x - 1] && grid[y, x].value != 0)
                    {
                        Combine(new Point(x - 1, y), new Point(x, y));
                        score += grid[y, x - 1].value;
                    }
                }
            }

            (int value, bool nEw)[,] squishedGridV2 = new (int value, bool nEw)[grid.GetLength(0), grid.GetLength(1)];

            //Squish them again:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0, i = 0; x < grid.GetLength(1); x++, i++)
                {
                    while (x < grid.GetLength(1) - 1 && grid[y, x].value == 0)
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
            if (spawn)
            {
                SpawnTile();
            }

            //Save last move:
            prevMove = direction;
        }

        public int TestMoveForPoints(Direction direction)
        {
            (int value, bool nEw)[,] gridBeforeMove = grid;

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

            (int value, bool nEw)[,] squishedGrid = new (int value, bool nEw)[grid.GetLength(0), grid.GetLength(1)];

            //Squish all of the tiles to the left:
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0, i = 0; x < grid.GetLength(1); x++, i++)
                {
                    while (x < grid.GetLength(1) - 1 && grid[y, x].value == 0)
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
                    if (grid[y, x] == grid[y, x - 1] && grid[y, x].value != 0)
                    {
                        Combine(new Point(x - 1, y), new Point(x, y));
                        points += grid[y, x - 1].value;
                    }
                }
            }

            grid = gridBeforeMove;

            return points;
        }

        private (int value, bool nEw)[,] Transpose((int value, bool nEw)[,] grid)
        {
            (int value, bool nEw)[,] result = new (int value, bool nEw)[grid.GetLength(1), grid.GetLength(0)];

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    result[x, y] = grid[y, x];
                }
            }

            return result;
        }

        private (int value, bool nEw)[,] ReverseRows((int value, bool nEw)[,] grid)
        {
            (int value, bool nEw)[,] result = new (int value, bool nEw)[grid.GetLength(0), grid.GetLength(1)];

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    result[y, (grid.GetLength(1) - 1) - x] = grid[y, x];
                }
            }

            return result;
        }

        private (int value, bool nEw)[,] ReverseColumns((int value, bool nEw)[,] grid)
        {
            (int value, bool nEw)[,] result = new (int value, bool nEw)[grid.GetLength(0), grid.GetLength(1)];

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
                grid[leftTile.y, leftTile.x] = (grid[leftTile.y, leftTile.x].value * 2, false);
                grid[rightTile.y, rightTile.x] = (0, false);
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
                    if (grid[y, x].value == 0)
                    {
                        availableSpaces.Add(new Point(x, y));
                    }
                }
            }

            if (availableSpaces.Count == 0)
            {
                gameOver = true;
                return;
            }

            //Getting a random number:
            int number = random.Next(0, availableSpaces.Count);

            //Choosing a spot:
            Point ChosenSpot = availableSpaces[number];

            if (number % 3 == 0)
            {
                grid[ChosenSpot.y, ChosenSpot.x].value = 4;
            }
            else
            {
                grid[ChosenSpot.y, ChosenSpot.x].value = 2;
            }
        }
    }
}
