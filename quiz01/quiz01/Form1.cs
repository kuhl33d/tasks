namespace quiz01
{
    class mosqituo
    {
        public int x, y;
        public Bitmap img;
        public int sel;
        public mosqituo()
        {
            sel = 0;
        }
    }
    class catcher
    {
        public int x, y, w, h;
        public int margin;
        public int current_on;
        public int sel_x,sel_y,sel_w,sel_h;
        public Color sel_clr;
        public Color clr;
        public catcher()
        {
            current_on = -1;
            sel_clr = Color.Yellow;
        }
    }
    public partial class Form1 : Form
    {

        List<catcher> C = new List<catcher>();
        List<mosqituo> M = new List<mosqituo>();
        Bitmap img;
        Bitmap asset_0 = new Bitmap("1.bmp");
        int oldx, oldy;
        bool Down = false;
        public Form1()
        {
            asset_0.MakeTransparent(asset_0.GetPixel(0,0));
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.SteelBlue;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
            this.MouseMove += Form1_MouseMove;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            Down = false;
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if(C.Count != 0)
            {
                oldx = e.X;
                oldy = e.Y;
                if(C[0].current_on != -1)
                {
                    Down = true;
                }
            }
            draw(this.CreateGraphics());
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if(Down == false)
            {

                if (C.Count != 0)
                {
                    if(e.X >=  C[0].x && e.X <= C[0].x + C[0].w && e.Y >= C[0].y && e.Y <= C[0].y + C[0].h)
                    {
                        
                        if(e.X <= C[0].x+C[0].margin && e.Y >= C[0].y+C[0].margin && e.Y <= C[0].y + C[0].margin + C[0].h - 2 * C[0].margin)
                        {
                            this.Text = "1";
                            C[0].current_on = 1;
                            C[0].sel_x = C[0].x;
                            C[0].sel_y = C[0].y+C[0].margin;
                            C[0].sel_w = C[0].margin;
                            C[0].sel_h = C[0].h - 2 * C[0].margin;
                        }
                        else if (e.X >= C[0].x+C[0].w-C[0].margin && e.Y >= C[0].y + C[0].margin && e.Y <= C[0].y + C[0].margin + C[0].h - 2 * C[0].margin)
                        {
                            this.Text = "3";
                            C[0].current_on = 3;
                            C[0].sel_x = C[0].x + C[0].w - C[0].margin;
                            C[0].sel_y = C[0].y + C[0].margin;
                            C[0].sel_w = C[0].margin;
                            C[0].sel_h = C[0].h - 2 * C[0].margin;
                        }
                        else if (e.X >= C[0].x+C[0].margin && e.X <= C[0].x+C[0].w-C[0].margin && e.Y <= C[0].y+C[0].margin)
                        {
                            this.Text = "2";
                            C[0].current_on = 2;
                            C[0].sel_x = C[0].x + C[0].margin;
                            C[0].sel_y = C[0].y;
                            C[0].sel_h = C[0].margin;
                            C[0].sel_w = C[0].w - 2 * C[0].margin;
                        }
                        else if (e.X >= C[0].x + C[0].margin && e.X <= C[0].x + C[0].w - C[0].margin && e.Y >= C[0].y + C[0].h - C[0].margin)
                        {
                            this.Text = "4";
                            C[0].current_on = 4;
                            C[0].sel_x = C[0].x + C[0].margin;
                            C[0].sel_y = C[0].y + C[0].h - C[0].margin;
                            C[0].sel_h = C[0].margin;
                            C[0].sel_w = C[0].w - 2 * C[0].margin;
                        }
                        else
                        {
                            this.Text = "";
                            C[0].current_on = -1;
                            for(int i = 0; i < M.Count; i++)
                            {
                                M[i].sel = 0;
                            }
                        }

                        if(C[0].current_on != -1)
                        {
                            for(int i = 0; i < M.Count; i++)
                            {
                                switch (C[0].current_on)
                                {
                                    case 1:
                                        if(M[i].x <= C[0].sel_x + C[0].margin)
                                        {
                                            M[i].sel = 1;
                                        }
                                        break;
                                    case 2:
                                        if(M[i].y <= C[0].sel_y + C[0].margin)
                                        {
                                            M[i].sel = 1;
                                        }
                                        break;
                                    case 3:
                                        if (M[i].x >= C[0].sel_x + C[0].margin)
                                        {
                                            M[i].sel = 1;
                                        }
                                        break;
                                    case 4:
                                        if (M[i].y >= C[0].sel_y + C[0].margin)
                                        {
                                            M[i].sel = 1;
                                        }
                                        break;
                                }
                            }
                        }

                    }
                    else
                    {
                        this.Text = "";
                        C[0].current_on = -1;
                        for(int i=0;i<M.Count; i++)
                        {
                            M[i].sel = 0;
                        }
                    }
                }
            }
            else
            {
                C[0].x += (e.X - oldx);
                C[0].y += (e.Y - oldy);
                C[0].sel_x += e.X - oldx;
                C[0].sel_y += e.Y - oldy;
                for(int i = 0; i < M.Count; i++)
                {
                    if(M[i].sel == 1)
                    {
                        M[i].x += e.X - oldx;
                        M[i].y += e.Y - oldy;
                    }
                }
                oldx = e.X;
                oldy = e.Y;
                draw(this.CreateGraphics());
            }
        }

        void createCatcher()
        {
            
            catcher pnn = new catcher();
            pnn.w = 200;
            pnn.h = 200;
            pnn.x = ClientSize.Width / 2 - pnn.w / 2;
            pnn.y = ClientSize.Height / 2 - pnn.h / 2;
            pnn.margin = pnn.w/5;
            pnn.clr = Color.Red;
            C.Add(pnn);
        }
        private void Form1_Load(object? sender, EventArgs e)
        {
            img = new Bitmap(ClientSize.Width, ClientSize.Height);
            
            draw(this.CreateGraphics());
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                    if(C.Count == 0)
                    {
                        createCatcher();
                    }
                    break;
                case Keys.Space:
                    if(M.Count == 0 && C.Count != 0)
                    {
                        createMosqituos();
                    }
                    break;
            }
            draw(this.CreateGraphics());
        }
        void createMosqituos()
        {
            mosqituo pnn;
            Random rand = new Random();
            for(int i = 0; M.Count<20; i++)
            {
                pnn = new mosqituo();
                pnn.x = rand.Next(0, ClientSize.Width-asset_0.Width);
                pnn.y = rand.Next(ClientSize.Height / 5, ClientSize.Height - ClientSize.Height/5-asset_0.Height);
                if (check(pnn) == 0)
                {
                    M.Add(pnn);
                }
            }
        }
        int check(mosqituo pnn)
        {
            for(int i=0;i<M.Count; i++)
            {
                if ( (Math.Abs(pnn.x - M[i].x) <= asset_0.Width) && (Math.Abs(pnn.y - M[0].y) <= asset_0.Height) ){
                    return 1;
                }
            }
            if( (Math.Abs(pnn.x-C[0].x)<asset_0.Width) || (Math.Abs(pnn.x - C[0].x) < C[0].w ) ){
                if ((Math.Abs(pnn.y - C[0].y) < asset_0.Height) || (Math.Abs(pnn.y - C[0].y) < C[0].h))
                {
                    return 1;
                }
            }
            return 0;
        }
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(this.CreateGraphics());
        }
        void draw(Graphics G) { 
            Graphics g2 = Graphics.FromImage(img);
            g2.Clear(this.BackColor);

            g2.DrawLine(new Pen(Color.Black,5),0,ClientSize.Height/5,ClientSize.Width,ClientSize.Height/5);
            g2.DrawLine(new Pen(Color.Black, 5), 0, ClientSize.Height - ClientSize.Height / 5, ClientSize.Width, ClientSize.Height - ClientSize.Height / 5);

            for (int i = 0; i < C.Count; i++)
            {
                if(C[i].current_on != -1)
                {
                    g2.FillRectangle(new SolidBrush(Color.Yellow), C[i].sel_x, C[i].sel_y, C[i].sel_w, C[i].sel_h);
                }
                g2.DrawRectangle(new Pen(C[i].clr,5), C[i].x, C[i].y, C[i].w, C[i].h);
                g2.DrawRectangle(new Pen(C[i].clr,5), C[i].x+C[i].margin, C[i].y+C[i].margin, C[i].w- C[i].margin*2, C[i].h- C[i].margin*2);
            }
            for(int i = 0; i < M.Count; i++)
            {
                if (M[i].sel == 1)
                {
                    g2.DrawLine(new Pen(Color.Red,2), C[0].sel_x + C[0].sel_w / 2, C[0].sel_y + C[0].sel_h / 2, M[i].x + asset_0.Width / 2, M[i].y + asset_0.Height / 2);   
                }
            }
            for(int i=0; i < M.Count; i++)
            {
                g2.DrawImage(asset_0,M[i].x,M[i].y);
            }
            G.DrawImage(img, 0, 0);
        }
    }
}