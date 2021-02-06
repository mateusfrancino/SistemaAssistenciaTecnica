using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx.Forms_Menu
{
    public partial class FrmAbrirOS : Form
    {

        //CODIGO DE CONEXAO COM O BANCO DE DADOS MYSQL
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;

        string dispositivo_selecionado = "";

        public int num;
        public FrmAbrirOS()
        {
            InitializeComponent();
            num = 0;
            

        }

        private void DesabilitarCampos()
        {
            txtCliente.Enabled = false;
            txtMarca.Enabled = false;
            txtModelo.Enabled = false;
            txtIdentificacao.Enabled = false;
            txtAcessorios.Enabled = false;
            txtDefeitoInformado.Enabled = false;
            txtObs.Enabled = false;
            cbCargo.Enabled = false;
            cbPrioridade.Enabled = false;
            cbStatus.Enabled = false;
            btnCam.Enabled = false;
            btnCel.Enabled = false;
            btnNot.Enabled = false;
            btnOutros.Enabled = false;
            btnPc.Enabled = false;
            btnTv.Enabled = false;
            btnVGame.Enabled = false;
            btnSalvar.Enabled = false;
            dateTime.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtCliente.Enabled = true;
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtIdentificacao.Enabled = true;
            txtAcessorios.Enabled = true;
            txtDefeitoInformado.Enabled = true;
            txtObs.Enabled = true;
            cbCargo.Enabled = true;
            cbPrioridade.Enabled = true;
            cbStatus.Enabled = true;
            btnCam.Enabled = true;
            btnCel.Enabled = true;
            btnNot.Enabled = true;
            btnOutros.Enabled = true;
            btnPc.Enabled = true;
            btnTv.Enabled = true;
            btnVGame.Enabled = true;
            btnSalvar.Enabled = true;
            dateTime.Enabled = true;

        }

        private void LimparCampos()
        {
            txtCliente.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtIdentificacao.Text = "";
            txtAcessorios.Text = "";
            txtDefeitoInformado.Text = "";
            txtObs.Text = "";
            LimparEscolha();
        }
        private void CarregarCombobox()
        {
            con.AbrirCon();
            sql = "SELECT * FROM funcionarios WHERE funcao = 'Técnico'";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbCargo.DataSource = dt;
            //cbCargo.ValueMember = "id";
            cbCargo.DisplayMember = "nome";

            con.FecharCon();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            txtCliente.Text = Program.chamadaClientes;
        }

        private void FrmAbrirOS_Load(object sender, EventArgs e)
        {
            CarregarCombobox();
            DesabilitarCampos();
        }

        private void btnPc_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "Computador";
            btnPc.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }

        private void btnCel_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "Celular";
            btnCel.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "Notebook";
            btnNot.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }


        private void btnTv_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "TV";
            btnTv.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }

        private void btnCam_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "Câmera";
            btnCam.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }

        private void btnVGame_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "Vídeo Game";
            btnVGame.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }

        private void btnOutros_Click(object sender, EventArgs e)
        {
            dispositivo_selecionado = "Outros Dispositivos";
            btnOutros.Image = Properties.Resources.ok;
            btnLimpar.Visible = true;
        }

        private void LimparEscolha()
        {

            {
                btnPc.Image = Properties.Resources.pc;
                btnCel.Image = Properties.Resources.cel;
                btnNot.Image = Properties.Resources.notebook;
                btnTv.Image = Properties.Resources.tv;
                btnCam.Image = Properties.Resources.cam;
                btnVGame.Image = Properties.Resources.game;
                btnOutros.Image = Properties.Resources.outros;
                dispositivo_selecionado = "";
            }

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparEscolha();
            btnLimpar.Visible = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            /* if (dispositivo_selecionado == "")
             {
                 MessageBox.Show("Você precisa selecionar um dispositivo!");
             }
             if (txtCliente.Text == "")
             {
                 MessageBox.Show("Informe um cliente!");
             }
             if (txtDefeitoInformado.Text == "")
             {
                 MessageBox.Show("Informe um defeito!");
             }
            */

            txtCliente.Text = Program.chamadaClientes;

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            LimparCampos();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {

            Program.chamadaClientes = "Cliente";
            Cadastros.FrmClientes form = new Cadastros.FrmClientes(this);
            form.Show();



        }

        private void FrmAbrirOS_Activated(object sender, EventArgs e)
        {
            txtCliente.Text = Program.chamadaClientes;
        }

        private void FrmAbrirOS_Click(object sender, EventArgs e)
        {
            txtCliente.Text = Program.chamadaClientes;
        }
    }
}
