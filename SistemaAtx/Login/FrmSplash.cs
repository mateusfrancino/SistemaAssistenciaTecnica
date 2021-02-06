using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx.Login
{
    public partial class FrmSplash : Form
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (guna2ProgressBar1.Value < 100)
            {
                guna2ProgressBar1.Value = guna2ProgressBar1.Value + 2;
            }
            else
            {
                timer1.Enabled = false;
                this.Visible = false;
                Login login = new Login();
                login.Show();
            }

        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            guna2AnimateWindow1.SetAnimateWindow(this);
            /* DEFINE A SOMBRA DO FORMULÁRIO AO SER EXBIDO NOS CANTOS */
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
