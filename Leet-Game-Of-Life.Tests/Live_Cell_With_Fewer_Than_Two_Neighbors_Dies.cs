using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Leet_Game_Of_Life.Models;
using System.Collections.Generic;

namespace Leet_Game_Of_Life.Tests
{
    [TestClass]
    public class Live_Cell_With_Fewer_Than_Two_Neighbors_Dies
    {
        private int neighborCount = 0;
        List<Cell> holdingList;

        [TestMethod]
        public void IfCellHasLessThen2Neighbors()
        {
            //Arange
            List<Cell> initialList = CreateInitialList(8, 8);
           
            foreach (var item in initialList)  
            {
                if (item.X.Equals(2) && item.Y.Equals(2))
                {
                    item.IsDead = false;
                }
                else if (item.X.Equals(1) && item.Y.Equals(2))
                {
                    item.IsDead = false;
                }
                else if (item.X.Equals(2) && item.Y.Equals(1))
                {
                    item.IsDead = false;
                }
            }

            foreach (var cell in initialList)
            {
                CheckForNeighborsAndIncrementNeighborCount(initialList, cell);
            }

            holdingList = new List<Cell>(initialList);

            //Assert
            foreach (var cell in holdingList)
            {
                Assert.IsTrue(cell.IsDead);
            }
        }

        public void CheckForNeighborsAndIncrementNeighborCount(List<Cell> grid, Cell referenceCell)
        {
            foreach (var cell in grid)
            {
                if ((referenceCell.X.Equals(cell.X - 1) && referenceCell.Y.Equals(cell.Y - 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X) && referenceCell.Y.Equals(cell.Y - 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X + 1) && referenceCell.Y.Equals(cell.Y - 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X - 1) && referenceCell.Y.Equals(cell.Y)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X + 1) && referenceCell.Y.Equals(cell.Y)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X - 1) && referenceCell.Y.Equals(cell.Y + 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X) && referenceCell.Y.Equals(cell.Y + 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X + 1) && referenceCell.Y.Equals(cell.Y + 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }

                if (neighborCount < 2)
                {
                    AddCellToHoldingList(referenceCell);
                }
            }

        }

        public void CheckIfAliveNeighborExists(Cell cell)
        {
            if (!cell.IsDead)
            {
                neighborCount++;
            }
        }

        public List<Cell> CreateInitialList(int rows, int colummns)
        {
            List<Cell> initialList = new List<Cell>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colummns; j++)
                {
                    initialList.Add(new Cell(i, j, true));   
                }
            }

            holdingList = new List<Cell>(initialList);

            return initialList;
        }

        public void AddCellToHoldingList(Cell cell)
        {
            var index = holdingList.IndexOf(cell);
            holdingList[index].IsDead = true;
        }
    }
}
