using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leet_Game_Of_Life.Models;

namespace Leet_Game_Of_Life.Models
{
    public class Rules
    {
        private int neighborCount = 0;
        List<Cell> holdingList;
        List<Cell> initialList; //= CreateInitialList(8, 8);

        //foreach (var cell in initialList)
        //{
        //    CheckForNeighborsAndIncrementNeighborCount(initialList, cell);
        //}
        //holdingList = new List<Cell>(initialList);

        public void CheckForNeighborsAndIncrementNeighborCount(List<Cell> grid, Cell referenceCell)
        {
            holdingList = new List<Cell>(grid);

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
                else if ((referenceCell.X.Equals(cell.X + 1) && referenceCell.Y.Equals(cell.Y))) // 1
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X - 1) && referenceCell.Y.Equals(cell.Y + 1)))
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X) && referenceCell.Y.Equals(cell.Y + 1))) // 1
                {
                    CheckIfAliveNeighborExists(cell);
                }
                else if ((referenceCell.X.Equals(cell.X + 1) && referenceCell.Y.Equals(cell.Y + 1))) // 1
                {
                    CheckIfAliveNeighborExists(cell);
                }

                if (neighborCount < 2 && neighborCount > 1)
                {
                    AddCellToHoldingList(referenceCell);
                    neighborCount = 0;
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

        public List<Cell> GetHoldingList()
        {
            return holdingList;
        }
    }
}
