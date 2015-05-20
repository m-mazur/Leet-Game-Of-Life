using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Leet_Game_Of_Life.Models;

namespace Leet_Game_Of_Life.Web.Controllers
{
    public class GameController : ApiController
    {
        // GET api/game/

        public IEnumerable<Row> Get()
        {
            Grid tempGrid = new Grid();
            Row tempRow = new Row();
            
            for (int i = 0; i < 15; i++)
            {
                tempRow.AddCellToRow(new Cell(i, i, true));
            }

            tempGrid.AddRowToGrid(tempRow);

            return tempGrid.CollectionOfRows;
        }
    }
}
