using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class PredatorCell : Cell
    {
        public int energy { get; set; }
        public PredatorCell(int x, int y) : base(x, y)
        {
            energy = 50;
        }
        public void Mult(int x, int y, Cell[,] field)
        {
            IAction multAction = new MultAction();
            Creator gCreator = new PredatorCreator();
            bool res = multAction.DoAction(x, y, gCreator, field);
            if (res) energy -= 6;
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
            IAction eatAction = new EatAction();
            bool res = eatAction.DoAction(x, y, null, field);
            if (res)
            {
                energy += 5;
                if (energy >= 6) Mult(x, y, field);
            }
            else Move(x, y, field);

        }
    }
    public class PredatorCreator : Creator
    {
        public override Cell CreateCell(int x, int y) { return new GrassEaterCell(x, y); }
    }
}
