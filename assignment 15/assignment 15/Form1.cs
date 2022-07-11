namespace assignment_15
{
    class egg
    {
        public int x, y, curImg, direction;
    }
    class coin
    {
        public int x, y;
    }
    class micky
    {
        public int x, y, curImg;
        public Bitmap[] pics;
        public micky()
        {
            curImg = 0;
            pics = new Bitmap[4];
            for (int i = 0; i < 4; i++)
            {
                pics[i] = new Bitmap((i + 1) + ".bmp");
                pics[i].MakeTransparent(pics[i].GetPixel(0, 0));
            }
        }
    }
    public partial class Form1 : Form
    {
        int l = 0;
        micky M;
        Bitmap[] coins_pics = new Bitmap[4];
        Bitmap[] eggs_pics = new Bitmap[3];
        coin[] coins = new coin[4];
        List<egg> eggs = new List<egg>();
        Random r = new Random();
        public Form1()
        {
            WindowState = FormWindowState.Maximized;
            for (int i = 0; i < 4; i++)
            {
                coins_pics[i] = new Bitmap("c" + (i + 1) + ".bmp");
                coins_pics[i].MakeTransparent(coins_pics[i].GetPixel(0, 0));
                if (i < 3)
                {
                    eggs_pics[i] = new Bitmap("e" + (i + 1) + ".bmp");
                    eggs_pics[i].MakeTransparent(eggs_pics[i].GetPixel(0, 0));
                }
                coins[i] = new coin();
            }
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Draw(CreateGraphics());
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && l == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (e.X >= coins[i].x && e.X <= coins[i].x + coins_pics[i].Width && e.Y >= coins[i].y && e.Y <= coins[i].y + coins_pics[i].Height)
                    {
                        egg pnn = new egg();
                        pnn.x = e.X;
                        pnn.curImg = r.Next(3);
                        switch (i)
                        {
                            case 0:
                                pnn.y = M.y - eggs_pics[0].Height;
                                pnn.direction = 1;
                                break;
                            case 1:
                                pnn.y = M.y - eggs_pics[0].Height;
                                pnn.direction = 0;
                                break;
                            case 2:
                                pnn.y = M.y + M.pics[0].Height - eggs_pics[0].Height - 10;
                                pnn.direction = 1;
                                break;
                            case 3:
                                pnn.y = M.y + M.pics[0].Height - eggs_pics[0].Height - 10;
                                pnn.direction = 0;
                                break;
                        }
                        eggs.Add(pnn);
                        switch (i)
                        {
                            case 0:
                                coins[i].y = ClientSize.Height / 4;
                                coins[i].x = r.Next(coins_pics[0].Width, M.x - coins_pics[0].Width);
                                break;
                            case 1:
                                coins[i].y = ClientSize.Height / 4;
                                coins[i].x = r.Next(M.x + M.pics[0].Width + coins_pics[0].Width, ClientSize.Width - coins_pics[0].Width);
                                break;
                            case 2:
                                coins[i].y = 3 * ClientSize.Height / 4;
                                coins[i].x = r.Next(coins_pics[0].Width, M.x - coins_pics[0].Width);
                                break;
                            case 3:
                                coins[i].y = 3 * ClientSize.Height / 4;
                                coins[i].x = r.Next(M.x + M.pics[0].Width + coins_pics[0].Width, ClientSize.Width - coins_pics[0].Width);
                                break;
                        }
                        Draw(CreateGraphics());
                    }
                }
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    M.curImg = 0;
                    break;
                case Keys.Up:
                    M.curImg = 1;
                    break;
                case Keys.Down:
                    M.curImg = 2;
                    break;
                case Keys.Left:
                    M.curImg = 3;
                    break;
                case Keys.Space:
                    if (l == 1 && eggs.Count != 0)
                    {
                        l = 0;
                    }
                    if (l == 0)
                    {
                        Anim();
                    }
                    break;
            }
            Draw(CreateGraphics());
        }
        void CreateCoins()
        {
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        coins[i].y = ClientSize.Height / 4;
                        coins[i].x = r.Next(coins_pics[0].Width, M.x - coins_pics[0].Width);
                        break;
                    case 1:
                        coins[i].y = ClientSize.Height / 4;
                        coins[i].x = r.Next(M.x + M.pics[0].Width + coins_pics[0].Width, ClientSize.Width - coins_pics[0].Width);
                        break;
                    case 2:
                        coins[i].y = 3 * ClientSize.Height / 4;
                        coins[i].x = r.Next(coins_pics[0].Width, M.x - coins_pics[0].Width);
                        break;
                    case 3:
                        coins[i].y = 3 * ClientSize.Height / 4;
                        coins[i].x = r.Next(M.x + M.pics[0].Width + coins_pics[0].Width, ClientSize.Width - coins_pics[0].Width);
                        break;
                }
            }
        }
        void Anim()
        {
            for (int i = 0; i < eggs.Count; i++)
            {
                if (eggs[i].direction == 1)
                {
                    eggs[i].x += 7;
                    eggs[i].curImg++;
                    eggs[i].curImg %= 3;
                    if (eggs[i].x >= M.x - 10)
                    {
                        MessageBox.Show("egg had reached mickey");
                        l = 1;
                        break;
                    }
                }
                else
                {
                    eggs[i].x -= 7;
                    eggs[i].curImg--;
                    if (eggs[i].curImg == -1)
                    {
                        eggs[i].curImg = 2;
                    }
                    if (eggs[i].x + 10 <= M.x + M.pics[0].Width)
                    {
                        MessageBox.Show("egg had reached mickey");
                        l = 1;
                        break;
                    }
                }
            }
            if (l == 1)
            {
                eggs.Clear();
                CreateCoins();
            }
            Draw(CreateGraphics());
        }
        private void Form1_Load(object? sender, EventArgs e)
        {
            M = new micky();
            M.x = ClientSize.Width / 2 - M.pics[0].Width / 2;
            M.y = ClientSize.Height / 2 - M.pics[0].Height / 2;
            int n;
            egg pnn;
            n = r.Next(1, 5);
            for (int i = 0; i < n; i++)
            {
                pnn = new egg();
                pnn.direction = 1;
                pnn.curImg = r.Next(3);
                pnn.y = M.y - eggs_pics[0].Height;
                pnn.x = r.Next(0, M.x - 50);
                eggs.Add(pnn);
            }
            n = r.Next(1, 5);
            for (int i = 0; i < n; i++)
            {
                pnn = new egg();
                pnn.direction = 1;
                pnn.curImg = r.Next(3);
                pnn.y = M.y + M.pics[0].Height - eggs_pics[0].Height - 10;
                pnn.x = r.Next(0, M.x - 50);
                eggs.Add(pnn);
            }
            n = r.Next(1, 5);
            for (int i = 0; i < n; i++)
            {
                pnn = new egg();
                pnn.direction = 0;
                pnn.curImg = r.Next(3);
                pnn.y = M.y - eggs_pics[0].Height;
                pnn.x = r.Next(M.x + M.pics[0].Width + 50, ClientSize.Width);
                eggs.Add(pnn);
            }
            n = r.Next(1, 5);
            for (int i = 0; i < n; i++)
            {
                pnn = new egg();
                pnn.direction = 0;
                pnn.curImg = r.Next(3);
                pnn.y = M.y + M.pics[0].Height - eggs_pics[0].Height - 10;
                pnn.x = r.Next(M.x + M.pics[0].Width + 50, ClientSize.Width);
                eggs.Add(pnn);
            }
            Draw(CreateGraphics());
        }

        void Draw(Graphics G)
        {
            Bitmap img = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(img);
            g.Clear(BackColor);
            g.DrawImage(M.pics[M.curImg], M.x, M.y);
            g.FillRectangle(new SolidBrush(Color.DarkCyan), 0, M.y, M.x, 10);
            g.FillRectangle(new SolidBrush(Color.DarkCyan), M.x + M.pics[0].Width, M.y, M.x, 10);
            g.FillRectangle(new SolidBrush(Color.DarkCyan), 0, M.y + M.pics[0].Height - 10, M.x, 10);
            g.FillRectangle(new SolidBrush(Color.DarkCyan), M.x + M.pics[0].Width, M.y + M.pics[0].Height - 10, M.x, 10);
            for (int i = 0; i < eggs.Count; i++)
            {
                g.DrawImage(eggs_pics[eggs[i].curImg], eggs[i].x, eggs[i].y);
            }
            for (int i = 0; i < 4 && l == 1; i++)
            {
                g.DrawImage(coins_pics[i], coins[i].x, coins[i].y);
            }
            G.DrawImage(img, 0, 0);
        }
    }
}

