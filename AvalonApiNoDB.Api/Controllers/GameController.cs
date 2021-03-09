﻿using System;
using System.Collections.Generic;
using System.Linq;
using AvalonApiNoDB.Api.Controllers.Dto;
using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AvalonApiNoDB.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        public GameController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
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
            } catch(KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

            if (g.Players.Count >= 10)
            {
                return Forbid("Game is full");
            }

            if (g.Players.Select(p => p.Name).Contains(input.PlayerName))
            {
                return Forbid("Name already taken");
            }

            if (input.PlayerName.Length > 15)
            {
                return Forbid("Name can be no longer than 15 characters");
            }
            Player p = new Player(input.PlayerName);

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
            Game g = new Game();

            GameStore.AddGame(g);

            return g;
        }
    }
}
