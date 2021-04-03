using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using ReversiData.Data;
using ReversiData.Models;
using ReversiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiServices
{
    public class GameManagerService : IGameManagerService
    {
        private readonly IGameService _gameService;
        private readonly ApplicationDbContext _context;

        public GameManagerService(IGameService gameService,
            ApplicationDbContext context)
        {
            _gameService = gameService;
            _context = context;
        }

        public async Task<string> CreateGame(string id)
        {
            var game = _gameService.GetDefaultGame();

            game.PlayerOne = id;

            _context.Games.Add(game);

            await this._context.SaveChangesAsync();

            return game.Id.ToString();
        }

        public Game GetGame(string id)
        {
            var game = _context.Games.Where(x => x.Id.ToString() == id).FirstOrDefault();

            if (game is null)
            {
                throw new Exception("This game doesn't exist");
            }

            return game;
        }

        public async Task JoinGame(string gameid, string id)
        {
            var game = _context.Games.Where(x => x.Id.ToString() == gameid).FirstOrDefault();

            if (game is null)
            {
                throw new Exception("This game doesn't exist");
            }

            game.PlayerTwo = id;

            await this._context.SaveChangesAsync();
        }

        public async Task SaveGame()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
