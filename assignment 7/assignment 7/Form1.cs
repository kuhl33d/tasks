namespace assignment_7
{
    public partial class Form1 : Form
    {
        public List<Color> Colors = new List<Color>();
        public int rclick = 0, R = 0, G = 0, B = 0,CurColor=-1;
        public Form1()
        {
            this.Width = this.Height = 500;
            this.CenterToScreen();
            this.Text = "R: "+R.ToString()+" G: "+G.ToString()+" B: "+B.ToString();
            MessageBox.Show("each 3 right-click save the X-values as RGB\nsave only the X-Values as 255\neach up or down arrow show the color");
            this.MouseDown += new MouseEventHandler(saveColor);
            this.MouseMove += new MouseEventHandler(showColor);
            this.KeyDown += new KeyEventHandler(changeColor);
        }
        public void showColor(object sender, MouseEventArgs e)
        {
            switch (rclick % 3)
            {
                case 0:
                    R = (e.X * 255) / this.Width;
                    break;
                case 1:
                    G = (e.X * 255) / this.Width;
                    break;
                case 2:
                    B = (e.X * 255) / this.Width;
                    break;
            }
            this.Text = "R: " + R.ToString() + " G: " + G.ToString() + " B: " + B.ToString();
            this.BackColor = Color.FromArgb(R,G,B);
        }
        public void saveColor(object sender, MouseEventArgs e) 
        { 
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                rclick++;
                if(rclick % 3 == 0)
                {
                    Colors.Add(this.BackColor);
                    rclick = 0;
                    R = G = B = 0;
                }
            }
        }
        public void changeColor(object sender, KeyEventArgs k)
        {
            if(Colors.Count != 0)
            {
                if(k.KeyCode == Keys.Up)
                {
                    this.BackColor = Colors[++CurColor % Colors.Count];
                    CurColor %= Colors.Count;
                    
                }
                else if(k.KeyCode == Keys.Down)
                {
                    if(CurColor-1 == -1)
                    {
                        CurColor = Colors.Count;
                    }
                    this.BackColor= Colors[--CurColor];
                    
                }
                this.Text = "R: "+this.BackColor.R.ToString()+" G: "+this.BackColor.G.ToString()+" B: "+this.BackColor.B.ToString();
            }
        }
    }
}