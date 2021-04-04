using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiData.Models
{
    public class JoinEventArgs : EventArgs
    {
        public JoinEventArgs(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }
    }
}
