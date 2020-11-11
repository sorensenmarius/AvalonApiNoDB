using System;
using System.Collections.Generic;
using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using Microsoft.AspNetCore.Mvc;

namespace AvalonApiNoDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            return GameStore.GetGame(id);
        }

        [HttpPost]
        public Game JoinGame(int joinCode, string playerName)
        {
            Game g = GameStore.GetGameByJoinCode(joinCode);

            Player p = new Player(playerName);

            g.Players.Add(p);

            return g;
        }
    }
}
