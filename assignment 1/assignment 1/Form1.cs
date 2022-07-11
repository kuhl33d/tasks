namespace assignment_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            MessageBox.Show("base on your horizontal move\nchange the background color for each (1/4) of the width");
            this.MouseMove += new MouseEventHandler(doit);
        }
        void doit(object sender,MouseEventArgs e)
        {
            switch(e.X / (this.Width / 4))
            {
                case 0:
                    this.BackColor = Color.Red;
                    break;
                case 1:
                    this.BackColor = Color.Blue;
                    break;
                case 2:
                    this.BackColor = Color.Green;
                    break;
                case 3:
                    this.BackColor = Color.Violet;
                    break;
            }
        }
    }
}