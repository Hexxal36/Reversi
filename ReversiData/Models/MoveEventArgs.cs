using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiData.Models
{
    public class MoveEventArgs : EventArgs
    {
        public MoveEventArgs(int x, int y, string id)
        {
            xIndex = x;
            yIndex = y;
            Id = id;
        }


        public int xIndex { get; set; }
        public int yIndex { get; set; }
        public string Id { get; set; }
    }
}
