using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace War
{
    internal class Card:Button
    {
        private const int sizeW = 50, sizeH = 70;
        public Card(string picture)
        {
            Picture = picture;
            State = false;
            BackColor = CloseColor;
            Size=new Size(sizeW, sizeH);
            Enabled = false;
        }

        public string Picture { get; set; }

        public bool State {  get; private set; }

        public Color CloseColor { get; } = Color.LightPink;

        public Color OpenColor { get; } = Color.Lavender;

        //public Color FinishColor { get; } = Color.LightGray;

        public void Open()
        {
            State = true;
            BackColor = OpenColor;
            Text = Picture;
            Enabled = false;

        }

        public void Close()
        {
            State = false;
            BackColor = CloseColor;
            Text = "";
            Enabled = true;
        }


    }
}
