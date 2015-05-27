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

        public List<Cell> CheckNeighborStateAndRunLogic(List<Cell> snapshot)
        {
            return CheckRules(gridProcessor.CreateProcessedList(snapshot));
        }

        private List<Cell> CheckRules (List<Cell> grid) 
        {
            var holdingList = gridProcessor.CreateHoldingGrid(grid);

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

                if (neighborCount < 2 || neighborCount > 3)
                {
                    holdingList.RemoveAt(grid.IndexOf(cell));
                    holdingList.Insert(grid.IndexOf(cell), new Cell(cell.X, cell.Y, true));
                }

                if (cell.IsDead && neighborCount == 3)
                {
                    holdingList.RemoveAt(grid.IndexOf(cell));
                    holdingList.Insert(grid.IndexOf(cell), new Cell(cell.X, cell.Y, false));
                }
            }

            return holdingList;
        }
    }
}