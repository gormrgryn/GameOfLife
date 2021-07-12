using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public abstract class Cell // ABSTRACT PRODUCT
    {
        public abstract int x { get; set; }
        public abstract int y { get; set; }
    }
    public abstract class Creator // ABSTRACT PRODUCT CREATOR
    {
        public abstract Cell CreateCell(int x, int y);
    }
    public class GrassCell : Cell // CONCRETE PRODUCT
    {
        public override int x { get; set; }
        public override int y { get; set; }
        public GrassCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public bool Mult(int x, int y, Cell[,] field)
        {
            MultAction mAction = new MultAction();
            Creator gCreator = new GrassCreator();
            if (GetNeighbours.CountNeighbours(x, y, field) >= 4) return mAction.DoAction(x, y, gCreator, field);
            return false;
        }
    }
    public class GrassCreator : Creator // CONCRETE PRODUCT CREATOR
    {
        public override Cell CreateCell(int x, int y) { return new GrassCell(x, y); }
    }
    public class EmptyCell : Cell // CONCRETE PRODUCT
    {
        public override int x { get; set; }
        public override int y { get; set; }
        public EmptyCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class EmptyCreator : Creator // CONCRETE PRODUCT CREATOR
    {
        public override Cell CreateCell(int x, int y) { return new EmptyCell(x, y); }
    }
    interface IAction // ACTION INTERFACE
    {
        bool DoAction(int x, int y, Creator creator, Cell[,] field);
    }
    class MultAction : IAction // ACTION CLASS
    {
        public bool DoAction(int x, int y, Creator creator, Cell[,] field)
        {
            Cell[] cells = GetNeighbours.GetCells(x, y, field);
            foreach (var i in cells)
            {
                if (i is EmptyCell)
                {
                    field[i.x, i.y] = creator.CreateCell(i.x, i.y);
                    return true;
                }
            }
            return false;
        }
    }
    class GetNeighbours
    {
        static public Cell[] GetCells(int x, int y, Cell[,] field)
        {
            int cols = field.GetLength(0);
            int rows = field.GetLength(1);
            Cell[] ngbs = new Cell[]
            {
                field[(x+cols) % cols, (y+1+rows) % rows],
                field[(x+cols) % cols, (y-1+rows) % rows],
                field[(x+1+cols) % cols, (y+rows) % rows],
                field[(x-1+cols) % cols, (y+rows) % rows],
                field[(x+1+cols) % cols, (y+1+rows) % rows],
                field[(x-1+cols) % cols, (y-1+rows) % rows],
                field[(x+1+cols) % cols, (y-1+rows) % rows],
                field[(x-1+cols) % cols, (y+1+rows) % rows]
            };
            return ngbs;
        }
        static public int CountNeighbours(int x, int y, Cell[,] field)
        {
            int count = 0;
            
            foreach (var i in GetCells(x, y, field))
            {
                if (i is GrassCell) count++;
            }
            return count;
        }
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
                    if (random.Next(density) == 0) field[x, y] = new GrassCell(x, y);
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
                        GrassCell cell = new GrassCell(x, y);
                        cell.Mult(x, y, field);
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
