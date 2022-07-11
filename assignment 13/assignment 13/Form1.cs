namespace assignment_13
{
    class Container
    {
        public int Rects;
        public Container()
        {
            Rects = 0;
        }
    }
    public partial class Form1 : Form
    {
        Bitmap img;
        Container Left=new Container();
        Container Right=new Container();
        int elipse = 0;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(paint);
            this.Load += new EventHandler(load);
            this.MouseDown += new MouseEventHandler(down);
            this.KeyDown += new KeyEventHandler(kdown);
        }
        void kdown(object sender, KeyEventArgs k)
        {
            if(k.KeyCode == Keys.Enter)
            {
                if(Right.Rects+elipse == Left.Rects)
                {
                    MessageBox.Show("OK");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }
        void down(object sender,MouseEventArgs e)
        {
            if (e.X > this.ClientSize.Width / 2)
            {
                if(e.Button == MouseButtons.Left)
                {
                    elipse++;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if(elipse > 0)
                        elipse--;

                }
                Draw(CreateGraphics());
            }
        }
        void paint(object sender,PaintEventArgs p)
        {
            Draw(p.Graphics);
        }
        public void load(object sender,EventArgs e)
        {
            img = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Random R = new Random();
            Left.Rects = R.Next(40);
            Right.Rects = R.Next(Left.Rects);
            Draw(CreateGraphics());
        }

        void Draw(Graphics m)
        {
            Graphics g = Graphics.FromImage(img);
            g.Clear(BackColor);
            g.DrawRectangle(new Pen(Color.Red,10), 0, 0, this.ClientSize.Width / 2, this.ClientSize.Height);
            g.DrawRectangle(new Pen(Color.Blue,10), this.ClientSize.Width / 2+10, 0, this.ClientSize.Width / 2, this.ClientSize.Height);
            
            for (int i=0;i<Left.Rects;i++)
            {
                g.FillRectangle(new SolidBrush(Color.Tan), (i%7) * (this.ClientSize.Width / 14) + 5, (i / 7) * (this.ClientSize.Height / 7)+5, this.ClientSize.Width / 14 - 10, this.ClientSize.Height / 7 - 10);
            }
            for (int i = 0; i < Right.Rects+elipse; i++)
            {
                if (i >= Right.Rects)
                {
                    g.FillEllipse(new SolidBrush(Color.Violet), (i % 7) * (this.ClientSize.Width / 14) + (this.ClientSize.Width / 2) + 15, (i / 7) * (this.ClientSize.Height / 7) + 5, this.ClientSize.Width / 14 - 10, this.ClientSize.Height / 7 - 10);

                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.SteelBlue), (i % 7) * (this.ClientSize.Width / 14) + (this.ClientSize.Width / 2) + 15, (i / 7) * (this.ClientSize.Height / 7) + 5, this.ClientSize.Width / 14 - 10, this.ClientSize.Height / 7 - 10);
                }
            }
            m.DrawImage(img,0,0);
        }
    }
}