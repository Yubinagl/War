using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War
{
    public partial class Form1 : Form
    {
        private Card[] cards;
        private Card[] userCards;
        private Card[] computerCards;
        private Player user;
        private Computer computer;
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateCard(ref Card[] cards)
        {
            string[] picture =
            {
                "A","2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"
            };

            cards = new Card[picture.Length*2];
            for(int i=0, j=0; i<cards.Length; i+=2, j++)
            {
                cards[i]=new Card(picture[j]);
                cards[i+1]=new Card(picture[j]);
            }
        }

        private void ShuffleCard(Card[] cards)
        {
            Random r = new Random();
            int n = cards.Length - 1;

            while (n > 0)
            {
                int w = r.Next(0, n);
                string s = cards[n].Picture;
                cards[n].Picture = cards[w].Picture;
                cards[w].Picture = s;
                n--;
            }
        }

        private bool AllOpen(Card[] cards)
        {
            foreach(Card c in cards)
            {
                if (c.State == false)
                    return false;
            }
            return true;
        }

        private int Judge(Card[] cards1, int index1, Card[] cards2, int index2)
        {
            if (index1 < 0 || index1 >= cards1.Length ||
                index2 < 0 || index2 >= cards2.Length)
                return 0;

            string[] pictures =
            {
                "A","2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"
            };

            int[] num = { 14, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            int score1=0;
            for(int i=0; i<pictures.Length; i++)
            {
                if (cards1[index1].Picture == pictures[i])
                {
                    score1 = num[i];
                    break;
                }
                else
                    score1 = 14;
            }

            int score2=0;
            for (int i = 0; i < pictures.Length; i++)
            {
                if (cards2[index2].Picture == pictures[i])
                {
                    score2 = num[i];
                    break;
                }
                else 
                    score2 = 14;
            }

            if (score1 == score2)
                return 1;
            else if (score1 > score2)
                return 2;
            else
                return 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateCard(ref cards);
            ShuffleCard(cards);

            userCards = new Card[cards.Length / 2];
            computerCards=new Card[cards.Length / 2];
            Array.Copy(cards, userCards, userCards.Length);
            Array.Copy(cards, userCards.Length, computerCards, 0, computerCards.Length);

            user = new Player();
            computer = new Computer(computerCards.Length);//lengthを渡すのはなんで

            
            SuspendLayout();
            int offsetX = 130, offsetY = 240;
            for (int i=0; i<userCards.Length; i++)
            {
                userCards[i].Name = "userCard" + i;
                int sizeW=userCards[i].Size.Width;
                //int sizeH=userCards[i].Size.Height;
                userCards[i].Location = new Point(
                    offsetX + sizeW * i, offsetY);
                userCards[i].Click += new EventHandler(CardButtons_Click);
                userCards[i].Enabled = true;
            }
            Controls.AddRange(userCards);

            offsetY = 30;
            for(int i=0; i<computerCards.Length; i++)
            {
                computerCards[i].Name = "computerCard" + i;
                int sizeW=computerCards[i].Size.Width;
                //int sizeH=computerCards[i].Size.Height;
                computerCards[i].Location = new Point(
                    offsetX + i * sizeW, offsetY);
            }
            Controls.AddRange(computerCards);

            ResumeLayout(false);

            label5.Text = "カードを選んでね";
            button1.Enabled = false;
        }

        private void CardButtons_Click(object sender, EventArgs e)
        {
            //ユーザーのカードの添え字を取得
            int n1 = int.Parse(((Button)sender).Name.Substring(8));
            user.Reset();
            computer.Reset();
            if (user.BeforeOpenCardIndex != -1)
                userCards[user.BeforeOpenCardIndex].BackColor = Color.LightGray;
            if (computer.BeforeOpenCardIndex != -1)
                computerCards[computer.BeforeOpenCardIndex].BackColor = Color.LightGray;

            user.NowOpenCardIndex = n1;
            userCards[n1].Open();

            int n2=computer.DrawCard();
            //computer.NowOpenCardIndex = n2; なんでいらないんだろう
            computerCards[n2].Open();

            int result = Judge(userCards, n1, computerCards, n2);

            if (result == 1)
            {
                label5.Text = "ひきわけ";
                user.Score += result;
                computer.Score += result;
            }
            else if (result == 2)
            {
                label5.Text = "わたしの勝ち";
                user.Score += result;
            }
                
            else
            {
                label5.Text = "コンピュータの勝ち";
                computer.Score += 2;
            }

            label2.Text = "得点：" + computer.Score;
            label4.Text = "得点：" + user.Score;

            if (AllOpen(userCards))
            {
                if(user.Score==computer.Score)
                    label5.Text = "ひきわけです";
                else if(user.Score>computer.Score)
                    label5.Text = "わたしの勝ちです";
                else
                    label5.Text = "コンピュータの勝ちです";

                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShuffleCard(cards);

            Array.Copy(cards, userCards, userCards.Length);
            Array.Copy(cards, userCards.Length, computerCards, 0, computerCards.Length);

            foreach(Card card in userCards)
            {
                card.Close();
            }
            foreach(Card card in computerCards)
            {
                card.Close();
            }

            user.Reset();
            user.Score = 0;
            computer.Reset();
            computer.CardsClear();
            computer.Score = 0;

            label2.Text = computer.Score.ToString();
            label4.Text = user.Score.ToString();

            label5.Text = "カードを選んでください";
            button1.Enabled = false;
        }
    }
}
