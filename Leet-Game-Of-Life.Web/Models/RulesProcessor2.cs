using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leet_Game_Of_Life.Web.Models
{
    public class RulesProcessor2
    {
        private List<Cell> initialList;
        private List<Cell> holdingList;
        private Grid grid;
        private int neighborCount;
        private int column;
        private int row;

        public RulesProcessor2()
        {
            this.grid = new Grid();
            this.neighborCount = 0;
        }

        public List<Cell> CheckNeighborStateAndRunLogic(List<Cell> snapshot)
        {
            initialList = ProcessList(snapshot);
            holdingList = new List<Cell>(initialList);
            List<List<Cell>> finalList = new List<List<Cell>>();
            column = FindColumnCount();
            row = FindRowCount();

            CheckRules(initialList);


            finalList.Add(holdingList);

            return holdingList;
        }

        private void CheckRules (List<Cell> list) 
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
        
        public List<Cell> ProcessList(List<Cell> list)
        {
            List<Cell> processedList = grid.CreateGrid(15, 40);
            
            foreach (var cell in processedList.Reverse<Cell>())
            {
                var newCell = list.Find(tempCell => (tempCell.X.Equals(cell.X)) && (tempCell.Y.Equals(cell.Y)));
                var index = processedList.IndexOf(cell);
                
                if (newCell != null)
                {
                    processedList.Remove(cell);
                    processedList.Insert(index, newCell);
                }
            }

            return processedList;
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

        private int FindRowCount()
        {
            int rowCount = 0;
            rowCount = initialList.FindAll(tempCell => tempCell.X.Equals(0)).Count;
            return rowCount;
        }

        private int FindColumnCount()
        {
            int colCount = 0;
            colCount = initialList.FindAll(tempCell => tempCell.Y.Equals(0)).Count;
            return colCount;
        }

        private int WrapEdges(int referenceCellPosition, bool isRow)
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
    }
}