using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leet_Game_Of_Life.Models
{
    public class Grid
    {
        private List<Row> tempGrid;
        
        public Grid()
        {
            tempGrid = new List<Row>();
        }

        public void AddRowToGrid(Row row)
        {
            tempGrid.Add(row);
        }

        public List<Row> CollectionOfRows {
            get 
            {
                return tempGrid;
            }
        }
    }
}
