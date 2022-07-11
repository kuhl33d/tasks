using System.Windows.Forms;
namespace pac_man
{
    class enemie
    {
        public System.Windows.Forms.Timer animator;
        public int r, c, speed,dir,p_dir;
        public Color clr;
        public bool alive;
        map grid;
        pacman P;
        int ct;
        public Random R;
        public enemie(map grid,pacman P)
        {
            R = new Random();
            this.grid = grid;
            this.P = P;
            alive = true;
            speed = 1;
            animator = new System.Windows.Forms.Timer();
            animator.Tick += anim;
            animator.Interval = 200;
            animator.Start();
        }
        ~enemie()
        {
            animator.Stop();
        }
        void anim(object? sender, EventArgs e)
        {
            int[] d;
            if(dir == 0)
            {
                if(legal(r, c - 1) == false)
                {
                    d = new int[] {1,2,3};
                    while (true)
                    {
                        dir = d[R.Next(3)];
                        if(dir == 1)
                        {
                            if(legal(r-1,c) == true)
                            {
                                break;
                            }
                        }
                        else if(dir == 2)
                        {
                            if (legal(r, c+1) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 3)
                        {
                            if (legal(r + 1, c) == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (dir == 1)
            {
                if (legal(r - 1, c) == false)
                {
                    d = new int[] { 0, 2, 3 };
                    while (true)
                    {
                        dir = d[R.Next(3)];
                        if (dir == 0)
                        {
                            if (legal(r, c-1) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 2)
                        {
                            if (legal(r, c + 1) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 3)
                        {
                            if (legal(r + 1, c) == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (dir == 2)
            {
                if (legal(r, c + 1) == false)
                {
                    d = new int[] { 0, 1, 3 };
                    while (true)
                    {
                        dir = d[R.Next(3)];
                        if (dir == 0)
                        {
                            if (legal(r, c - 1) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 1)
                        {
                            if (legal(r - 1, c) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 3)
                        {
                            if (legal(r + 1, c) == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (dir == 3)
            {
                if (legal(r + 1, c) == false)
                {
                    d = new int[] { 0, 1, 2 };
                    while (true)
                    {
                        dir = d[R.Next(3)];
                        if (dir == 0)
                        {
                            if (legal(r, c - 1) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 2)
                        {
                            if (legal(r, c + 1) == true)
                            {
                                break;
                            }
                        }
                        else if (dir == 1)
                        {
                            if (legal(r - 1, c) == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (legal(r, c - 1) == true && legal(r, c + 1) == true)
            {
                d = new int[] { 0, 1, dir };
                dir = d[R.Next(3)];
            }
            move();
        }
        bool legal(int r,int c)
        {
            if(r <0 || c < 0 || c > 27 || r > 30)
            {
                return false;
            }
            if(grid.m[r,c].wall == true || grid.m[r, c].gate == true)
            {
                return false;
            }
            return true;
        }
        public void move()
        {
            if (c == 0)
            {
                dir = 2;
            }
            else if (c == 27)
            {
                dir = 0;
            }
            if (dir == 0)
            {
                if (grid.m[r, c - 1].wall == false)
                {
                    c -= speed;
                }
            }
            else if (dir == 2)
            {
                if (grid.m[r, c + 1].wall == false)
                {
                    c += speed;
                }
            }
            else if (dir == 1)
            {
                if (grid.m[r - 1, c].wall == false)
                {
                    r -= speed;
                }
            }
            else if (dir == 3)
            {
                if (grid.m[r + 1, c].wall == false && grid.m[r + 1, c].gate == false)
                {
                    r += speed;
                }
            }

        }
    }
    class pacman
    {
        public System.Windows.Forms.Timer animator;
        public int dir;
        public int r,c,speed;
        map grid;
        public int score;
        public bool isalive;
        public bool power;
        public int power_length;
        public pacman(map grid)
        {
            power_length= 0;
            power = false;
            isalive = true;
            this.grid = grid;
            score = 0;
            dir = 0;
            r = 23; 
            c = 13; 
            speed = 1;
            animator = new System.Windows.Forms.Timer();
            animator.Tick += anim;
            animator.Interval = 200;
            animator.Start();
        }
        ~pacman()
        {
            animator.Stop();
        }
        private void anim(object? sender, EventArgs e)
        {
            if(power == true)
            {
                power_length -= animator.Interval;
                if(power_length <= 0)
                {
                    power_length = 0;
                    power = false;
                }
            }
            if(isalive == true)
            {
                move();
                if(grid.m[r,c].dot == true && grid.m[r, c].eaten == false)
                {
                    score += 100;
                    grid.m[r, c].eaten = true;
                    grid.dots--;
                }
                else if(grid.m[r, c].bigdot == true && grid.m[r, c].eaten == false)
                {
                    power = true;
                    power_length = animator.Interval * 50;
                    grid.m[r, c].eaten = true;
                    grid.dots--;
                }
            }
        }

        public void move()
        {
            if(c == 0)
            {
                c = 26;
            }else if(c == 27)
            {
                c = 1;
            }
            if(dir == 0)
            {
                if(grid.m[r,c-1].wall == false)
                {
                    c -= speed;
                }
            }
            else if(dir == 2)
            {
                if (grid.m[r, c+1].wall == false)
                {
                    c += speed;
                }
            }
            else if(dir == 1)
            {
                if (grid.m[r-1,c].wall == false)
                {
                    r -= speed;
                }
            }
            else if (dir == 3)
            {
                if (grid.m[r + 1,c].wall == false && grid.m[r + 1, c].gate == false)
                {
                    r += speed;
                }
            }

        }
    }
    class tile
    {
        public int x, y, w, h;
        public bool gate=false, wall = false, dot = false, bigdot = false, eaten = false;
        public tile(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class map
    {
        public tile[,] m;
        public int r, c,dots=0;
        public map(int r,int c)
        {
            m = new tile[r,c];
            this.r = r;
            this.c = c;
            int [,] defined = {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
                {1,3,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,3,1},
                {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1},
                {1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1},
                {1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1},
                {1,1,1,1,1,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,1,1,1,1,1},
                {0,0,0,0,0,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,0,0,0,0,0},
                {0,0,0,0,0,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,0,0,0,0,0},
                {0,0,0,0,0,1,2,1,1,0,1,1,1,4,4,1,1,1,0,1,1,2,1,0,0,0,0,0},
                {1,1,1,1,1,1,2,1,1,0,1,0,0,0,0,0,0,1,0,1,1,2,1,1,1,1,1,1},
                {0,0,0,0,0,0,2,0,0,0,1,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,0},
                {1,1,1,1,1,1,2,1,1,0,1,0,0,0,0,0,0,1,0,1,1,2,1,1,1,1,1,1},
                {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0},
                {0,0,0,0,0,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,0,0,0,0,0},
                {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0},
                {1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1},
                {1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
                {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
                {1,3,2,2,1,1,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,1,1,2,2,3,1},
                {1,1,1,2,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,2,1,1,1},
                {1,1,1,2,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,2,1,1,1},
                {1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1},
                {1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1},
                {1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1},
                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
            for (int i = 0; i<r; i++)
		    {
                for (int j = 0; j < c; j++)
                {
                    m[i, j] = new tile(i,j);
                    switch (defined[i, j])
                    {
                        case 1:
                            m[i, j].wall = true;
                            break;
                        case 2:
                            m[i, j].dot = true;
                            dots++;
                            break;
                        case 3:
                            m[i,j].bigdot = true;
                            dots++;
                            break;
                        case 4:
                            m[i,j].gate = true;
                            break;

                    }
                }
		    }
        }
    }
    public partial class Form1 : Form
    {
        List<enemie> ghosts = new List<enemie>();
        pacman P;
        int W , H;
        int offset_X, offset_Y;
        map grid = new map(31, 28);
        Bitmap screen;
        Graphics g;
        System.Windows.Forms.Timer drawer = new System.Windows.Forms.Timer();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            drawer.Tick += Drawer_Tick;
            this.KeyDown += Form1_KeyDown;
            drawer.Start();
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (grid.m[(int)P.r, (int)P.c - 1].wall == false)
                    {
                        P.dir = 0;
                    }
                    break;
                case Keys.Up:
                    if (grid.m[(int)P.r-1, (int)P.c].wall == false)
                    {
                        P.dir = 1;
                    }
                    break;
                case Keys.Right:
                    if (grid.m[(int)P.r, (int)P.c + 1].wall == false)
                    {
                        P.dir = 2;
                    }
                    break;
                case Keys.Down:
                    if (grid.m[(int)P.r + 1, (int)P.c].wall == false && grid.m[(int)P.r + 1, (int)P.c].gate == false)
                    {
                        P.dir = 3;
                    }
                    break;
            }
        }

        private void Drawer_Tick(object? sender, EventArgs e)
        {
            if (P.isalive == true)
            {
                draw(CreateGraphics());
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(CreateGraphics());
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            screen = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            g = Graphics.FromImage(screen);
            W = 20;
            H = 20;
            offset_Y = ClientSize.Width/2 - grid.c*W/2;
            offset_X = ClientSize.Height/2 - grid.r*H/2;
            P = new pacman(grid);
            Color[] clrs =
            {
                Color.Red,
                Color.Cyan,
                Color.Pink,
                Color.BlueViolet
            };
            for(int i = 0; i < clrs.Length; i++)
            {
                enemie g = new enemie(grid,P);
                if(i == 0)
                {
                    g.r = 1;
                    g.c = g.R.Next(12);
                    g.dir = 0;
                }
                else if(i == 1)
                {
                    g.r = g.R.Next(1, 25);
                    g.c = 6;
                    g.dir = 3;
                }
                else if(i == 2)
                {
                    g.r = 1;
                    g.c = g.R.Next(18, 25);
                    g.dir = 2;
                }
                else
                {
                    g.r = g.R.Next(9, 17);
                    g.c = 19;
                    g.dir = 3;
                }
                g.clr = clrs[i];
                ghosts.Add(g);
            }
        }
        void draw(Graphics G)
        {
            if(grid.dots == 0)
            {
                MessageBox.Show("you win Score: " + P.score);
                P.isalive = false;
                Form f = new menu();
                f.Show();
                this.Close();
            }
            g.Clear(BackColor);
            g.DrawString("score: "+P.score,new Font("Neon Pixel-7", 16),new SolidBrush(Color.White),offset_Y,offset_X-H*2);
            for(int i = 0; i < grid.r; i++)
            {
                for (int j = 0; j < grid.c; j++)
                {
                    if (grid.m[i, j].wall == true)
                    {
                        g.FillRectangle(new SolidBrush(Color.Blue), j * W + offset_Y, i * H + offset_X, W, H);
                        g.DrawRectangle(new Pen(Color.White), j * W + offset_Y, i * H + offset_X, W, H);
                    }
                    else if(grid.m[i, j].dot == true && grid.m[i, j].eaten == false)
                    {
                        g.FillEllipse(new SolidBrush(Color.Yellow), j * W+ W/3 + offset_Y, i * H+ H/3 + offset_X, W/4, H/4);
                    }
                    else if(grid.m[i, j].bigdot == true && grid.m[i, j].eaten == false)
                    {
                        g.FillEllipse(new SolidBrush(Color.White), j * W + offset_Y, i * H + offset_X, W, H);
                    }
                    else if(grid.m[i, j].gate == true)
                    {
                        g.FillRectangle(new SolidBrush(Color.Pink), j * W + offset_Y, i * H + offset_X + H/3, W, H/3);
                    }
                    if(P.r == i && P.c == j)
                    {
                        g.FillEllipse(new SolidBrush(Color.Yellow), j * W + offset_Y, i * H + offset_X, W, H);
                    }
                }
            }
            for(int i = 0; i < ghosts.Count; i++)
            {
                if(P.power == false)
                {
                    g.FillEllipse(new SolidBrush(ghosts[i].clr), ghosts[i].c * W + offset_Y, ghosts[i].r * H + offset_X, W, H);
                    ghosts[i].animator.Interval = 200;
                    if (ghosts[i].r==P.r && ghosts[i].c == P.c)
                    {
                        P.isalive = false;
                        P.animator.Stop();
                        for(int j = 0; j < ghosts.Count; j++)
                        {
                            ghosts[j].animator.Stop();
                        }
                        MessageBox.Show("you lose Score: " + P.score);
                        Form f = new menu();
                        f.Show();
                        this.Close();
                        break;
                    }
                }
                else
                {
                    g.FillEllipse(new SolidBrush(Color.Blue), ghosts[i].c * W + offset_Y, ghosts[i].r * H + offset_X, W, H);
                    ghosts[i].animator.Interval = 300;
                    if (ghosts[i].r == P.r && ghosts[i].c == P.c)
                    {
                        ghosts[i].alive = false;
                        ghosts[i].animator.Stop();
                        ghosts.RemoveAt(i);
                        P.score += 1000;
                    }
                }
            }
            G.DrawImage(screen, 0, 0);
        }
    }
}