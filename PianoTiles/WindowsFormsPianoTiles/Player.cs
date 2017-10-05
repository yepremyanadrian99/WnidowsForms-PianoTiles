using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsPianoTiles
{
    [Serializable]
    class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Player() { }

        public Player(string Name, int Score)
        {
            this.Name = Name;
            this.Score=Score;
        }
    }
}