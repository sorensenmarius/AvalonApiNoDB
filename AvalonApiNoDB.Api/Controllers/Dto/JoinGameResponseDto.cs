using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Api.Controllers.Dto
{
    public class JoinGameResponseDto
    {
        public Game Game { get; set; }
        public Player Me { get; set; }
    }
}
