using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    interface IAction // ACTION INTERFACE
    {
        bool DoAction(int x, int y, Creator creator, Cell[,] field);
    }
    interface ITypeAction<T> // ACTION INTERFACE
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
                    Cell newCell = creator.CreateCell(i.x, i.y);
                    field[i.x, i.y] = newCell;
                    return true;
                }
            }
            return false;
        }
    }
    class MoveAction : IAction
    {
        public bool DoAction(int x, int y, Creator creator, Cell[,] field)
        {
            Cell[] cells = GetNeighbours.GetCells(x, y, field);
            cells = cells.Where(c => c is EmptyCell).ToArray();
            if (cells.Length != 0)
            {
                Random random = new Random();
                int rnd = random.Next(cells.Length);
                Cell cell = cells[rnd];
                if (cell != null)
                {
                    field[cell.x, cell.y] = field[x, y];
                    field[x, y] = new EmptyCell(x, y);
                    return true;
                }
            }
            return false;
        }
    }
    class DieAction : IAction
    {
        public bool DoAction(int x, int y, Creator creator, Cell[,] field)
        {
            field[x, y] = new EmptyCell(x, y);
            return true;
        }
    }
    class EatAction<T> : ITypeAction<T>
    {
        public bool DoAction(int x, int y, Creator creator, Cell[,] field)
        {
            Cell[] cells = GetNeighbours.GetCells(x, y, field);
            foreach (var i in cells)
            {
                if (i is T)
                {
                    field[i.x, i.y] = field[x, y];
                    field[x, y] = new EmptyCell(x, y);
                    return true;
                }
            }
            return false;
        }
    }
}
