using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leet_Game_Of_Life.Web.Models
{
    public class RulesProcessor
    {
        private GridProcessor gridProcessor;
        private List<Cell> grid;
        private int neighborCount;

        public RulesProcessor(List<Cell> listFromView)
        {
            this.neighborCount = 0;
            this.gridProcessor = new GridProcessor();
            this.grid = gridProcessor.CreateProcessedList(listFromView);
        }

        public List<Cell> RunCalculation()
        {


            return null;
        }

        private void CheckRules (List<Cell> grid)
        {
            foreach (var cell in initialList)
            {
                var contextGrid = CreateContextGrid(initialList, cell);

                foreach (var cellFromContext in contextGrid)
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

        private int FindRowCount(List<Cell> grid)
        {
            return grid.FindAll(tempCell => tempCell.X.Equals(0)).Count;
        }

        private int FindColumnCount(List<Cell> grid)
        {
            return grid.FindAll(tempCell => tempCell.Y.Equals(0)).Count;
        }
    }
}