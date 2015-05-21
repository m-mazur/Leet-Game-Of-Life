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
        //GET api/game/
        public IEnumerable<Cell> Get()
        {
            return new Grid().CreateGrid(10, 10);
        }

        //POST api/game/1
        public void Post(List<Cell> listCells)
        {
            List<Cell> list = listCells;
        }
    }
}