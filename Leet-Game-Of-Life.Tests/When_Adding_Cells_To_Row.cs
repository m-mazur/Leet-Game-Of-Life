using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Leet_Game_Of_Life.Models;

namespace Leet_Game_Of_Life.Tests
{
    [TestClass]
    public class When_Adding_Cells_To_Row
    {
        [TestMethod]
        public void RowShouldReturnLengthOf15()
        {
            Row tempRow = new Row();
            
            for (int i = 0; i < 15; i++)
            {
                tempRow.AddCellToRow(new Cell(i, i, true));
            }

            Assert.AreEqual(tempRow.CollectionOfCells.Count, 15);
        }
    }
}
