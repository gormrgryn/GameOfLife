using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public abstract class Cell // ABSTRACT PRODUCT
    {
        public int x { get; set; }
        public int y { get; set; }
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public abstract class Creator // ABSTRACT PRODUCT CREATOR
    {
        public abstract Cell CreateCell(int x, int y);
    }
}
