using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leet_Game_Of_Life.Models
{
    public class Row
    {
        private List<Cell> tempCollectionOfCells;
        
        public Row()
        {
            tempCollectionOfCells = new List<Cell>();
        }

        public void AddCellToRow(Cell cell)
        {
            tempCollectionOfCells.Add(cell);
        }
        
        public List<Cell> CollectionOfCells 
        {
            get
            {
                return tempCollectionOfCells;
            }
        }
    }
}
