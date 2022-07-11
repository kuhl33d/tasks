namespace assignment_6
{
    public partial class Form1 : Form
    {
        bool D = false;
        int R, G;
        public Form1()
        {
            this.Text = this.BackColor.R.ToString() + "," + this.BackColor.G.ToString() + "," + this.BackColor.B.ToString();
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseUp += new MouseEventHandler(release);
        }
        int abs(int n)
        {
            if (n < 0)
                return (n * -1);
            return n;
        }
        void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            //(((e.X+(e.X-X))*255)/Width)
            if (D)
            {
                R = ((e.X * 255) / Width) % 255;
                G = ((e.Y * 255) / Height) % 255;
                if (R < 0)
                {
                    R *= -1;
                }
                if (G < 0)
                {
                    G *= -1;
                }
                this.BackColor = Color.FromArgb(R, G, 0);
            }
            this.Text = this.BackColor.R.ToString() + "," + this.BackColor.G.ToString() + "," + this.BackColor.B.ToString();
        }

        void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            D = true;
        }

        void release(object? sender, MouseEventArgs e)
        {
            D = false;
        }
    }
}