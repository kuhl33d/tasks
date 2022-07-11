namespace assignment_19
{
    class player
    {
        public float x, y, w, ox, oy, ow;
        public void anim(char d,float speed)
        {
            switch (d)
            {
                case 'l':
                    if (y >= oy && y <= oy + ow && x <=ox)
                    {
                        break;
                    }
                    else if(y >= oy + 2 * ow - w && y <= oy + 3 * ow && x <= ox)
                    {
                        break;
                    }
                    else if (x >= ox - 2 * ow+10)
                    {
                        x-=speed;
                    }
                    break;
                case 'r':
                    if (y >= oy && y <= oy + ow && x >= ox + 2 * ow-w)
                    {
                        break;
                    }
                    else if(y >= oy + 2 * ow - w && y <= oy + 3 * ow && x >= ox + 2 * ow - w)
                    {
                        break;
                    }
                    else if(x <= ox + 4 * ow-w-10)
                    {
                        x+=speed;
                    }
                    break;
                case 'u':
                    if((x >= ox - 2*ow && x < ox)||(x>ox+2*ow && x <= ox + 4 * ow))
                    {
                        if(y > oy + ow)
                        {
                            y-=speed;
                        }
                    }
                    else
                    {
                        if (y > oy)
                        {
                            y-=speed;
                        }
                    }
                    break;
                case 'd':
                    if ((x >= ox - 2 * ow && x < ox) || (x >= ox + 2 * ow - w && x <= ox + 4 * ow))
                    {
                        if (y < oy + 2 * ow-w)
                        {
                            y+=speed;
                        }
                    }
                    else
                    {
                        if (y < oy + 3 * ow-w)
                        {
                            y+=speed;
                        }
                    }
                    break;
            }
        }
    }
    class rect
    {
        public float x,y,w,h,oX,oY;
        public int d;
        public void anim(float speed)
        {
            switch (d)
            {
                case 0:
                    x+=speed;
                    if(x >= oX + w)
                    {
                        d++;
                    }
                    break;
                case 1:
                    y+=speed;
                    if(y >= oY + h * 2)
                    {
                        d++;
                    }
                    break;
                case 2:
                    x-=speed;
                    if(x <= oX)
                    {
                        d++;
                    }
                    break;
                case 3:
                    y-=speed;
                    if(y <= oY)
                    {
                        d = 0;
                    }
                    break;
            }
        }

    }
    public partial class Form1 : Form
    {
        player P;
        rect[] enemies = new rect[2];
        Bitmap img;
        Graphics g;
        int w = 100;
        float originX, originY;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Paint += Form1_Paint;
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Interval = 100;
            t.Start();
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    P.anim('u',w/10);
                    break;
                case Keys.Down:
                    P.anim('d',w/10);
                    break;
                case Keys.Left:
                    P.anim('l',w/10);
                    break;
                case Keys.Right:
                    P.anim('r',w/10);
                    break;
            }
        }
        bool collide(rect enemy,player p)
        {
            if(p.x >= enemy.x && p.x <= enemy.x + enemy.w)
            {
                if(p.y >= enemy.y && p.y <= enemy.y + enemy.h)
                {
                    return true;
                }
            }
            return false;
        }
        private void T_Tick(object? sender, EventArgs e)
        {
            if(collide(enemies[0],P))
            {
                this.Text = "collided with enemy 0";
                P.x = originX - 2 * w + 10;
                P.y = originY + w + 10;
                return;
            }
            else if (collide(enemies[1],P))
            {
                this.Text = "collided with enemy 1";
                P.x = originX - 2 * w + 10;
                P.y = originY + w + 10;
                return;
            }
            else
            {
                draw(CreateGraphics());
                enemies[0].anim(w/20);
                enemies[1].anim(w/20);
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            originX = ClientSize.Width/2 - (w*3/2);
            originY = ClientSize.Height / 2 - (w * 3 / 2);
            P = new player();
            P.w = w/2;
            P.ow = w;
            P.ox = originX;
            P.oy = originY;
            P.x = originX-2*w+10;
            P.y = originY + w + 10;
            enemies[0] = new rect();
            enemies[1] = new rect();
            enemies[0].x = originX;
            enemies[0].y = originY;
            enemies[0].oX = originX;
            enemies[0].oY = originY;
            enemies[0].w = w;enemies[0].h = w;
            enemies[0].d = 0;
            enemies[1].x = originX+w;
            enemies[1].y = originY+w*2;
            enemies[1].w = w; enemies[1].h = w;
            enemies[1].oX = originX;
            enemies[1].oY = originY;
            enemies[1].d = 2;
            img = new Bitmap(ClientSize.Width, ClientSize.Height);
            g = Graphics.FromImage(img);
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            draw(CreateGraphics());
        }
        void draw(Graphics G)
        {
            g.Clear(this.BackColor);
            g.FillEllipse(new SolidBrush(Color.Aqua), P.x, P.y,P.w,P.w);
            for(int i = 0;i < 2; i++){
                g.FillRectangle(new SolidBrush(Color.White), enemies[i].x, enemies[i].y, enemies[i].w, enemies[i].h);
            }
            g.DrawLine(new Pen(Color.Blue,5), originX, originY, originX+w*2, originY);
            g.DrawLine(new Pen(Color.Blue, 5), originX + w * 2, originY, originX + w * 2, originY+w);
            g.DrawLine(new Pen(Color.Blue, 5), originX + w * 2, originY + w, originX + w * 4, originY + w);
            g.DrawLine(new Pen(Color.Blue, 5), originX + w * 4, originY + w, originX + w * 4, originY + w*2);
            g.DrawLine(new Pen(Color.Blue, 5), originX + w * 4, originY + w * 2, originX + w * 2, originY + w * 2);
            g.DrawLine(new Pen(Color.Blue, 5), originX + w * 2, originY + w * 2, originX + w * 2, originY + w * 3);
            g.DrawLine(new Pen(Color.Blue, 5), originX + w * 2, originY + w * 3, originX, originY + w * 3);
            g.DrawLine(new Pen(Color.Blue, 5), originX, originY + w * 3, originX, originY + w * 2);
            g.DrawLine(new Pen(Color.Blue, 5), originX, originY + w * 3, originX, originY + w * 2);
            g.DrawLine(new Pen(Color.Blue, 5), originX, originY + w * 2, originX - w * 2, originY + w * 2);
            g.DrawLine(new Pen(Color.Blue, 5), originX - w * 2, originY + w * 2, originX - w * 2, originY + w);
            g.DrawLine(new Pen(Color.Blue, 5), originX - w * 2, originY + w, originX, originY + w);
            g.DrawLine(new Pen(Color.Blue, 5), originX, originY + w,originX,originY);
            G.DrawImage(img,0,0);
        }
    }
}