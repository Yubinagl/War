using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    internal class Computer:Player
    {
        private Random random;

        private int length;

        private bool[] cards;
        public Computer(int length)
        {
            random = new Random();
            this.length = length;
            cards = new bool[length];
        }
        public int DrawCard()
        {
            int n = -1;
            //わからない
            while (n < 0)
            {
                n = random.Next(length);
                if (cards[n] == true)
                    n = -1;
            }
            cards[n] = true;
            NowOpenCardIndex = n;
            return n; 
        }

        public void CardsClear()
        {
            Array.Clear(cards, 0, cards.Length);//?
    }
}
