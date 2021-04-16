using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiData.Models
{
    public class JoinEventArgs : EventArgs
    {
        public JoinEventArgs(string userId, string gameId)
        {
            this.UserId = userId;
            this.GameId = gameId;
        }

        public string UserId { get; set; }

        public string GameId { get; set; }
    }
}
