namespace quiz02
{
    class actor
    {
        public int x, y, img=0,f=0;
        public List<burger> b = new List<burger>();
        public void anim()
        {
            if (f == 0)
            {
                img++;
                if(img == 7)
                {
                    img--;
                    f = 1;
                }
            }else if(f == 1)
            {
                img--;
                if (img == -1)
                {
                    img++;
                    f = 0;
                }
            }
        }
    }
    class burger
    {
        public int x, y,f=0,h;
        public int top, bottom;
        public burger(int t, int b,int h)
        {
            top = t;
            bottom = b;
            this.h = h;
        }
        public void anim()
        {
            if (f == 0)
            {
                y -= h;
                //y--;
                //y -= ((top + h) - (bottom - h)) / 5;
                if (y <= top)
                    f = 1;
            }
            else
            {
                y += h;
                //y++;
                //y += ((top + h) - (bottom - h)) / 5;
                if (y >= bottom - h - 10)
                    f = 0;
            }
        }

    }
    public partial class Form1 : Form
    {
        Bitmap[] actor_imgs;
        actor[] actors;
        List<burger> b1 = new List<burger> ();
        List<burger> b2 = new List<burger>();
        Bitmap b_img;
        Random R;
        bool anim = true;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        Bitmap D_img;
        Graphics g;
        public Form1()
        { 
            this.WindowState = FormWindowState.Maximized;
            t.Interval = 250;
            t.Start();
            
            t.Tick += T_Tick;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if(anim == false)
            {
                int which = -1;
                int second = -1;
                for(int i = 0; i < 3; i++)
                {
                    if(e.X >= actors[i].x && e.X <= actors[i].x + actor_imgs[0].Width)
                    {
                        which = i;
                        break;
                    }
                }
                if(which != -1)
                {
                    if(b1.Count >= 3 && b2.Count >= 3)
                    {
                        if(which == 0)
                        {
                            second = 1;
                        }else if(which == 1)
                        {
                            second = 0;
                        }
                        else
                        {
                            second = 1;
                        }
                        actors[which].b.Add(b1[b1.Count - 1]);
                        b1.RemoveAt(b1.Count - 1);
                        actors[which].b.Add(b1[b1.Count - 1]);
                        b1.RemoveAt(b1.Count - 1);
                        actors[second].b.Add(b1[b1.Count - 1]);
                        b1.RemoveAt(b1.Count - 1);

                    }
                    else if(b1.Count >= 3)
                    {
                        if (which == 2)
                            return;
                        if(which == 0)
                            second = 1;
                        else
                            second = 0;
                        actors[which].b.Add(b1[b1.Count - 1]);
                        b1.RemoveAt(b1.Count - 1);
                        actors[which].b.Add(b1[b1.Count - 1]);
                        b1.RemoveAt(b1.Count - 1);
                        actors[second].b.Add(b1[b1.Count - 1]);
                        b1.RemoveAt(b1.Count - 1);
                    }
                    else if(b2.Count >= 3)
                    {
                        if (which == 0)
                            return;
                        if (which == 1)
                            second = 2;
                        else
                            second = 1;
                        actors[which].b.Add(b2[b2.Count - 1]);
                        b2.RemoveAt(b2.Count - 1);
                        actors[which].b.Add(b2[b2.Count - 1]);
                        b2.RemoveAt(b2.Count - 1);
                        actors[second].b.Add(b2[b2.Count - 1]);
                        b2.RemoveAt(b2.Count - 1);
                    }
                }
                
                if (b2.Count < 3 && b1.Count < 3)
                {
                    anim = true;
                }
                draw(this.CreateGraphics());
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(CreateGraphics());
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            R = new Random();
            actor_imgs = new Bitmap[7];
            actors = new actor[3];
            D_img = new Bitmap(ClientSize.Width, ClientSize.Height);
            g = Graphics.FromImage(D_img);
            b_img = new Bitmap("8.bmp");
            for (int i = 0; i < 7; i++)
            {
                actor_imgs[i] = new Bitmap((i + 1) + ".bmp");
                actor_imgs[i].MakeTransparent(actor_imgs[i].GetPixel(0, 0));
            }
            for (int i = 0; i < 3; i++)
            {
                actors[i] = new actor();
                actors[i].img = R.Next(7);
                actors[i].y = ClientSize.Height - actor_imgs[0].Height;
                actors[i].x = (i+1) * ClientSize.Width/4 - actor_imgs[0].Width/2;
            }


        }

        private void T_Tick(object? sender, EventArgs e)
        {
            if (anim == true)
            {
                if(b1.Count >= 3 || b2.Count >= 3)
                {
                    anim = false;
                    return;
                }
                for(int i = 0; i < actors.Length; i++)
                {
                    actors[i].anim();
                }
                for(int i = 0; i < b1.Count; i++)
                {
                    b1[i].anim();
                }
                for (int i = 0; i < b2.Count; i++)
                {
                    b2[i].anim();
                }
                if (R.Next(20) >= 15)
                {
                    int num = R.Next(1, 3);
                    for(int k=0;k<num; k++)
                    {

                        burger pnn = new burger(ClientSize.Height-actor_imgs[0].Height, ClientSize.Height, b_img.Height);
                        pnn.f = 0;
                        if (R.Next(2) == 0)
                        {
                            if (R.Next(2) == 0)
                            {
                                pnn.x = actors[0].x + actor_imgs[0].Width + 15;
                            }
                            else
                            {
                                pnn.x = actors[1].x - b_img.Width - 15;
                            }
                            bool b = false;
                            while (b == false)
                            {
                                pnn.y = ClientSize.Height - b_img.Height -  R.Next(5)* b_img.Height;
                                b = true;
                                for(int i = 0; i < b1.Count; i++)
                                {
                                    if (pnn.x == b1[i].x)
                                    {
                                        if(pnn.y == b1[i].y)
                                        {
                                            pnn.y = ClientSize.Height - b_img.Height - R.Next(5) * b_img.Height;
                                            b= false;
                                            break;
                                        }
                                    }
                                }
                            }
                            b1.Add(pnn);
                        }
                        else
                        {
                            if (R.Next(2) == 0)
                            {
                                pnn.x = actors[1].x + actor_imgs[0].Width + 15;
                            }
                            else
                            {
                                pnn.x = actors[2].x - b_img.Width - 15;
                            }
                            bool b = false;
                            while (b == false)
                            {
                                pnn.y = ClientSize.Height - b_img.Height - R.Next(5) * b_img.Height - R.Next(20);
                                b = true;
                                for (int i = 0; i < b1.Count; i++)
                                {
                                    if (pnn.x == b1[i].x)
                                    {
                                        if (pnn.y == b1[i].y)
                                        {
                                            pnn.y = ClientSize.Height - b_img.Height - R.Next(5) * b_img.Height - R.Next(20);
                                            b = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            b2.Add(pnn);
                        }
                    }
                }
            }
            
            this.Text = b1.Count + " " + b2.Count + " " + actors[0].b.Count + " " + actors[1].b.Count + " " + actors[0].b.Count + " ";
            draw(CreateGraphics());
        }
        void draw(Graphics G)
        {
            g.Clear(BackColor);
            for(int i = 0; i < actors.Length; i++)
            {
                g.DrawImage(actor_imgs[actors[i].img], actors[i].x, actors[i].y);
                for(int j = 0; j < actors[i].b.Count; j++)
                {
                    g.DrawImage(b_img, actors[i].x, actors[i].y - (j + 1) * b_img.Height);
                }
            }
            for(int i = 0; i < b1.Count; i++)
            {
                g.DrawImage(b_img, b1[i].x, b1[i].y);
            }
            for (int i = 0; i < b2.Count; i++)
            {
                g.DrawImage(b_img, b2[i].x, b2[i].y);
            }
            G.DrawImage(D_img, 0, 0);
        }
    }
}