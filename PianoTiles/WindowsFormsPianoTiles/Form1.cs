using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsPianoTiles
{
    public partial class Form1 : Form
    {
        Tiles[] tt;
        Methods mt;
        Random rr;
        internal bool lost = false;
        int clicked = -1;
        bool start = false;
        int score = 0;
        int interval = 0;
        int song = 0;
        bool lostplayed = false;

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            mt = new Methods(this);
            rr = new Random();
            tt = new Tiles[4];
            for (int i = 0; i < 25; i++) new SoundPlayer("Data/Songs/Fur Elise/" + i + ".wav").Load();
            Speed sp = new Speed(this);
            int speed = 20;
            if (sp.ShowDialog() == DialogResult.OK) speed = int.Parse(sp.numericUpDown1.Value.ToString());
            for (int i = 0; i < tt.Length; i++)
            {
                tt[i] = new Tiles(this, Width / 4 * i + 10, (Height / 4 + Height / 10) * i, Width / 4 - 10, Height / 4, i);
                tt[i].speed = speed;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (interval >= 10)
            {
                foreach (Tiles t in tt) t.speed++;
                interval = 0;
            }
            if (lost)
            {
                if (!lostplayed)
                {
                    lostplayed = true;
                    new System.Media.SoundPlayer("Data/Sounds/boom.wav").Play();
                }
                start = false;
                /*
                if (score < 10)
                {
                    g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 45, 0);//score
                }
                else if (score > 9 && score < 100)
                {
                    g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 65, 0);//score
                }
                else if (score > 99 && score < 1000)
                {
                    g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 125, 0);//score
                }*/
                g.DrawString("YOU LOSE", new Font("Arial", 100, FontStyle.Regular), Brushes.White, Width / 4 - 10, Height / 3);
                g.DrawString("Enter to Restart\nESC to Exit", new Font("Arial", 40, FontStyle.Regular), Brushes.White, Width / 3 + 30, Height / 2 + 20);
                //g.DrawString("ESC to Exit", new Font("Arial", 40, FontStyle.Regular), Brushes.White, Width / 3 + 30, Height / 2 + 20);
                try
                {
                    if (score > mt.p.Score)
                    {
                        mt.p.Score = score;
                        NewHighScore nw = new NewHighScore();
                        if (nw.ShowDialog() == DialogResult.OK) mt.p.Name = nw.textBoxName.Text;
                        else mt.p.Name = "Unnamed Player";
                        mt.Save(mt.p);
                    }
                }
                catch
                {
                    mt.p.Score = score;
                    NewHighScore nw = new NewHighScore();
                    if (nw.ShowDialog() == DialogResult.OK) mt.p.Name = nw.textBoxName.Text;
                    else mt.p.Name = "Unnamed Player";
                    mt.Save(mt.p);
                }
             //   return;
            }
            for (int i = 0; i < 4; i++) g.DrawLine(Pens.White, Width / 4 * i, 0, Width / 4 * i, Height);
            for (int i = 0; i < tt.Length; i++)
            {
                tt[i].Move();
                tt[i].Paint(g);
                tt[i].rec.X = Width / 4 * i + 5;
                tt[i].rec.Width = Width / 4 - 10;
                tt[i].rec.Height = Height / 4;
            }
            if (!start && !lost)
            {
                g.DrawString("Press Enter to Start", new Font("Arial", 40, FontStyle.Regular), Brushes.White, Width / 3, Height / 2);
                Invalidate();
                return;
            }
            if (score < 10)
            {
                g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 45, 0);//score
            }
            else if (score > 9 && score < 100)
            {
                g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 70, 0);//score
            }
            else if (score > 99 && score < 1000)
            {
                g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 95, 0);//score
            }
            else if (score > 999 && score < 10000)
            {
                g.DrawString(score.ToString(), new Font("Arial", 72, FontStyle.Regular), Brushes.White, Width / 2 - 125, 0);//score
            }
            g.DrawString("High:" + mt.p.Score + "\n" + mt.p.Name, new Font("Arial", 20, FontStyle.Regular), Brushes.White, 0, 0);//highscore
            g.DrawString(tt[0].speed.ToString(), new Font("Arial", 20, FontStyle.Regular), Brushes.White, Width - 65, 0);//speed
            Invalidate();
        }

        internal void ChangeLetter(int index)
        {
            foreach (Tiles t in tt)
            {
                if (t.index == index)
                {
                    int x = rr.Next(0, tt.Length * 2);
                    if (x == 0 || x == 1) x = 0;
                    else if (x == 2 || x == 3) x = 1;
                    else if (x == 4 || x == 5) x = 2;
                    else if (x == 6 || x == 7) x = 3;
                    tt[index].letter = x;
                    /*
                    for (; ; )
                    {
                        int x = new Random().Next(0, tt.Length);
                        if (index - 1 >= 0 && tt[index - 1].letter != x)
                        {
                            tt[index].letter = x;
                            break;
                        }
                        else if (index == 0 && tt[tt.Length - 1].letter != x)
                        {
                            tt[tt.Length - 1].letter = x;
                            break;
                        }
                        else continue;
                    }
                    */
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            for (; ; )
            {
                for (int i = 0; i < 24; i++)
                {
                    System.Threading.Thread.Sleep(250);
                    new SoundPlayer("Data/Songs/Fur Elise/" + i + ".wav").Play();      //For song testing only!!!
                }
            }
            return;
            */
            if (lost)
            {
                if (e.KeyData == Keys.Escape) Close();
                else if (e.KeyData == Keys.Enter) Application.Restart();
            }
            if (!start && e.KeyData == Keys.Enter)
            {
                foreach (Tiles t in tt)
                {
                    t.move = true;
                }
                start = true;
                return;
            }
            if (!start) return;
            if (clicked == -1)
            {
                if (tt[tt.Length - 1].tar == e.KeyData.ToString())
                {
                    tt[tt.Length - 1].clicked = true;
                    clicked = tt.Length - 1;
                    //score++; //dajana :D
                    score += tt[0].speed / 10;
                    interval++;
                    new SoundPlayer("Data/Songs/Fur Elise/0.wav").Play();
                    if (song == 24) song = 0;
                }
                else lost = true;
            }
            else
            {
                if (clicked - 1 >= 0 && tt[clicked - 1].tar == e.KeyData.ToString())
                {
                    tt[clicked - 1].clicked = true;
                    clicked = clicked - 1;
                    //score++; //dajana :D
                    score += tt[0].speed / 10;
                    interval++;
                    song++;
                    new SoundPlayer("Data/Songs/Fur Elise/" + song + ".wav").Play();
                    if (song == 24) song = 0;
                    return;
                }
                else if (clicked == 0 && tt[tt.Length - 1].tar == e.KeyData.ToString())
                {
                    tt[tt.Length - 1].clicked = true;
                    clicked = tt.Length - 1;
                    //score++; //dajana :D
                    score += tt[0].speed / 10;
                    interval++;
                    song++;
                    new SoundPlayer("Data/Songs/Fur Elise/" + song + ".wav").Play();
                    if (song == 24) song = 0;
                    return;
                }
                lost = true;
            }
        }
    }
}