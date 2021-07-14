using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{   
    class GetNeighbours
    {
        static public Cell[] GetCells(int x, int y, Cell[,] field)
        {
            int cols = field.GetLength(0);
            int rows = field.GetLength(1);
            Cell[] ngbs = new Cell[8];
            int k = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i != 0 && j != 0 && x + i >= 0 && y + j >= 0 && x + i < cols && y + j < rows)
                    {
                        ngbs[k] = field[x + i, y + j];
                    }
                    if (i != 0 && j != 0)
                    {
                        k++;
                    }
                }
            }
            return ngbs;
        }
        //static public Cell[] GetCells(int x, int y, Cell[,] field)
        //{
        //    int cols = field.GetLength(0);
        //    int rows = field.GetLength(1);
        //    Cell[] ngbs = new Cell[]
        //        {
        //            field[(x+cols) % cols, (y+1+rows) % rows],
        //            field[(x+cols) % cols, (y-1+rows) % rows],
        //            field[(x+1+cols) % cols, (y+rows) % rows],
        //            field[(x-1+cols) % cols, (y+rows) % rows],
        //            field[(x+1+cols) % cols, (y+1+rows) % rows],
        //            field[(x-1+cols) % cols, (y-1+rows) % rows],
        //            field[(x+1+cols) % cols, (y-1+rows) % rows],
        //            field[(x-1+cols) % cols, (y+1+rows) % rows]
        //        };
        //    return ngbs;
        //}

        //static public int CountNeighbours(int x, int y, Cell[,] field)
        //{
        //    int count = 0;

        //    foreach (var i in GetCells(x, y, field))
        //    {
        //        if (i is GrassCell) count++;
        //    }
        //    return count;
        //}
    }
    public class GameEngine
    {
        private Cell[,] field;
        private readonly int rows;
        private readonly int cols;
        public uint currentGen { get; private set; }
        private Random random = new Random();
        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new Cell[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int rand = random.Next(density);
                    if (rand == 1) field[x, y] = new GrassCell(x, y);
                    else if (rand == 2) field[x, y] = new GrassEaterCell(x, y);
                    else if (rand == 3) field[x, y] = new PredatorCell(x, y);
                    else field[x, y] = new EmptyCell(x, y);
                }
            }
        }
        public void NextGen()
        {
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (field[x, y] is GrassCell)
                    {
                        GrassCell cell = (GrassCell) field[x, y];
                        cell.Mult(x, y, field);
                    }
                    else if (field[x, y] is GrassEaterCell)
                    {
                        GrassEaterCell cell = (GrassEaterCell) field[x, y];
                        cell.Eat(x, y, field);
                    }
                    else if (field[x, y] is PredatorCell)
                    {
                        PredatorCell cell = (PredatorCell)field[x, y];
                        cell.Eat(x, y, field);
                    }
                }
            }
            currentGen++;
        }
        public Cell[,] GetCurrentGen()
        {
            var res = new Cell[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    res[x, y] = field[x, y];
                }
            }
            return res;
        }
    }
}
