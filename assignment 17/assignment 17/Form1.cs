namespace assignment_17
{
    class rect
    {
        public int x,y,w,h;
        public Color clr;
        public List<ball> balls;
        public rect()
        {
            balls= new List<ball>();
        }
    }
    class ball
    {
        public int x, y;
        public Bitmap img;
        public ball()
        {
            img = new Bitmap("ball2.bmp");
            img.MakeTransparent(img.GetPixel(0,0));
        }
    }
    class player
    {
        public int x, y,currImg;
        public Bitmap []imgs;
        public ball catchedBall;
        public int oldx, oldy;
        public int which;
        public player()
        {
            catchedBall = null;
            currImg = 0;
            which = -1;
            imgs = new Bitmap[8];
            for(int i = 0; i < 8; i++)
            {
                imgs[i] = new Bitmap("w" + (i + 1) + ".bmp");
                imgs[i].MakeTransparent(imgs[i].GetPixel(0,0));
            }
        }

    }
    public partial class Form1 : Form
    {
        player p =new player();
        rect[] goals = new rect[2];
        int n_balls = 7;
        bool catched = false;
        public Form1()
        {
            WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Yellow;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if(p.y >= 10)
                    {
                        p.y -= 10;
                        p.currImg++;
                        p.currImg %= 8;
                        if(catched == true)
                        {
                            p.catchedBall.y -= 10;
                        }
                    }
                    break;
                case Keys.Down:
                    if(p.y <= ClientSize.Height - p.imgs[p.currImg].Height - 10)
                    {
                        p.y += 10;
                        p.currImg++;
                        p.currImg %= 8;
                        if (catched == true)
                        {
                            p.catchedBall.y += 10;
                        }
                    }
                    break;
                case Keys.Left:
                    if(p.x >= 10)
                    {
                        p.x -= 10;
                        p.currImg++;
                        p.currImg %= 8;
                        if (catched == true)
                        {
                            p.catchedBall.x -= 10;
                        }
                    }
                    break;
                case Keys.Right:
                    if (p.x <= ClientSize.Width-p.imgs[p.currImg].Width - 10)
                    {
                        p.x += 10;
                        p.currImg++;
                        p.currImg %= 8;
                        if (catched == true)
                        {
                            p.catchedBall.x += 10;
                        }
                    }
                    break;
                case Keys.Enter:
                    if (catched == false)
                    {
                        carryBall();
                    }
                    else
                    {
                        leaveBall();
                    }
                    break;
            }
            draw(this.CreateGraphics());
        }
        void carryBall()
        {
            for(int i = 0; i < 2; i++)
            {
                if(p.x >= goals[i].x && p.x <= goals[i].x + goals[i].w)
                {
                    if(p.y >= goals[i].y && p.x <= goals[i].y + goals[i].h)
                    {
                        if(goals[i].balls.Count != 0)
                        {
                            ball pnn = goals[i].balls[goals[i].balls.Count - 1];
                            p.catchedBall = pnn;
                            p.oldx = pnn.x;
                            p.oldy = pnn.y;
                            pnn.x = p.x - pnn.img.Width;
                            pnn.y = p.y;
                            catched = true;
                            p.which = i;
                            break;
                        }
                    }
                }
            }
        }
        void leaveBall()
        {
            if(catched == true)
            {
                for(int i = 0; i < 2; i++)
                {
                    if (p.x >= goals[i].x && p.x <= goals[i].x + goals[i].w)
                    {
                        if (p.y >= goals[i].y && p.y <= goals[i].y + goals[i].h)
                        {
                            if(i != p.which)
                            {
                                ball pnn = p.catchedBall;
                                p.catchedBall = null;
                                goals[p.which].balls.RemoveAt(goals[p.which].balls.Count-1);
                                pnn.y = goals[i].y + (goals[i].balls.Count / (n_balls / 2)) * pnn.img.Height;
                                pnn.x = goals[i].x + (goals[i].balls.Count % (n_balls / 2)) * pnn.img.Width;
                                goals[i].balls.Add(pnn);
                                p.which = -1;
                            }
                            else
                            {
                                p.catchedBall.x = p.oldx;
                                p.catchedBall.y = p.oldy;
                                p.which = -1;
                                p.catchedBall = null;
                            }
                            break;
                        }
                    }
                }
                if(p.which != -1)
                {
                    p.catchedBall.x = p.oldx;
                    p.catchedBall.y = p.oldy;
                    p.catchedBall = null;
                    p.which= -1;
                }
                catched = false;
                if(goals[0].balls.Count == n_balls)
                {
                    MessageBox.Show("WINNER");
                }
            }
        }
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            p.x = ClientSize.Width / 2 - p.imgs[p.currImg].Width / 2;
            p.y = ClientSize.Height / 2 - p.imgs[p.currImg].Height / 2;
            goals[0] = new rect();
            goals[0].w = ClientSize.Width / 5;
            goals[0].h = ClientSize.Height / 10;
            goals[0].x = ClientSize.Width/2 - goals[0].w/2;
            goals[0].y = 0;
            goals[0].clr = Color.Red;
            goals[0].balls = new List<ball>();
            goals[1] = new rect();
            goals[1].w = ClientSize.Width / 5;
            goals[1].h = ClientSize.Height / 10;
            goals[1].x = ClientSize.Width / 2 - goals[1].w / 2;
            goals[1].y = ClientSize.Height - goals[1].h;
            goals[1].clr = Color.Green;
            goals[1].balls = new List<ball>();
            ball pnn;
            for(int i = 0; i < n_balls; i++)
            {
                pnn = new ball();
                pnn.y = goals[1].y + (i / (n_balls / 2)) * pnn.img.Height;
                pnn.x = goals[1].x + (i % (n_balls / 2)) * pnn.img.Width;
                goals[1].balls.Add(pnn);
            }
            draw(this.CreateGraphics());
        }
        void draw(Graphics g)
        {
            Bitmap img = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g2 = Graphics.FromImage(img);
            g2.Clear(BackColor);
            for(int i = 0; i < 2; i++)
            {
                g2.FillRectangle(new SolidBrush(goals[i].clr), goals[i].x, goals[i].y, goals[i].w, goals[i].h);
            }
            for(int i = 0; i < 2;i++)
            {
                for(int j = 0; j < goals[i].balls.Count; j++)
                {
                    g2.DrawImage(goals[i].balls[j].img, goals[i].balls[j].x, goals[i].balls[j].y);
                }
            }
            g2.DrawImage(p.imgs[p.currImg], p.x, p.y);

            g.DrawImage(img, 0, 0);
        }
    }
}