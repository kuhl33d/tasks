namespace assignment_14
{
    class Rect
    {
        public int x, y, w, h;
        public Color clr;
    }
    class stand
    {
        public int x, y,w,h;
        public List<Rect> rects;
        public stand()
        {
            rects = new List<Rect>();
        }
    }
    public partial class Form1 : Form
    {
        stand []stands = new stand[3];
        int n_rect = 3;
        bool isDrag = false;
        int which = -1,t,steps=0;
        Bitmap img;
        public Form1()
        {
            WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Draw(CreateGraphics());
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            if (isDrag == true)
            {
                t = -1;
                for(int i = 0; i < 3; i++)
                {
                    if (e.X >= stands[i].x && e.X <= stands[i].x + stands[i].w)
                    {
                        if (e.Y >= stands[i].y && e.Y <= stands[i].y + stands[i].h)
                        {
                            if(stands[i].rects.Count==0 || stands[which].rects[stands[which].rects.Count - 1].w < stands[i].rects[stands[i].rects.Count - 1].w)
                            {
                                t = i;
                                Rect pnn = stands[which].rects[stands[which].rects.Count - 1];
                                stands[which].rects.RemoveAt(stands[which].rects.Count - 1);
                                pnn.x = stands[i].x + stands[i].w / 2 - pnn.w / 2;
                                pnn.y = stands[i].y + (20 * (n_rect + 1)) - (stands[i].rects.Count + 1) * pnn.h;
                                stands[i].rects.Add(pnn);
                                steps++;
                                Draw(CreateGraphics());
                                if(stands[i].rects.Count == n_rect && i==2)
                                {
                                    MessageBox.Show("GAME OVER IN "+steps+" STEPS !!!");
                                }
                            }
                        }
                    }
                }
                if(t == -1)
                {
                    stands[which].rects[stands[which].rects.Count - 1].x = stands[which].x + stands[which].w / 2 - stands[which].rects[stands[which].rects.Count - 1].w / 2;
                    stands[which].rects[stands[which].rects.Count - 1].y = stands[which].y + (20 * (n_rect + 1)) - (stands[which].rects.Count) * stands[which].rects[stands[which].rects.Count - 1].h;
                }
            }
            isDrag = false;
            which = -1;
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDrag == true && which != -1)
            {
                stands[which].rects[stands[which].rects.Count-1].x = e.X;
                stands[which].rects[stands[which].rects.Count - 1].y = e.Y;
            }
            Draw(CreateGraphics());
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                for(int i = 0; i < 3; i++)
                {
                    if(e.X>=stands[i].x && e.X <= stands[i].x + stands[i].w)
                    {
                        if(e.Y >= stands[i].y && e.Y <= stands[i].y + stands[i].h)
                        {
                            if(e.X >= stands[i].rects[stands[i].rects.Count-1].x && e.X <= stands[i].rects[stands[i].rects.Count - 1].x+ stands[i].rects[stands[i].rects.Count - 1].w)
                            {
                                if(e.Y >= stands[i].rects[stands[i].rects.Count - 1].y && e.Y <= stands[i].rects[stands[i].rects.Count - 1].y + stands[i].rects[stands[i].rects.Count - 1].h)
                                {
                                    isDrag = true;
                                    which = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            img = new Bitmap(ClientSize.Width, ClientSize.Height);
            for (int i = 0; i < 3; i++)
            {
                stands[i] = new stand();
                stands[i].y = ClientSize.Height / 4;
                stands[i].w = n_rect * 10 + 60;
                stands[i].h = 40 + n_rect * 20;
                switch (i)
                {
                    case 0:
                        stands[i].x = ClientSize.Width / 4;
                        for (int j = 0; j < n_rect; j++)
                        {
                            Rect pnn = new Rect();
                            pnn.x = stands[i].x+(j)*10;
                            pnn.y = (n_rect-j)*20+stands[i].y;
                            pnn.w = stands[i].w - (j * 20);
                            pnn.h = 20;
                            pnn.clr = Color.Blue;
                            stands[i].rects.Add(pnn);
                        }
                        break;
                    case 1:
                        stands[i].x = ClientSize.Width / 2;
                        break;
                    case 2:
                        stands[i].x = 3 * ClientSize.Width / 4;
                        break;
                }
            }
            Draw(CreateGraphics());
        }

        void Draw(Graphics G)
        {
            Graphics g = Graphics.FromImage(img);
            g.Clear(BackColor);
            for(int i = 0; i < 3; i++)
            {
                g.FillRectangle(new SolidBrush(Color.Gray), stands[i].x+(stands[i].w/2) - 5,stands[i].y,10,stands[i].h);
                g.FillRectangle(new SolidBrush(Color.Gray), stands[i].x, stands[i].y+(20*(n_rect+1)), stands[i].w, 20);
            }
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < stands[i].rects.Count; j++)
                {
                    g.FillRectangle(new SolidBrush(stands[i].rects[j].clr), stands[i].rects[j].x, stands[i].rects[j].y, stands[i].rects[j].w, stands[i].rects[j].h);
                    g.DrawRectangle(new Pen(Color.Black, 2), stands[i].rects[j].x, stands[i].rects[j].y, stands[i].rects[j].w - 1, stands[i].rects[j].h - 1);
                }
            }
            G.DrawImage(img, 0, 0);
        }
    }
}