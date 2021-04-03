using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Services.Interfaces
{
    public interface IGameManagerService
    {
        Task<string> CreateGame();

        Game GetGame(string id);

        Task SaveGame();
    }
}
