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
        private int multCounter { get; set; }
        public GrassEaterCell(int x, int y) : base(x, y)
        {
            energy = 15;
            multCounter = 0;
        }
        public void Mult(int x, int y, Cell[,] field)
        {
            multCounter++;
            if (energy >= 3 && multCounter >= 8)
            {
                IAction multAction = new MultAction();
                Creator gCreator = new GrassEaterCreator();
                bool res = multAction.DoAction(x, y, gCreator, field);
                if (res) { energy -= 3; multCounter = 0; }
            }
            else Die(x, y, field);
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
            if (energy <= 0)
            {
                IAction dieAction = new DieAction();
                dieAction.DoAction(x, y, null, field);
            }
        }
        public void Eat(int x, int y, Cell[,] field)
        {
            IAction eatAction = new EatAction();
            bool res = eatAction.DoAction(x, y, null, field);
            if (res) energy += 4;
        }
    }
    public class GrassEaterCreator : Creator
    {
        public override Cell CreateCell(int x, int y) { return new GrassEaterCell(x, y); }
    }
}
