namespace assignment_4
{
    public class node
    {
        public Form child,parent;
        public int dir;
        public node()
        {
            child = new Form();
        }
        public void move(int n)
        {
            switch (dir)
            {
                case 0:
                    child.Location = new Point(child.Location.X, child.Location.Y + n);
                    if (child.Location.Y >= parent.Location.Y + parent.Height)
                    {
                        dir++;
                        Form1.moves = 0;
                    }
                    break;
                case 1:
                    child.Location = new Point(child.Location.X + n, child.Location.Y);
                        if (child.Location.X >= parent.Location.X + parent.Width)
                        {
                            dir++;
                            Form1.moves = 0;
                        }
                    break;
                case 2:
                    child.Location = new Point(child.Location.X, child.Location.Y-n);
                    if (child.Location.Y <= parent.Location.Y-child.Height)
                    {
                        dir++;
                        Form1.moves = 0;
                    }
                    break;
                case 3:
                    child.Location = new Point(child.Location.X-n, child.Location.Y);
                    if (child.Location.X <= parent.Location.X - child.Width)
                    {
                        dir=0;
                        Form1.moves = 0;
                    }
                    break;
            }
        }
    }
    public partial class Form1 : Form
    {
        public List<node> childs = new List<node> ();
        public int[] dir = { 0, 1, 2, 3 };
        public int clicks = 0;
        public Color[] bgcolors = { Color.Red, Color.Green, Color.Blue ,Color.Black};
        public static int moves = 0;
        public Form1()
        {
            MessageBox.Show("each left-click will create form in the following order:\nleft top\nleft bottom\nright bottom\nright top\neach right-click will move the forms counter clock-wise");
            this.Width = 400;
            this.Height = 400;
            CenterToScreen();
            this.MouseDown += new MouseEventHandler(Down);
            this.LocationChanged += Form1_LocationChanged;
        }

        void Form1_LocationChanged(object sender, EventArgs e)
        {
            this.Text = this.Location.ToString ();
        }

        void Down(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(clicks < 4)
                {
                    node pnn = new node();
                    pnn.child.Width = 200;
                    pnn.child.Height = 200;
                    pnn.child.Location = this.Location;
                    pnn.parent = this;
                    pnn.child.Show();
                    if (childs.Count != 0)
                    { 
                        pnn.dir = childs.ElementAt(childs.Count - 1).dir+1;
                        if (pnn.dir == 4)
                            pnn.dir = 0;
                    }
                    else
                    {
                        pnn.dir = 0;
                    }
                    switch (pnn.dir)
                    {
                        case 0:
                            pnn.child.Location = new Point(this.Location.X - pnn.child.Width,this.Location.Y-pnn.child.Height);
                            break;
                        case 1:
                            pnn.child.Location = new Point(this.Location.X - pnn.child.Width, this.Location.Y + this.Height);
                            break;
                        case 2:
                            pnn.child.Location = new Point(this.Location.X + this.Width, this.Location.Y + this.Height);
                            break;
                        case 3:
                            pnn.child.Location = new Point(this.Location.X + this.Width, this.Location.Y-pnn.child.Height);
                            break;
                    }
                    pnn.move(Form1.moves * (pnn.parent.Width / 10));
                    pnn.child.BackColor = bgcolors[clicks];
                    childs.Add(pnn);
                    pnn.child.Text = pnn.child.Location.ToString();
                    clicks++;
                }
            }
            else
            {
                Form1.moves++;
                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].move(childs[i].parent.Width/10);
                }
            }
        }
    }
}