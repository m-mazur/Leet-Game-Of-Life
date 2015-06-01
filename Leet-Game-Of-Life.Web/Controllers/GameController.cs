using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Leet_Game_Of_Life.Core.Logic;
using Leet_Game_Of_Life.Core.Models;

namespace Leet_Game_Of_Life.Web.Controllers
{
    public class GameController : ApiController
    {
        //GET api/game/
        public IEnumerable<Cell> Get(int pRow, int pCol)
        {
            Grid grid = new Grid().CreateGrid(pRow, pCol); 
            return grid.Cells;
        } 

        //POST api/game/1
        public IEnumerable<Cell> Post(List<Cell> gridSnapshot)
        {
            RulesProcessor rules = new RulesProcessor();
            Grid g = new Grid();
            g.Cells = gridSnapshot;
            return rules.CheckNeighborStateAndRunLogic(g).Cells;//gridSnapshot);
        }
    }
}