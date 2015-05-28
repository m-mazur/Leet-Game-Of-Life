using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leet_Game_Of_Life.Web.Models
{
    public class Grid
    {
        private List<Cell> cellList;

        public Grid()
        {
            cellList = new List<Cell>();
        }

        public List<Cell> CreateGrid(int column, int row)
        {

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    cellList.Add(new Cell(i, j, true));
                }
            }

            return cellList;
        }

       
    }
}