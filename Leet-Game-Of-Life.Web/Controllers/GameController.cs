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
        public IEnumerable<Cell> Get()
        {
            List<Cell> grid = new Grid().CreateGrid(15, 40);
            return grid;
        } 

        //POST api/game/1
        public IEnumerable<List<Cell>> Post(List<Cell> gridSnapshot)
        {
            RulesProcessor rules = new RulesProcessor();
            return rules.CheckNeighborStateAndRunLogicAndCreateDataSet(gridSnapshot);
        }
    }
}