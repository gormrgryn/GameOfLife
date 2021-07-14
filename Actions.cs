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
            foreach (var i in cells)
            {
                if (i is EmptyCell)
                {
                    field[i.x, i.y] = field[x, y];
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
    class EatAction : IAction
    {
        public bool DoAction(int x, int y, Creator creator, Cell[,] field)
        {
            Cell[] cells = GetNeighbours.GetCells(x, y, field);
            foreach (var i in cells)
            {
                if (i is GrassCell)
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
