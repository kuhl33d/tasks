using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp25
{
    public partial class Form1 : Form
    {
        public class nigs
        {
            public int x, y;
            public Bitmap img;
        }

        public Timer tt = new Timer();
        public nigs tom = new nigs();
        public nigs jerry = new nigs();
        public int ct = 0;
        public List<Point> sps = new List<Point>();
        public List<Point> eps = new List<Point>();
        public List<Point> rsps = new List<Point>();
        public List<Point> reps = new List<Point>();
        public List<Point> ups = new List<Point>();
        public Boolean isDrag = false;
        public Point p1, p2;
        public Boolean isDown = false;
        public int y1, y2, x, newx, newy;
        public Boolean created = false;
        public Bitmap off;
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            tt.Tick += Tt_Tick;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                p2 = new Point(e.X, e.Y);
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rsps.Add(p1);
                reps.Add(p2);
                isDown = false;
                tt.Start();
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                p1 = new Point(e.X, e.Y);
                isDown = true;
            }
            DrawDubb(CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            tom.y += 4;
            for (int i = 0; i < rsps.Count; i++)
            {
                if (tom.x + tom.img.Width / 2 <= rsps[i].X + 4 && tom.x + tom.img.Width / 2 >= rsps[i].X - 4
                    && tom.y + tom.img.Height / 2 <= rsps[i].Y + 4 && tom.y + tom.img.Height / 2 >= rsps[i].Y - 4)
                {
                    for (int j = 0; j < sps.Count; j++)
                    {
                        if (reps[i].X >= sps[j].X - 5 && reps[i].X <= sps[j].X + 5)
                        {
                            tom.x = sps[j].X - tom.img.Width / 2;
                        }
                    }

                    tom.y = reps[i].Y;
                }
            }
            for (int i = 0; i < reps.Count; i++)
            {
                if (tom.x + tom.img.Width / 2 <= reps[i].X + 4 && tom.x + tom.img.Width / 2 >= reps[i].X - 4
                    && tom.y + tom.img.Height / 2 <= reps[i].Y + 4 && tom.y + tom.img.Height / 2 >= reps[i].Y - 4)
                {
                    for (int j = 0; j < sps.Count; j++)
                    {
                        if (rsps[i].X >= sps[j].X - 5 && rsps[i].X <= sps[j].X + 5)
                        {
                            tom.x = sps[j].X - tom.img.Width / 2;
                        }
                    }

                    tom.y = rsps[i].Y;
                }
            }
            if (tom.y >= ClientSize.Height / 10 * 4)
            {
                if (tom.y >= jerry.y && tom.y <= jerry.y + jerry.img.Height
                    && tom.x >= jerry.x && tom.x <= jerry.x + jerry.img.Height)
                {
                    tt.Stop();
                    MessageBox.Show("YOU WIN!!!!");
                }
                else
                {
                    tt.Stop();
                    MessageBox.Show("YOU LOSE!!");
                }
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (ct != 0)
                {
                    sps = new List<Point>();
                    eps = new List<Point>();
                    rsps = new List<Point>();
                    reps = new List<Point>();
                }
                createblacklines();
                createredlines();
                createtomnjerry();
                created = true;
                ct++;
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            DrawDubb(CreateGraphics());
        }
        void createblacklines()
        {
            Random rr = new Random();
            int rand = rr.Next(2, 10);
            y1 = ClientSize.Height / 10;
            y2 = y1 * 4;
            x = ClientSize.Width / 10;
            for (int i = 0; i < rand; i++)
            {
                Point pnn = new Point(x + (x * i), y1);
                sps.Add(pnn);
            }
            for (int i = 0; i < rand; i++)
            {
                Point pnn = new Point(x + (x * i), y2);
                eps.Add(pnn);
            }
        }
        void createredlines()
        {
            Random rr = new Random();
            int rand = rr.Next(1, 10);
            for (int i = 0; i < rand; i++)
            {
                int xr = rr.Next(0, sps.Count - 2);
                int yr = rr.Next(ClientSize.Height / 10, ClientSize.Height / 10 * 4);
                Point pnn = new Point(sps[xr].X, yr);
                rsps.Add(pnn);
                Point pne = new Point(sps[xr + 1].X, yr);
                reps.Add(pne);
            }
        }
        void createtomnjerry()
        {
            Random rr = new Random();
            int rand = rr.Next(0, sps.Count - 1);
            tom.img = new Bitmap("tom.bmp");
            tom.x = sps[rand].X - tom.img.Width / 2;
            tom.y = sps[rand].Y - tom.img.Height;
            rand = rr.Next(0, eps.Count - 1);
            jerry.img = new Bitmap("jerry.bmp");
            jerry.x = eps[rand].X - jerry.img.Width / 2;
            jerry.y = eps[rand].Y;
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            if (created)
            {
                g.Clear(Color.Lime);

                for (int i = 0; i < sps.Count; i++)
                {
                    Pen p = new Pen(Brushes.Black, 4);
                    g.DrawLine(p, sps[i], eps[i]);
                }
                for (int i = 0; i < rsps.Count; i++)
                {
                    Pen p = new Pen(Brushes.Red, 4);
                    g.DrawLine(p, rsps[i], reps[i]);
                }
                if (isDown)
                {
                    Pen p = new Pen(Brushes.Blue, 4);
                    g.DrawLine(p, p1, p2);
                }
                tom.img.MakeTransparent(tom.img.GetPixel(0, 0));
                g.DrawImage(tom.img, tom.x, tom.y);
                jerry.img.MakeTransparent(jerry.img.GetPixel(0, 0));
                g.DrawImage(jerry.img, jerry.x, jerry.y);
            }


        }
    }
}
