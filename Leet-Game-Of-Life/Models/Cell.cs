using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leet_Game_Of_Life.Models
{
    public class Cell
    {
        public Cell(int x, int y, bool isDead)
        {
            X = x;
            Y = y;
            IsDead = isDead;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDead { get; set; }
    }
}
