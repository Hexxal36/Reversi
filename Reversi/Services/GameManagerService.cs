﻿using Reversi.Data;
using Reversi.Models;
using Reversi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Services
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

        public async Task<string> CreateGame()
        {
            var game = _gameService.GetDefaultGame();

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

        public async Task SaveGame()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
