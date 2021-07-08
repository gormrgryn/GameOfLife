using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameEngine
    {
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;
        public uint currentGen { get; private set; }
        private Random random = new Random();
        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = (random.Next(density) == 0);
                }
            }
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            bool[] ngbs = new bool[8]
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
            foreach (bool i in ngbs)
            {
                if (i) count++;
            }
            return count;
        }
        public void NextGen()
        {
            var newField = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int neighboursCount = CountNeighbours(x, y);
                    bool hasLife = field[x, y];

                    if (!hasLife && neighboursCount == 3) newField[x, y] = true;
                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3)) newField[x, y] = false;
                    else newField[x, y] = field[x, y];
                }
            }
            field = newField;
            currentGen++;
        }
        public bool[,] GetCurrentGen()
        {
            var res = new bool[cols, rows];
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
