using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Core.Domain.Games
{
    public enum GameStatus
    {
        WaitingForPlayers = 0,
        Playing = 1,
        AssassinTurn = 2,
        Ended = -1
    }
}
