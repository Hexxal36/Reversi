using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class Game
    {
        public string Id = Guid.NewGuid().ToString();

        public string PlayerOne { get; set; }

        public string PlayerTwo { get; set; }

        public string Moves { get; set; }

        public string BoardString { get; set; }

        public char OnTurn { get; set; }
    }
}
