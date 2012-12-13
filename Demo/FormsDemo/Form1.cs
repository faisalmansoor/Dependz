using System;
using System.Windows.Forms;

namespace FormsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.axShockwaveFlash1.Movie = "http://www.adobe.com/swf/software/flash/about/mini_FMA_about_01.swf";
        }
    }
}
