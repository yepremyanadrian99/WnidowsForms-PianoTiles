using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPianoTiles
{
    public partial class Speed : Form
    {
        Form1 fr;

        public Speed(Form1 fr)
        {
            InitializeComponent();
            this.fr = fr;
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button1.PerformClick();
            else if (e.KeyData == Keys.Escape) Environment.Exit(0);
        }
    }
}
