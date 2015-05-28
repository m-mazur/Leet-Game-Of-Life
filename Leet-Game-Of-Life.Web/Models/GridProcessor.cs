using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leet_Game_Of_Life.Web.Models
{
    public class GridProcessor
    {
        public GridProcessor()
        {

        }

        public List<Cell> CreateProcessedList(List<Cell> initialGrid)
        {
            List<Cell> processedGrid = new Grid().CreateGrid(15, 40);

            foreach (var cell in processedGrid.Reverse<Cell>())
            {
                var initialCell = initialGrid.Find(tempCell => (tempCell.X.Equals(cell.X)) && (tempCell.Y.Equals(cell.Y)));
                var indexOfCell = processedGrid.IndexOf(cell);

                if (initialCell != null)
                {
                    processedGrid.Remove(cell);
                    processedGrid.Insert(indexOfCell, initialCell);
                }
            }

            return processedGrid;
        }

        public List<Cell> CreateContextGrid(List<Cell> grid, Cell referenceCell)
        {
            var tempList = new List<Cell>();

            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));

            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(referenceCell.Y)));
            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(referenceCell.Y)));

            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            tempList.Add(grid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));

            tempList.RemoveAll(tempCell => tempCell == null);

            return tempList;
        }

        private int WrapEdges(int referenceCellPosition, bool isRow)
        {
            var value = 0;
            value = isRow ? rowCount : columnCount;

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