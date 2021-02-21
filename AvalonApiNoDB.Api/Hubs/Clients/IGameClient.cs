using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Api.Hubs.Clients
{
    public interface IGameClient
    {
        Task GameUpdated(Game game);
    }
}
