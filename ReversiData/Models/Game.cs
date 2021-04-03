using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiData.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public string PlayerOne { get; set; }

        public string PlayerTwo { get; set; }

        public string Moves { get; set; }

        public string BoardString { get; set; }

        public char OnTurn { get; set; }

        public string Winner { get; set; }

        public bool IsActive { get; set; }
    }
}
