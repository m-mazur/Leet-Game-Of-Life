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

        public List<Cell> CreateProcessedList(List<Cell> unProcessedGrid)
        {
            var processedList = grid.CreateGrid(15, 40);

            foreach (var cell in processedList.Reverse<Cell>())
            {
                var newCell = unProcessedGrid.Find(tempCell => (tempCell.X.Equals(cell.X)) && (tempCell.Y.Equals(cell.Y)));
                var index = processedList.IndexOf(cell);

                if (newCell != null)
                {
                    processedList.Remove(cell);
                    processedList.Insert(index, newCell);
                }
            }

            return processedList;
        }

        public List<Cell> CreateContextGrid(List<Cell> processedGrid, Cell referenceCell)
        {
            SetRowAndColumnCount(processedGrid);

            var contextGrid = new List<Cell>();

            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));

            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(referenceCell.Y)));
            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(referenceCell.Y)));

            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            contextGrid.Add(processedGrid.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));

            contextGrid.RemoveAll(tempCell => tempCell == null);

            return contextGrid;
        }

        public List<Cell> CreateHoldingGrid(List<Cell> grid) 
        {
            return new List<Cell>(grid);
        }

        private void SetRowAndColumnCount(List<Cell> processedGrid)
        {
            rowCount = processedGrid.FindAll(tempCell => tempCell.X.Equals(0)).Count;
            columnCount = processedGrid.FindAll(tempCell => tempCell.Y.Equals(0)).Count; ;
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