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
        // GET api/game/1

        public IEnumerable<Cell> Get(string id)
        {
            List<Cell> cells1 = new List<Cell>();
            List<Cell> cells2 = new List<Cell>();
            List<Cell> cells3 = new List<Cell>();

            //1
            cells1.Add(new Cell(1, 1, true));
            cells1.Add(new Cell(2, 1, true));
            cells1.Add(new Cell(3, 1, true));
            cells1.Add(new Cell(4, 1, true));
            
            cells1.Add(new Cell(1, 2, false));
            cells1.Add(new Cell(2, 2, true));
            cells1.Add(new Cell(3, 2, true));
            cells1.Add(new Cell(4, 2, true));
            
            cells1.Add(new Cell(1, 3, false));
            cells1.Add(new Cell(2, 3, true));
            cells1.Add(new Cell(3, 3, true));
            cells1.Add(new Cell(4, 3, true));
            
            cells1.Add(new Cell(1, 4, false));
            cells1.Add(new Cell(2, 4, true));
            cells1.Add(new Cell(3, 4, true));
            cells1.Add(new Cell(4, 4, true));

            //2
            cells2.Add(new Cell(1, 1, true));
            cells2.Add(new Cell(2, 1, true));
            cells2.Add(new Cell(3, 1, true));
            cells2.Add(new Cell(4, 1, true));
            
            cells2.Add(new Cell(1, 2, false));
            cells2.Add(new Cell(2, 2, false));
            cells2.Add(new Cell(3, 2, true));
            cells2.Add(new Cell(5, 2, true));
            
            cells2.Add(new Cell(1, 3, false));
            cells2.Add(new Cell(2, 3, false));
            cells2.Add(new Cell(3, 3, true));
            cells2.Add(new Cell(4, 3, true));
            
            cells2.Add(new Cell(1, 4, false));
            cells2.Add(new Cell(2, 4, false));
            cells2.Add(new Cell(3, 4, true));
            cells2.Add(new Cell(4, 4, true));

            //3
            cells3.Add(new Cell(1, 1, true));
            cells3.Add(new Cell(2, 1, true));
            cells3.Add(new Cell(3, 1, true));
            cells3.Add(new Cell(4, 1, true));
            
            cells3.Add(new Cell(1, 2, false));
            cells3.Add(new Cell(2, 2, false));
            cells3.Add(new Cell(3, 2, false));
            cells3.Add(new Cell(4, 2, true));
            
            cells3.Add(new Cell(1, 3, false));
            cells3.Add(new Cell(2, 3, false));
            cells3.Add(new Cell(3, 3, false));
            cells3.Add(new Cell(4, 3, true));
            
            cells3.Add(new Cell(1, 4, false));
            cells3.Add(new Cell(2, 4, false));
            cells3.Add(new Cell(3, 4, false));
            cells3.Add(new Cell(4, 4, true));

            if (int.Parse(id) == 1)
            {
                return cells1;
            }
            else if (int.Parse(id) == 2)
            {
                return cells2;
            }
            else if (int.Parse(id) == 3)
            {
                return cells3;
            }
            else
            {
                return cells2;
            }
        }
    }
}