using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StundenplanOrganisierer
{
    public partial class Fehlerfenster : Form           //nur ein kleines Experiment, einfacher mit MessageBox
    {
        public Fehlerfenster(string fehler)
        {
            InitializeComponent();
            textBox1.Text = fehler;
        }
    }
}
