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
        public IEnumerable<Cell> Get(int row, int column)
        {
            var grid = new Grid().CreateGrid(row, column).Cells;
            return grid;
        }

        //POST /api/game?row=5&column=5;
        public IEnumerable<Cell> Post(List<Cell> gridSnapshot)
        {
            var rules = new RulesProcessor();
            var grid = new Grid();
            
            grid.Cells = gridSnapshot;
            
            return rules.CheckNeighborStateAndRunLogic(grid).Cells;
        }
    }
}