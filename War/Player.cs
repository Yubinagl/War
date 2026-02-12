using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    internal class Player
    {
        public Player() 
        {
            NowOpenCardIndex = -1;
            BeforeOpenCardIndex = -1;
            Score = 0;
        }

        public int NowOpenCardIndex {  get; set; }

        public int BeforeOpenCardIndex { get; set; }

        public int Score {  get; set; }

        public void Reset()
        {
            BeforeOpenCardIndex = NowOpenCardIndex;
            NowOpenCardIndex = -1;
        }
    }
}
