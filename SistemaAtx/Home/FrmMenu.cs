using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx
{
    public partial class FrmMenu : Form
    {
        public Form _objForm;
        public FrmMenu()
        {
            InitializeComponent();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

      

        private void minimizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void pictureBox11_Click(object sender, EventArgs e)
        {
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmVendasHoje
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            _objForm?.Close();

            _objForm = new Forms_Menu.FormOsHoje
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmEmManutencao
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmConcluido
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }



        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmNovaVenda
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmNovaVenda
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AbrirOrdem();
        }

        private void AbrirOrdem()
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmAbrirOS()
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            AbrirOrdem();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmNovoOrcamento
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmNovoOrcamento
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void PicAtx_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVendasHoje.Visible == true)
                {
                    lblVendasHoje.Visible = false;
                    lblValorOculto.Visible = true;
                    btnOcultar.Visible = false;
                    btnVer.Visible = true;
                }
                else
                {
                    lblVendasHoje.Visible = true;
                    lblValorOculto.Visible = false;
                    btnOcultar.Visible = true;
                    btnVer.Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível recuperar dados atualizados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void desabilitarSistema()
        {
            var resultado = MessageBox.Show("Seu sistema está INATIVO, entre em contato com o suporte \n WhatsApp(33) 998080977 \n E - mail suporte@atxsistemas.com", "Licença Expirada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                System.Diagnostics.Process pStart = new System.Diagnostics.Process();
                pStart.StartInfo = new System.Diagnostics.ProcessStartInfo(@"https:www.atxsistemas.com/ativacao");
                pStart.Start();
                Environment.Exit(0);
            }
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {

            if(Program.statusAtivacao == "Inativo")
            {
                desabilitarSistema();
            }
            else
            {
                lblValorOculto.Visible = true;
                lblVendasHoje.Visible = false;
                btnOcultar.Visible = false;
                LblNomeUsuario.Text = Program.nomeUsuario;
                LblCargoUsuario.Text = Program.cargoUsuario;
            }
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVendasHoje.Visible == true)
                {
                    lblVendasHoje.Visible = false;
                    lblValorOculto.Visible = true;
                    btnOcultar.Visible = false;
                    btnVer.Visible = true;
                }
                else
                {
                    lblVendasHoje.Visible = true;
                    lblValorOculto.Visible = false;
                    btnOcultar.Visible = true;
                    btnVer.Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível recuperar dados atualizados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmCadastroProdutos
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void FrmMenu_Activated(object sender, EventArgs e)
        {
            
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
        }

        private void funcionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmFuncionarios
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmFornecedores
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void cargosFunçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmFuncoes
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmEstoque
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmEstoque
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmEstoque
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process pStart = new System.Diagnostics.Process();
            pStart.StartInfo = new System.Diagnostics.ProcessStartInfo(@"https:www.atxsistemas.com/suporte");
            pStart.Start();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process pStart = new System.Diagnostics.Process();
            pStart.StartInfo = new System.Diagnostics.ProcessStartInfo(@"https:www.atxsistemas.com/suporte");
            pStart.Start();
        }


        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmCadastroClientes
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
            
        }

        private void dadosEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Cadastros.FrmEmpresa
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();

        }
    }
}
