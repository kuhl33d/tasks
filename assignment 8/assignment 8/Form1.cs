namespace assignment_8
{
    public class node
    {
        public Form child,parent;
        public int dir;
        public node()
        {
            child=new Form();
            child.Opacity=0.5   ;
        }
        public void move(int w)
        {
            switch (dir)
            {
                case 1:
                    child.Location = new Point(child.Location.X+w,child.Location.Y);
                    if (child.Location.X >= parent.Location.X + child.Width)
                    {
                        dir = 2;
                    }
                    break;
                case 2:
                    child.Location = new Point(child.Location.X-w, child.Location.Y);
                    if (child.Location.X <= parent.Location.X)
                    {
                        dir = 1;
                    }
                    break;
                case 3:
                    child.Location = new Point(child.Location.X - w, child.Location.Y+w);
                    if (child.Location.X <= parent.Location.X - (child.Width*2) && child.Location.Y >= parent.Location.Y + child.Height)
                    {
                        dir = 4;
                    }
                    break;
                case 4:
                    child.Location = new Point(child.Location.X + w, child.Location.Y - w);
                    if (child.Location.X >= parent.Location.X - child.Width && child.Location.Y <= parent.Location.Y)
                    {
                        dir = 3;
                    }
                    break;
                case 5:
                    child.Location = new Point(child.Location.X + w, child.Location.Y + w);
                    if (child.Location.X >= parent.Location.X + parent.Width + child.Width && child.Location.Y >= parent.Location.Y + child.Height)
                    {
                        dir = 6;
                    }
                    break;
                case 6:
                    child.Location = new Point(child.Location.X - w, child.Location.Y - w);
                    if (child.Location.X <= parent.Location.X + parent.Width && child.Location.Y <= parent.Location.Y)
                    {
                        dir = 5;
                    }
                    break;
            }
        }
    }
    public partial class Form1 : Form
    {
        public List<node> childs = new List<node>();
        public node pnn;
        public Keys last;
        public Form1()
        {
            this.Width = this.Height = 500;
            this.CenterToScreen();
            this.KeyDown += new KeyEventHandler(task);
        }
        public void task(object sender,KeyEventArgs k)
        {
            if(k.KeyCode == Keys.Enter && childs.Count == 0)
            {
                for(int i = 0;i < 8;i++)
                {
                    pnn = new node();
                    pnn.parent = this;
                    pnn.child.StartPosition = FormStartPosition.Manual;
                    if (i == 0 || i == 2)
                    {
                        pnn.dir = 1;
                        pnn.child.BackColor = Color.Red;
                    }
                    else if(i == 1 || i == 3)
                    {
                        pnn.dir = 2;
                        pnn.child.BackColor= Color.Aquamarine;
                    }
                    else if(i == 4)
                    {
                        pnn.dir = 3;
                        pnn.child.BackColor = Color.DarkGray;
                    }
                    else if(i == 5)
                    {
                        pnn.dir = 4;
                        pnn.child.BackColor = Color.BlueViolet;
                    }
                    else if(i == 6)
                    {
                        pnn.dir = 5;
                        pnn.child.BackColor = Color.DarkGray;

                    }
                    else if(i == 7)
                    {
                        pnn.dir = 6;
                        pnn.child.BackColor = Color.BlueViolet;
                    }
                    pnn.child.Width =  this.Width / 2;
                    pnn.child.Height = this.Height / 2;
                    if (i == 0)
                    {
                        pnn.child.Location = new Point(this.Location.X, this.Location.Y-pnn.child.Height);
                    }else if(i== 1)
                    {
                        pnn.child.Location = new Point(this.Location.X+pnn.child.Width, this.Location.Y - pnn.child.Height);
                    }else if (i == 2)
                    {
                        pnn.child.Location = new Point(this.Location.X, this.Location.Y + this.Height);
                    }else if(i == 3)
                    {
                        pnn.child.Location = new Point(this.Location.X + pnn.child.Width, this.Location.Y + this.Height);
                    }else if (i == 4)
                    {
                        pnn.child.Location = new Point(this.Location.X - pnn.child.Width, this.Location.Y);
                    }else if (i == 5)
                    {
                        pnn.child.Location = new Point(this.Location.X - (pnn.child.Width*2), this.Location.Y + pnn.child.Height);
                    }else if (i == 6)
                    {
                        pnn.child.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                    }else if (i == 7)
                    {
                        pnn.child.Location = new Point(this.Location.X + this.Width + pnn.child.Width, this.Location.Y + pnn.child.Height);
                    }
                    childs.Add(pnn);
                    pnn.child.Show();
                }
                this.Focus();
            }
            else if(k.KeyCode == Keys.D1|| k.KeyCode == Keys.D2 || k.KeyCode == Keys.D3)
            {
                last = k.KeyCode;
            }
            else if(k.KeyCode == Keys.Space && childs.Count != 0)
            {
                if(last == Keys.D1)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        childs[i].move(this.Width / 20);
                    }
                }else if(last == Keys.D2)
                {
                    for (int i = 4; i < 8; i++)
                    {
                        childs[i].move(this.Width / 20);
                    }
                }
                else if(last == Keys.D3)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        childs[i].move(this.Width / 20);
                    }
                }
            }
        }
    }
}