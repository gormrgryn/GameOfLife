using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class EmptyCell : Cell // CONCRETE PRODUCT
    {
        public EmptyCell(int x, int y) : base(x, y) { }
    }
    public class EmptyCreator : Creator // CONCRETE PRODUCT CREATOR
    {
        public override Cell CreateCell(int x, int y) { return new EmptyCell(x, y); }
    }
}
