using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace WindowsFormsPianoTiles
{
    class Tiles
    {
        Form1 fr;
        internal Rectangle rec;
        Random rr;
        int interval = 10;
        internal int speed = 40;
        internal bool move = false;
        internal int index;
        internal int letter = 0;
        internal string tar;
        internal bool clicked = false;

        public Tiles() { }

        public Tiles(Form1 fr, int x, int y, int width, int height, int index)
        {
            this.fr = fr;
            this.index = index;
            rec.X = x;
            rec.Y = y;
            rec.Width = width;
            rec.Height = height;
            rr = new Random();
        }

        internal void Paint(Graphics g)
        {
            if (!clicked)
            {
                g.FillRectangle(Brushes.Black, rec);
                switch (letter)
                {
                    case 0:
                        {
                            tar = "W";
                            g.DrawString(tar, new Font("Arial", 60, FontStyle.Bold), Brushes.White, new Point(rec.X + (rec.Width / 3), rec.Y + rec.Height / 4));
                            break;
                        }
                    case 1:
                        {
                            tar = "S";
                            g.DrawString(tar, new Font("Arial", 60, FontStyle.Bold), Brushes.White, new Point(rec.X + (rec.Width / 3), rec.Y + rec.Height / 4));
                            break;
                        }
                    case 2:
                        {
                            tar = "A";
                            g.DrawString(tar, new Font("Arial", 60, FontStyle.Bold), Brushes.White, new Point(rec.X + (rec.Width / 3), rec.Y + rec.Height / 4));
                            break;
                        }
                    case 3:
                        {
                            tar = "D";
                            g.DrawString(tar, new Font("Arial", 60, FontStyle.Bold), Brushes.White, new Point(rec.X + (rec.Width / 3), rec.Y + rec.Height / 4));
                            break;
                        }
                }
                return;
            }
            //g.FillRectangle(Brushes.White, rec);
            g.FillRectangle(Brushes.Transparent, rec);
        }

        internal void Move()
        {
            if (move)
            {
                if (clicked)
                {
                    //rec.Y = -rec.Height - rr.Next(2, 5) * 20;  //esel karelia ogtagortsel, shat mets chi tarberutyun@!!
                    rec.Y = -rec.Height;
                    fr.ChangeLetter(index);
                    clicked = false;
                }
                if (rec.Y > fr.Height)
                {
                    if (!clicked)
                    {
                        fr.lost = true;
                    }
                }
                /*
                if (clicked)
                {
                    rec.Y = -rec.Width * 2;
                    fr.ChangeLetter(index);
                    clicked = false;
                }
                */
                rec.Y += speed;
                Thread.Sleep(interval);
            }
        }
    }
}