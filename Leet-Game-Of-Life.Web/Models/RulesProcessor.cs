using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leet_Game_Of_Life.Web.Models
{
    public class RulesProcessor
    {
        private List<Cell> grid;
        private List<Cell> holdingList;
        private GridProcessor gridProcessor;

        public RulesProcessor()
        {
            this.gridProcessor = new GridProcessor();
        }

        public List<Cell> CheckNeighborStateAndRunLogic(List<Cell> snapshot)
        { 
            this.grid = gridProcessor.CreateProcessedList(snapshot);
            this.holdingList = gridProcessor.CreateHoldingGrid(grid);

            CheckRules(grid);

            return holdingList;
        }

        private void CheckRules (List<Cell> list) 
        {
            var holdingList = gridProcessor.CreateHoldingGrid(list);

            foreach (var cell in grid)
            {
                var contextGrid = gridProcessor.CreateContextGrid(grid, cell);
                var neighborCount = 0;

                foreach (var cellFromContext in contextGrid)
                {
                    if (!cellFromContext.IsDead)
                    {
                        neighborCount++;
                    }
                }

                IfCellHasTwoOrThreeLivingNeighbors(cell, neighborCount);
                IfCellIsDeadAndHasThreeNeighbors(cell, neighborCount);
            }
        }

        private void IfCellHasTwoOrThreeLivingNeighbors(Cell cell, int neighborCount)
        {
            if (neighborCount < 2 || neighborCount > 3)
            {
                holdingList.RemoveAt(grid.IndexOf(cell));
                holdingList.Insert(grid.IndexOf(cell), new Cell(cell.X, cell.Y, true));
            }
        }

        private void IfCellIsDeadAndHasThreeNeighbors(Cell cell, int neighborCount)
        {
            if (cell.IsDead && neighborCount == 3)
            {
                holdingList.RemoveAt(grid.IndexOf(cell));
                holdingList.Insert(grid.IndexOf(cell), new Cell(cell.X, cell.Y, false));
            }
        }
    }
}