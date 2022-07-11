namespace assignment_5
{
    public partial class Form1 : Form
    {
        public class node 
        {
            public Form child;
            public node()
            {
                child = new Form();
            }
        }
        public int rclicks = 0,X,Y;
        public List<node> childs = new List<node>();
        public Form1()
        {
            MessageBox.Show("each right-click will generate forms column on each side sequentially\nmove move will move the forms with in");
            this.Width = 400;
            this.Height = 400;
            this.CenterToScreen();
            this.MouseDown += new MouseEventHandler(CreateChilds);
            this.MouseMove += new MouseEventHandler(MoveChilds);

        }
        public void CreateChilds(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) 
            {
                if (rclicks == 0) 
                {
                    X= e.X;
                    Y= e.Y;
                }
                node pnn;
                for (int i = 0; i < 4; i++) {
                    pnn = new node();
                    pnn.child.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    pnn.child.Width = this.Width / 4;
                    pnn.child.Height = this.Height / 4;
                    if (rclicks % 2 == 0)
                    {
                        //left
                        pnn.child.Location = new Point(this.Location.X - pnn.child.Width - ((childs.Count/8) * pnn.child.Width), this.Location.Y + (i * pnn.child.Height) +Y);
                        pnn.child.BackColor = Color.Yellow;
                    }
                    else 
                    {
                        //right
                        pnn.child.Location = new Point(this.Location.X + this.Width + ((childs.Count / 8) * pnn.child.Width), this.Location.Y + (i * pnn.child.Height)+Y);
                        pnn.child.BackColor = Color.Red;
                    }
                    pnn.child.Show();
                    childs.Add(pnn);
                }
                rclicks++;
                this.Focus();
            }
        }
        public void MoveChilds(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].child.Location = new Point(childs[i].child.Location.X, childs[i].child.Location.Y + (e.Y - Y));
            }
            X=e.X;
            Y=e.Y;
        }
    }
}