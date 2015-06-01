using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leet_Game_Of_Life.Core.Models;

namespace Leet_Game_Of_Life.Core.Logic
{
    public class RulesProcessor
    {
        private GridProcessor gridProcessor;

        public RulesProcessor()
        {
            this.gridProcessor = new GridProcessor();
        }

        public Grid CheckNeighborStateAndRunLogic(Grid snapshot)//List<Cell> snapshot)
        {
            return CheckRules(gridProcessor.CreateProcessedList(snapshot));
        }

        private Grid CheckRules (Grid grid)//List<Cell> grid) 
        {
            //var holdingList = gridProcessor.CreateHoldingGrid(grid);
            var holdingList = gridProcessor.CreateHoldingGrid(grid);
            foreach (var cell in grid.Cells)
            {
                var contextGrid = gridProcessor.CreateContextGrid(grid, cell);
                var neighborCount = 0;

                foreach (var cellFromContext in contextGrid.Cells)
                {
                    if (!cellFromContext.IsDead)
                    {
                        neighborCount++;
                    }
                }

                if (neighborCount < 2 || neighborCount > 3)
                {
                    holdingList.Cells.RemoveAt(grid.Cells.IndexOf(cell));
                    holdingList.Cells.Insert(grid.Cells.IndexOf(cell), new Cell(cell.X, cell.Y, true));
                }

                if (cell.IsDead && neighborCount == 3)
                {
                    holdingList.Cells.RemoveAt(grid.Cells.IndexOf(cell));
                    holdingList.Cells.Insert(grid.Cells.IndexOf(cell), new Cell(cell.X, cell.Y, false));
                }
            }

            return holdingList;
        }
    }
}