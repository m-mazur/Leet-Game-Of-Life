using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leet_Game_Of_Life.Core;

namespace Leet_Game_Of_Life.Core.Models
{
    public class Grid
    {
        private List<Cell> cells;

        public List<Cell> Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        public Grid()
        {
            cells = new List<Cell>();
        }
         

        public /*List<Cell>*/Grid CreateGrid(int column, int row)
        {
            Grid g = new Grid();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    g.Cells.Add(new Cell(i, j, true));
                }
            }

            return g;
        }


    }
}