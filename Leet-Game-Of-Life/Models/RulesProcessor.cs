using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leet_Game_Of_Life.Models
{
    public class RulesProcessor
    {
        private List<Cell> initialList;
        private List<Cell> holdingList;
        private int neighborCount;

        public RulesProcessor(List<Cell> initialList)
        {
            this.initialList = initialList;
            this.holdingList = new List<Cell>(initialList);
            this.neighborCount = 0;
        }

        private List<Cell> CreateContextGrid(List<Cell> initialGrid, Cell referenceCell)
        {
            var tempList = new List<Cell>();

            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X - 1) && tempCell.Y.Equals(referenceCell.Y - 1)));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(referenceCell.Y - 1)));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X + 1) && tempCell.Y.Equals(referenceCell.Y - 1)));

            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X - 1) && tempCell.Y.Equals(referenceCell.Y)));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X + 1) && tempCell.Y.Equals(referenceCell.Y)));

            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X - 1) && tempCell.Y.Equals(referenceCell.Y + 1)));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(referenceCell.Y + 1)));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X + 1) && tempCell.Y.Equals(referenceCell.Y + 1)));

            tempList.RemoveAll(tempCell => tempCell == null);

            return tempList;
        }

        public List<Cell> CheckNeighborStateAndRunLogic()
        {
            foreach (var cell in initialList)
            {
                var contextGrid = CreateContextGrid(initialList, cell);

                foreach(var cellFromContext in contextGrid) 
                {
                    if (!cellFromContext.IsDead)
                    {
                        neighborCount++;
                    }
                }

                IfCellHasTwoOrThreeLivingNeighbors(cell);
                IfCellIsDeadAndHasThreeNeighbors(cell);

                neighborCount = 0;
            }

            return holdingList;
        }

        private void IfCellHasTwoOrThreeLivingNeighbors(Cell cell)
        {
            if (neighborCount < 2 || neighborCount > 3)
            {
                holdingList.RemoveAt(initialList.IndexOf(cell));
                holdingList.Insert(initialList.IndexOf(cell), new Cell(cell.X, cell.Y, true));
            }
        }

        private void IfCellIsDeadAndHasThreeNeighbors(Cell cell)
        {
            if (cell.IsDead && neighborCount == 3)
            {
                holdingList.RemoveAt(initialList.IndexOf(cell));
                holdingList.Insert(initialList.IndexOf(cell), new Cell(cell.X, cell.Y, false));
            }
        }
    }
}
