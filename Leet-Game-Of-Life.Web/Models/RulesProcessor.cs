using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leet_Game_Of_Life.Web.Models
{
    public class RulesProcessor
    {
        private List<Cell> initialList;
        private List<Cell> holdingList;
        private Grid grid;
        private int neighborCount;
        private int column;
        private int row;

        public RulesProcessor(List<Cell> initialList)
        {
            this.grid = new Grid();
            this.initialList = initialList;
            this.initialList = ProcessList(this.initialList);
            this.holdingList = new List<Cell>(this.initialList);
            this.neighborCount = 0;
            column = FindColumnCount();
            row = FindRowCount();
        }

        private List<Cell> ProcessList(List<Cell> list) 
        {
            List<Cell> processedList = grid.CreateGrid(15, 40);
            List<Cell> justAnotherListYaj = new List<Cell>(processedList);

            foreach (var cell in processedList)
            {
                var newCell = initialList.Find(tempCell => (tempCell.X.Equals(cell.X)) && (tempCell.Y.Equals(cell.Y)));

                if (newCell != null)
                {
                    justAnotherListYaj.RemoveAt(processedList.IndexOf(cell));
                    justAnotherListYaj.Insert(processedList.IndexOf(cell), newCell);
                }
            }

            return justAnotherListYaj;
        }

        public int FindRowCount()
        {
            int rowCount = 0;
            rowCount = initialList.FindAll(tempCell => tempCell.X.Equals(0)).Count;
            return rowCount;
        }

        public int FindColumnCount()
        {
            int colCount = 0;
            colCount = initialList.FindAll(tempCell => tempCell.Y.Equals(0)).Count;
            return colCount;
        }

        public int WrapEdges(int referenceCellPosition, bool isRow)
        {
            int value = 0;
            value = isRow ? row : column;

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


        private List<Cell> CreateContextGrid(List<Cell> initialGrid, Cell referenceCell)
        {
            var tempList = new List<Cell>();

            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y - 1, true))));

            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(referenceCell.Y)));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(referenceCell.Y)));

            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X - 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(referenceCell.X) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));
            tempList.Add(initialList.Find(tempCell => tempCell.X.Equals(WrapEdges(referenceCell.X + 1, false)) && tempCell.Y.Equals(WrapEdges(referenceCell.Y + 1, true))));

            tempList.RemoveAll(tempCell => tempCell == null);

            return tempList;
        }

        public List<Cell> CheckNeighborStateAndRunLogic()
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