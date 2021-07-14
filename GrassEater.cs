using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GrassEaterCell : Cell
    {
        public int energy { get; set; }
        public GrassEaterCell(int x, int y) : base(x, y)
        {
            energy = 30;
        }
        public void Mult(int x, int y, Cell[,] field)
        {
            IAction multAction = new MultAction();
            Creator gCreator = new GrassEaterCreator();
            bool res = multAction.DoAction(x, y, gCreator, field);
            if (res) energy -= 3;
        }
        public void Move(int x, int y, Cell[,] field)
        {
            if (energy >= 1)
            {
                IAction moveAction = new MoveAction();
                bool res = moveAction.DoAction(x, y, null, field);
                if (res) energy--;
            }
            else Die(x, y, field);
        }
        public void Die(int x, int y, Cell[,] field)
        {
            IAction dieAction = new DieAction();
            dieAction.DoAction(x, y, null, field);
        }
        public void Eat(int x, int y, Cell[,] field)
        {
            ITypeAction<GrassCell> eatAction = new EatAction<GrassCell>();
            bool res = eatAction.DoAction(x, y, null, field);
            if (res)
            {
                energy += 4;
                if (energy >= 5) Mult(x, y, field);
            }
            else Move(x, y, field);
        }
    }
    public class GrassEaterCreator : Creator
    {
        public override Cell CreateCell(int x, int y) { return new GrassEaterCell(x, y); }
    }
}
