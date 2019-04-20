using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot
{
    public partial class Servidores : Form
    {
        public Servidores()
        {
            InitializeComponent();
        }

        private void Servidores_Load(object sender, EventArgs e)
        {
            string[] b = { "a", "b" };

            for(int i =0; i < 5; i++)
            {
                dataServidores.Rows.Add(b);
            }


        }
    }
}
