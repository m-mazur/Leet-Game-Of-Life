using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leet_Game_Of_Life.Core;

namespace Leet_Game_Of_Life.Core.Models
{
    public class Cell
    {
        public Cell() { }
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