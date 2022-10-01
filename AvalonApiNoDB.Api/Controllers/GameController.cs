using System;
using AvalonApiNoDB.Api.Controllers.Dto;
using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using Microsoft.AspNetCore.Mvc;

namespace AvalonApiNoDB.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public Game Get(Guid id)
        {
            return GameStore.GetGame(id);
        }

        [HttpPost("Join")]
        public ActionResult<JoinGameResponseDto> Join(JoinGameInputDto input)
        {
            Game g;
            try
            {
                g = GameStore.GetGameByJoinCode(input.JoinCode);
                g.ValidateJoin(input.PlayerName);
            } catch(Exception e)
            {
                return ValidationProblem(e.Message);
            }

            var p = new Player(input.PlayerName);

            g.Players.Add(p);

            return new JoinGameResponseDto()
            {
                Game = g,
                Me = p
            };
        }

        [HttpPut]
        public Game Create()
        {
            var g = new Game();

            GameStore.AddGame(g);

            return g;
        }
    }
}
