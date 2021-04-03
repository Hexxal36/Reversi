using ReversiData.Models;
using System.Threading.Tasks;

namespace ReversiServices.Interfaces
{
    public interface IGameManagerService
    {
        Task<string> CreateGame();

        Game GetGame(string id);

        Task SaveGame();
    }
}
