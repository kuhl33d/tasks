namespace assignment_3
{
    public struct n
    {
        public int X, Y;
    }
    public partial class Form1 : Form
    {
        public List<n> up = new List<n>();
        public List<n> down = new List<n>();
        public int ct = 0,Y;
        public bool pos = false;
        public n pnn;
        //0 up, 1 down
        public Form1()
        {
            MessageBox.Show("first left-click will determine the level\n=========================\nafter that each left-click will switch from up to down and save the position\n=========================\nright-click will display the point for each list");
            this.MouseDown += new MouseEventHandler(func);
        }
        void func(object sender,MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ct++;
                if(ct == 1)
                {
                    Y = e.Y;
                    this.Text = e.Y.ToString();
                }
                else
                {
                    if(pos)
                    {
                        if(e.Y > Y)
                        {
                            pnn = new n();
                            pnn.X = e.X;
                            pnn.Y = e.Y;
                            down.Add(pnn);
                            pos = !pos;
                        }
                        else
                        {
                            MessageBox.Show("error");
                        }
                    }
                    else
                    {
                        if (e.Y < Y)
                        {
                            pnn = new n();
                            pnn.X = e.X;
                            pnn.Y = e.Y;
                            up.Add(pnn);
                            pos = !pos;
                        }
                        else
                        {
                            MessageBox.Show("error");
                        }
                    }
                }
            }
            else
            {
                string st = "";
                st += "up:\n";
                for(int i = 0; i < up.Count; i++)
                {
                    st += up[i].X.ToString() + "," + up[i].X.ToString() + "\n";
                }
                MessageBox.Show(st);
                st = "";
                st += "down:\n";
                for (int i = 0; i < down.Count; i++)
                {
                    st += down[i].X.ToString() + "," + down[i].X.ToString() + "\n";
                }
                MessageBox.Show(st);
            }
        }
    }
}