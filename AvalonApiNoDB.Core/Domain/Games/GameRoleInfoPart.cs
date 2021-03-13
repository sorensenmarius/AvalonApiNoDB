using AvalonApiNoDB.Core.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace AvalonApiNoDB.Core.Domain.Games
{
    public partial class Game
    {
        public string GetEvilPlayers()
        {
            var evil = Players.Where(p => p.IsEvil && p.RoleId != Role.Oberon).Select(p => p.Name);
            return string.Join(" & ", evil);
        }

        public string GetRoleString(Role role)
        {
            var roleInfo = "";
            switch (role)
            {
                case Role.NotYetChosen:
                    return "This shouldn't have happened :S";
                case Role.Servant:
                    return "You win if you can complete 3 Missions.";
                case Role.Minion:
                    return "The Evil players are: " + GetEvilPlayers();
                case Role.Merlin:
                    roleInfo += "You know who the evil players are, but don't reveal yourself|The Evil players are: ";
                    var evil = Players.Where(p => p.IsEvil && p.RoleId != Role.Mordred).Select(p => p.Name);
                    roleInfo += string.Join(" & ", evil);
                    return roleInfo;
                case Role.Percival:
                    roleInfo += "You know who Merlin is, support him without revealing him.|Merlin is: ";
                    var morgOrMerlin = Players.Where(p => p.RoleId == Role.Merlin || p.RoleId == Role.Morgana).Select(p => p.Name);
                    roleInfo += string.Join(" or ", morgOrMerlin);
                    return roleInfo;
                case Role.Mordred:
                    return "Merlin does not know that you are evil.|The evil players are: " + GetEvilPlayers();
                case Role.Morgana:
                    return "Percival sees you as Merlin.|The evil players are: " + GetEvilPlayers();
                case Role.Oberon:
                    return "You are Evil.";
                case Role.Assassin:
                    return "If you figure out who Merlin is, you win!|The evil players are: " + GetEvilPlayers();
            };
            return "No role? How? :thinking:";
        }
        public int GetNumberOfEvils(int numberOfPlayers)
        {
            numberOfPlayers = numberOfPlayers < 5 ? 5 : numberOfPlayers;
            return new List<int>() { 2, 2, 3, 3, 3, 4 }[numberOfPlayers - 5];
        }
    }
}
