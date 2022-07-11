using System.Windows.Forms;
namespace assignment_18
{
    class star
    {
        public int x,y;
        public void anim(int d)
        {
            switch (d)
            {
                case 0:
                    x -= 20;
                    break;
                case 1:
                    x += 20;
                    break;
                default:
                    break;
            }
            y += 20;
        }
    }
    class ship
    {
        public int x, y,speed;
        public Bitmap img;
        public bool pressed,fire;
        public ship()
        {
            pressed = false;
            fire = false;
            speed = 0;
            img = new Bitmap("1.bmp");
            img.MakeTransparent(img.GetPixel(0,0));
        }
        public void anim(char d)
        {
            switch (d)
            {
                case 'l':
                    if(speed >= -20)
                    {
                        speed--;
                    }
                    break;
                case 'r':
                    if (speed <= 20)
                    {
                        speed ++;
                    }
                    break;
                default:
                    break;
            }
            x += speed;
        }
    }
    public partial class Form1 : Form
    {
        List<star> stars = new List<star>();
        Random R = new Random();
        static System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        Bitmap img;
        Bitmap asset_0 = new Bitmap("2.bmp");
        Graphics g;
        int T = 0;
        ship S = new ship();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            t.Tick += new EventHandler(tick);
            t.Interval = 100;
            t.Start();
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
        }

        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Space)
                S.pressed = false;
        }
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    S.anim('l');
                    S.pressed = true;
                    break;
                case Keys.Right:
                    S.anim('r');
                    S.pressed = true;
                    break;
                case Keys.Space:
                    S.fire = true;
                    break;
            }
            if (S.x <= 0)
            {
                S.speed = 0;
                S.x = 0;
            }
            else if (S.x >= ClientSize.Width - S.img.Width)
            {
                S.speed = 0;
                S.x = ClientSize.Width - S.img.Width;
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            S.x = ClientSize.Width / 2 - S.img.Width / 2;
            S.y = ClientSize.Height - S.img.Height;
            img = new Bitmap(ClientSize.Width, ClientSize.Height);
            g = Graphics.FromImage(img);
            asset_0.MakeTransparent(asset_0.GetPixel(0, 0));
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(this.CreateGraphics());
        }

        void tick(object sender,EventArgs e)
        {
            if(S.pressed == true)
            {
                if (S.speed < 0)
                    S.anim('l');
                else
                    S.anim('r');
            }
            if(S.pressed == false)
            {
                if(S.speed > 0)
                {
                    S.speed -=2;
                }else if(S.speed < 0)
                {
                    S.speed +=2;
                }
                if (S.speed == 1 || S.speed == -1)
                    S.speed = 0;
                S.anim('s');
                if (S.x <= 0)
                {
                    S.speed = 0;
                    S.x = 0;
                }
                else if (S.x >= ClientSize.Width - S.img.Width)
                {
                    S.speed = 0;
                    S.x = ClientSize.Width - S.img.Width;
                }
            }
            star pnn;
            int j=0;
            if (T%20 == 0)
            {
                j = R.Next(4);
                T = 0;
            }
            for (int i = 0; i < j; i++)
            {
                pnn = new star();
                pnn.y = 0;
                pnn.x = R.Next(0, ClientSize.Width - asset_0.Width);
                stars.Add(pnn);
            }
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].anim(R.Next(4));
                if(stars[i].y >= ClientSize.Height)
                {
                    stars.RemoveAt(i);
                }
            }
            T++;
            this.Text = "speed: " + S.speed;
            draw(this.CreateGraphics());
            if (S.fire)
            {
                for(int i = 0; i < stars.Count; i++)
                {
                    if((S.x + S.img.Width / 2) >= stars[i].x && (S.x + S.img.Width / 2) <= stars[i].x + asset_0.Width)
                    {
                        stars.RemoveAt(i);
                    }
                }
                S.fire = false;
            }

        }
        void draw(Graphics G)
        {
            g.Clear(this.BackColor);
            if (S.fire)
            {
                g.DrawLine(new Pen(Color.Yellow, 5), S.x + S.img.Width / 2, S.y, S.x + S.img.Width / 2, 0);
            }
            for(int i = 0; i < stars.Count; i++)
            {
                g.DrawImage(asset_0, stars[i].x, stars[i].y);
            }
            g.DrawImage(S.img, S.x, S.y);
            G.DrawImage(img, 0, 0);
        }
    }
}