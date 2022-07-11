namespace assignment_10
{
    public partial class Form1 : Form
    {
        int x=-1, y=-1,x2=-1,y2=-1;
        Graphics g;
        public Form1()
        {
            g = this.CreateGraphics();
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            int t;
            if (x2 != -1)
            {
                if (x > x2)
                {
                    t = x;
                    x = x2;
                    x2 = t;
                }
                if(y > y2)
                {
                    t = y;
                    y = y2;
                    y2 = t;
                }
                g.Clear(BackColor);
                g.FillRectangle(new SolidBrush(Color.Orange), x, y, x2 - x, y2 - y);
                x = x2 = y = y2 = -1;
            }
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (x == -1)
            {
                x = e.X;
                y = e.Y;
                g.Clear(BackColor);
                g.FillEllipse(new SolidBrush(Color.DarkBlue), x, y, 5, 5);
            }
            else
            {
                x2 = e.X;
                y2 = e.Y;
            }
        }
    }
}