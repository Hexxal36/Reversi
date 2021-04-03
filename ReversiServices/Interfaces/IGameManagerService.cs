using ReversiData.Models;
using System.Threading.Tasks;

namespace ReversiServices.Interfaces
{
    public interface IGameManagerService
    {
        Task<string> CreateGame(string id);

        Task JoinGame(string gameid, string id);

        Game GetGame(string id);

        Task SaveGame();
    }
}
