using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Api.Hubs.Clients
{
    public interface IGameClient
    {
        Task PlayerJoined(Player player);
        Task MessageReceived(string message);
        Task GameUpdated(Game game);
    }
}
