using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leet_Game_Of_Life.Core.Models;

namespace Leet_Game_Of_Life.Core.Logic
{
    public class GridProcessor
    {
        private Grid grid;
        private int rowCount;
        private int columnCount;

        public GridProcessor()
        {
            this.grid = new Grid();
        }

        public Grid CreateProcessedList(Grid unProcessedGrid)
        {
            rowCount = unProcessedGrid.Cells.Last().X + 1;
            columnCount = unProcessedGrid.Cells.Last().Y + 1;
            var processedList = grid.CreateGrid(columnCount, rowCount);
            foreach (var cell in processedList.Cells.Reverse<Cell>())
            {
                var newCell = unProcessedGrid.Cells.Find(tempCell => (tempCell.X.Equals(cell.X)) && (tempCell.Y.Equals(cell.Y)));
                var index = processedList.Cells.IndexOf(cell);

                if (newCell != null)
                {
                    processedList.Cells.Remove(cell);
                    processedList.Cells.Insert(index, newCell);
                }
            }

            return processedList;
        }

        public Grid CreateContextGrid(Grid processedGrid, Cell referenceCell)
        {
            var contextGrid = new Grid();

            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));

            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(referenceCell.Y)));
            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(referenceCell.Y)));

            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            contextGrid.Cells.Add(processedGrid.Cells.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));

            contextGrid.Cells.RemoveAll(tempCell => tempCell == null);

            return contextGrid;
        }

        public List<Cell> CreateHoldingGrid(List<Cell> gridCells)
        {
            return new List<Cell>(gridCells);
        }

        private int WrapEdges(int referenceCellPosition, bool isRow)
        {
            var value = isRow ? columnCount : rowCount;

            if (referenceCellPosition < 0)
            {
                referenceCellPosition += value;
            }

            if (referenceCellPosition > value - 1)
            {
                referenceCellPosition -= value;
            }

            return referenceCellPosition;
        }
    }
}