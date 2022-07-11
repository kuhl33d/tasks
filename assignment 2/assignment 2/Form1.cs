using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment_2
{
    public partial class Form1 : Form
    {
        int flag = 0;
        public Form1()
        {
            MessageBox.Show("each click decrease the opacity by (0.1)\nmake it 9 times\nthe following 9 click should increase the opacity by (0.1) each time and so on");
            this.Text = this.Opacity.ToString();
            this.MouseDown += new MouseEventHandler(doit);
        }

        void doit(object sender, MouseEventArgs e)
        {
            
            if ((int)(this.Opacity*10) == 1)
                flag = 1;

            if (flag == 0)
                this.Opacity -= 0.1;
            else
                this.Opacity += 0.1;

            if ((float)this.Opacity == 1)
                flag = 0;

            this.Text = this.Opacity.ToString() + " " + flag.ToString();
        }
    }
}
