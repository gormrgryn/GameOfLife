using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GrassCell : Cell // CONCRETE PRODUCT
    {
        private int multCount { get; set; }
        public GrassCell(int x, int y) : base(x, y)
        {
            multCount = 0;
        }
        public bool Mult(int x, int y, Cell[,] field)
        {
            multCount++;
            if (multCount >= 4)
            {
                MultAction mAction = new MultAction();
                Creator gCreator = new GrassCreator();
                bool res = mAction.DoAction(x, y, gCreator, field);
                if (res) multCount = 0;
                return res;
            }
            return false;
        }
    }
    public class GrassCreator : Creator // CONCRETE PRODUCT CREATOR
    {
        public override Cell CreateCell(int x, int y) { return new GrassCell(x, y); }
    }
}
