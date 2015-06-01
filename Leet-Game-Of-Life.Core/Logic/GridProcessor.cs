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

        public Grid CreateProcessedList(Grid unProcessedGrid)//List<Cell> unProcessedGrid)
        {
            //var processedList = grid.CreateGrid(14, 34);
            rowCount = unProcessedGrid.Cells.Last().X + 1;
            columnCount = unProcessedGrid.Cells.Last().Y + 1;
            var processedList = grid.CreateGrid(rowCount, columnCount);
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

        public Grid/*List<Cell>*/ CreateContextGrid(Grid/*List<Cell>*/ processedGrid, Cell referenceCell)
        {
            //SetRowAndColumnCount(processedGrid);

            var contextGrid = new Grid();//List<Cell>();

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

        public Grid CreateHoldingGrid(Grid/*List<Cell>*/ grid) 
        {
            return new Grid(grid);
        }

        private void SetRowAndColumnCount(List<Cell> processedGrid)
        {
            //rowCount = processedGrid.FindAll(tempCell => tempCell.X.Equals(0)).Count;
            //columnCount = processedGrid.FindAll(tempCell => tempCell.Y.Equals(0)).Count; ;
        }

        private int WrapEdges(int referenceCellPosition, bool isRow)
        {
            var value = isRow ? rowCount : columnCount;

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