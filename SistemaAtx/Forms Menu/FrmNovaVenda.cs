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
    public partial class FrmNovaVenda : Form
    {
        //CODIGO DE CONEXAO COM O BANCO DE DADOS MYSQL
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        public FrmNovaVenda()
        {
            InitializeComponent();
        }
        private void HabilitarCampos()
        {
            txtProduto.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValorUn.Enabled = true;
            txtTotal.Enabled = true;
            btnFinalizar.Enabled = true;

        }

        private void LimparCampos()
        {
            txtProduto.Text = "";
            txtQuantidade.Text = "";
            txtValorUn.Text = "";
            txtTotal.Text = "";
            txtCodigoBarras.Text = "";
            txtDesconto.Text = "";
            txtSubTotal.Text = "";

        }


        private void FrmNovaVenda_Load(object sender, EventArgs e)
        {
            HabilitarCampos();
            LimparCampos();
            btnFinalizar.Enabled = true;
            txtQuantidade.Text = "1";
        }

        private void FormatarDGDetalhesVendas()
        {
            grid.Columns[0].HeaderText = "Cod";
            grid.Columns[1].HeaderText = "Código Venda";
            grid.Columns[2].HeaderText = "Produto";
            grid.Columns[3].HeaderText = "Quantidade";
            grid.Columns[4].HeaderText = "Valor Un";
            grid.Columns[5].HeaderText = "Valor SubTotal";
            grid.Columns[6].HeaderText = "Valor Total";
            grid.Columns[7].HeaderText = "Funcionário";
            grid.Columns[8].HeaderText = "Id Produto";

            //FORMATAR COLUNA PARA MOEDA
            grid.Columns[4].DefaultCellStyle.Format = "C2";
            //grid.Columns[5].DefaultCellStyle.Format = "C2";


            grid.Columns[1].Visible = false;
            grid.Columns[5].Visible = false;
            grid.Columns[6].Visible = false;
            grid.Columns[7].Visible = false;
            grid.Columns[8].Visible = false;

        }


        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (txtQuantidade.Text.ToString().Trim() == "")
            {

                MessageBox.Show("Preencha a Quantidade", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuantidade.Focus();
                return;
            }


            if (txtProduto.Text.ToString().Trim() == "")
            {

                MessageBox.Show("Insira um produto", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProduto.Focus();
                return;
            }

            
                  

        }    
    }
}
