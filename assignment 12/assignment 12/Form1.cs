namespace assignment_12
{
    class circle
    {
        public int x, y, w, h;
        public Color clr;
    }
    public partial class Form1 : Form
    {
        List<circle> circles = new List<circle>();
        bool Drag = false;
        int oldX, oldY;
        int n1 = 4, n2 = 8, n3 = 5;
        Bitmap img;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                for (int i = 0; i < circles.Count; i++)
                {
                    circles[i].x += e.X - oldX;
                    circles[i].y += e.Y - oldY;
                }
                oldX = e.X;
                oldY = e.Y;
                draw(CreateGraphics());
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Drag = true;
                oldX = e.X;
                oldY = e.Y;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            img = new Bitmap(ClientSize.Width, ClientSize.Height);
            circle pnn;
            for (int i = 0; i < n1; i++)
            {
                pnn = new circle();
                pnn.x = i * 30;
                pnn.y = ClientSize.Height / 2;
                pnn.w = 25;
                pnn.h = 25;
                pnn.clr = Color.Gray;
                circles.Add(pnn);
            }
            for (int i = 0; i < n2; i++)
            {
                pnn = new circle();
                pnn.x = (n1 + i) * 30;
                pnn.w = 25;
                pnn.h = 25;
                pnn.y = ClientSize.Height / 2 - pnn.h*2;
                pnn.clr = Color.DeepSkyBlue;
                circles.Add(pnn);
                pnn = new circle();
                pnn.x = (n1 + i) * 30;
                pnn.w = 25;
                pnn.h = 25;
                pnn.y = ClientSize.Height / 2 + pnn.h*2;
                pnn.clr = Color.SkyBlue;
                circles.Add(pnn);
            }
            for (int i = 0; i < n3; i++)
            {
                pnn = new circle();
                pnn.x = (n1+n2 + i) * 30;
                pnn.y = ClientSize.Height / 2;
                pnn.w = 25;
                pnn.h = 25;
                pnn.clr = Color.DimGray;
                circles.Add(pnn);
            }
            draw(CreateGraphics());
        }
        void draw(Graphics G)
        {
            Graphics G2 = Graphics.FromImage(img);
            G2.Clear(BackColor);
            for (int i = 0; i < circles.Count; i++)
            {
                G2.FillEllipse(new SolidBrush(circles[i].clr), circles[i].x, circles[i].y, circles[i].w, circles[i].h);
            }
            G.DrawImage(img, 0, 0);
        }
    }
}