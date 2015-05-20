using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leet_Game_Of_Life.Models
{
    public class Row
    {
        public int X { get; set; }
        public IEnumerable<Cell> CollectionOfCells { get; set; }
    }
}
