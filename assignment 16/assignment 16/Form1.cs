namespace assignment_16
{
    class chick
    {
        public int x, y;
        public Bitmap img;
        public chick()
        {
            img = new Bitmap("1.bmp");
            img.MakeTransparent(img.GetPixel(0,0));
        }
    }
    class egg
    {
        public int x, y;
        public Bitmap img;
        public egg()
        {
            img = new Bitmap("3.bmp");
            img.MakeTransparent(img.GetPixel(0, 0));
        }
    }
    class platform
    {
        public int x, y;
        public Bitmap img;
        public List<egg> eggs;
        public platform()
        {
            img = new Bitmap("2.bmp");
            img.MakeTransparent(img.GetPixel(0, 0));
            eggs = new List<egg>();
        }
    }
    public partial class Form1 : Form
    {
        platform []platforms = new platform[3];
        chick chicken = new chick();
        List<egg> eggs = new List<egg>();
        bool isDrag = false;
        int oldx, oldy, which = -1;
        public Form1()
        {
            WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Paint += Form1_Paint;
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            isDrag = false;
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if(isDrag == true)
            {
                platforms[which].x += e.X-oldx;
                platforms[which].y += e.Y-oldy;
                for(int i = 0; i < platforms[which].eggs.Count; i++)
                {
                    platforms[which].eggs[i].x += e.X-oldx;
                    platforms[which].eggs[i].y += e.Y-oldy;
                }
                oldx = e.X;
                oldy = e.Y;
            }
            draw(this.CreateGraphics());
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                for(int i = 0; i < 3; i++)
                {
                    if(e.X >= platforms[i].x && e.X <= platforms[i].x + platforms[i].img.Width)
                    {
                        if(e.Y >= platforms[i].y && e.Y <= platforms[i].y + platforms[i].img.Width)
                        {
                            isDrag = true;
                            oldx = e.X;
                            oldy = e.Y;
                            which = i;
                            break;
                        }
                    }
                }
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if(chicken.x >= 10)
                    {
                        chicken.x -= 10;
                    }
                    break;
                case Keys.Right:
                    if(chicken.x <= ClientSize.Width - chicken.img.Width - 10)
                    {
                        chicken.x += 10;
                    }
                    break;
                case Keys.Space:
                    fire();
                    break;
            }
            draw(this.CreateGraphics());
        }
        void fire()
        {
            egg pnn = new egg();
            pnn.x = chicken.x+chicken.img.Width/2;
            pnn.y = -1;
            for (int i = 0; i < 3; i++)
            {
                if(platforms[i].x <= pnn.x && pnn.x <= platforms[i].x + platforms[i].img.Width)
                {
                    pnn.y = platforms[i].y-pnn.img.Height;
                    for(int j = 0; j < platforms[i].eggs.Count; j++)
                    {
                        if(platforms[i].eggs[j].x <= pnn.x && pnn.x <= platforms[i].eggs[j].x + platforms[i].eggs[j].img.Width)
                        {
                            pnn.y = platforms[i].eggs[j].y- platforms[i].eggs[j].img.Height;
                        }
                    }
                    platforms[i].eggs.Add(pnn);
                    break;
                }
            }
            if(pnn.y == -1)
            {
                pnn.y = ClientSize.Height - pnn.img.Height;
                eggs.Add(pnn);
            }
            draw(this.CreateGraphics());
        }
        private void Form1_Load(object? sender, EventArgs e)
        {
            chicken.x = ClientSize.Width/2;
            chicken.y = 0;
            for(int i = 0; i < 3; i++)
            {
                platforms[i] = new platform();
                platforms[i].x = ClientSize.Width/4 + (i* ClientSize.Width / 4 );
                platforms[i].y = ClientSize.Height / 2;

            }
            draw(this.CreateGraphics());
        }
        void draw (Graphics g)
        {
            Bitmap img = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g2 = Graphics.FromImage(img);
            g2.Clear(this.BackColor);
            g2.DrawImage(chicken.img,chicken.x,chicken.y);
            for(int i = 0; i < 3; i++)
            {
                g2.DrawImage(platforms[i].img, platforms[i].x,platforms[i].y);
                for(int j=0;j< platforms[i].eggs.Count; j++)
                {
                    g2.DrawImage(platforms[i].eggs[j].img, platforms[i].eggs[j].x, platforms[i].eggs[j].y);
                }
            }
            for(int i = 0; i < eggs.Count; i++)
            {
                g2.DrawImage(eggs[i].img, eggs[i].x, eggs[i].y);
            }
            g.DrawImage(img,0,0);
        }
    }
}