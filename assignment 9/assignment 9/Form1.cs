namespace assignment_9
{
    public class Rect
    {
        public int x, y, w, h;
        public Color clr;
        public int dir;
        public Rect Parent;
        public void anim(int dx,int dy)
        {
            switch (dir)
            {
                case 0:
                    y += dy;
                    if (y >= Parent.y + Parent.h)
                        dir++;
                    break;
                case 1:
                    x += dx;
                    if (x >= Parent.x + Parent.w)
                        dir++;
                    break;
                case 2:
                    y -= dy;
                    if (y <= Parent.y - h)
                        dir++;
                    break;
                case 3:
                    x -= dx;
                    if (x <= Parent.x - w)
                        dir = 0;
                    break;
            }
        }
    }


    public partial class Form1 : Form
    {
        List<Rect> childs = new List<Rect>();
        Rect pnn;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
        }
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
        void Form1_Load(object sender, EventArgs e)
        {
            pnn = new Rect();
            pnn.w = 400;
            pnn.h = 400;
            pnn.x = ClientSize.Width/2 - pnn.w/2;
            pnn.y = ClientSize.Height/2 - pnn.h/2;
            pnn.dir = -1;
            pnn.clr = Color.DimGray;
            pnn.Parent = pnn;
            childs.Add(pnn);
            Draw(CreateGraphics());
        }

        void Draw(Graphics g)
        {
            g.Clear(BackColor);
            for (int i = 0; i < childs.Count; i++)
            {
                g.FillRectangle(new SolidBrush(childs[i].clr), childs[i].x, childs[i].y, childs[i].w, childs[i].h);
            }
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    for(int i = 1; i < childs.Count; i++)
                    {
                        childs[i].anim(childs[0].w/10,childs[0].h/10);
                    }
                    break;
                case Keys.Up:
                    pnn = new Rect();
                    pnn.w = childs[0].w/5;
                    pnn.h = childs[0].h/5;
                    pnn.x = childs[0].x-pnn.w;
                    pnn.y = childs[0].y-pnn.h;
                    pnn.dir = 0;
                    pnn.clr = Color.Cyan;
                    pnn.Parent = childs[0];
                    if (safe(pnn, childs))
                    {
                        childs.Add(pnn);
                    }
                    break;
                case Keys.Left:
                    pnn = new Rect();
                    pnn.w = childs[0].w / 5;
                    pnn.h = childs[0].h / 5;
                    pnn.x = childs[0].x - pnn.w;
                    pnn.y = childs[0].y + childs[0].h;
                    pnn.dir = 1;
                    pnn.clr = Color.DarkCyan;
                    pnn.Parent = childs[0];
                    if (safe(pnn, childs))
                    {
                        childs.Add(pnn);
                    }
                    break;
                case Keys.Down:
                    pnn = new Rect();
                    pnn.w = childs[0].w / 5;
                    pnn.h = childs[0].h / 5;
                    pnn.x = childs[0].x + childs[0].w;
                    pnn.y = childs[0].y + childs[0].h;
                    pnn.dir = 2;
                    pnn.clr = Color.DeepSkyBlue;
                    pnn.Parent = childs[0];
                    if (safe(pnn, childs))
                    {
                        childs.Add(pnn);
                    }
                    break;
                case Keys.Right:
                    pnn = new Rect();
                    pnn.w = childs[0].w / 5;
                    pnn.h = childs[0].h / 5;
                    pnn.x = childs[0].x + childs[0].w;
                    pnn.y = childs[0].y - pnn.h;
                    pnn.dir = 3;
                    pnn.clr = Color.DeepSkyBlue;
                    pnn.Parent = childs[0];
                    if (safe(pnn, childs))
                    {
                        childs.Add(pnn);
                    }
                    break;
            }
            Draw(CreateGraphics());
        }
        bool safe(Rect pnn,List<Rect> childs)
        {
            for(int i=1;i<childs.Count; i++)
            {
                if(Math.Abs(pnn.x-childs[i].x) < pnn.w && Math.Abs(pnn.y - childs[i].y) < pnn.h)
                {
                    Text = "collide "+pnn.dir+" !!!";
                    return false;
                }
            }
            Text = "";
            return true;
        }
    }
}