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
    public partial class NewHighScore : Form
    {
        public NewHighScore()
        {
            InitializeComponent();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) buttonEnter.PerformClick();
            else if (e.KeyData == Keys.Escape) Close();
        }
    }
}
