namespace assignment_11
{
    class point
    {
        public int x, y;
    }
    public partial class Form1 : Form
    {
        List<point> up = new List<point>();
        List<point> down = new List<point>();
        int line, ct = 0,i1=-1,j1=-1;
        public Form1()
        {
            line = -1;
            this.MouseDown += Form1_MouseDown;
            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Draw(CreateGraphics());
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (up.Count != 0)
            {
                
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        j1 = -1;
                        if (i1 + 1 == up.Count)
                        {
                            i1 = -1;
                        }
                        i1++;
                        break;
                    case Keys.Down:
                        j1 = -1;
                        if (i1 - 1 < 0)
                        {
                            i1 = up.Count;
                        }
                        i1--;
                        break;
                    case Keys.Right:
                        i1 = -1;
                        if (j1 + 1 == up.Count)
                        {
                            j1 = -1;
                        }
                        j1++;
                        break;
                    case Keys.Left:
                        i1 = -1;
                        if (j1 - 1 < 0)
                        {
                            j1 = up.Count;
                        }
                        j1--;
                        break;
                }
            }
            Draw(CreateGraphics());
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(line == -1)
                {
                    line = e.Y;
                }
                else
                {
                    point pnn = new point();
                    pnn.x = e.X;
                    pnn.y = e.Y;
                    if (ct % 2 == 0)
                    {
                        if(e.Y < line)
                        {
                            up.Add(pnn);
                            ct++;
                        }
                        else
                        {
                            MessageBox.Show("error");
                        }
                    }
                    else
                    {
                        if (e.Y > line)
                        {
                            down.Add(pnn);
                            ct++;
                        }
                        else
                        {
                            MessageBox.Show("error");
                        }
                    }
                }
            }
            Draw(CreateGraphics());
        }
        void Draw(Graphics G)
        {
            G.Clear(BackColor);
            if(line != -1)
            {
                G.DrawLine(new Pen(Color.Black, 4), 0, line, ClientSize.Width, line);
            }
            for(int i = 0; i < up.Count; i++)
            {
                if(i==i1)
                    G.FillEllipse(new SolidBrush(Color.DeepSkyBlue), up[i].x, up[i].y,8,8);
                else
                    G.FillEllipse(new SolidBrush(Color.DimGray), up[i].x, up[i].y, 8, 8);
            }
            for (int i = 0; i < down.Count; i++)
            {
                if (i == j1)
                    G.FillEllipse(new SolidBrush(Color.DeepSkyBlue), down[i].x, down[i].y, 8, 8);
                else
                    G.FillEllipse(new SolidBrush(Color.DimGray), down[i].x, down[i].y, 8, 8);
            }
        }
    }
}