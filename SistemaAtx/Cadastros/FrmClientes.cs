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

namespace SistemaAtx.Cadastros
{
    public partial class FrmClientes : Form
    {
        //CODIGO DE CONEXAO COM O BANCO DE DADOS MYSQL
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        


        Forms_Menu.FrmAbrirOS fp;
        public FrmClientes(Forms_Menu.FrmAbrirOS f)
        {
            InitializeComponent();
            fp = f;
            f.num = 10;
        }


        private void FormatarDG()
        {
            try
            {
                grid.Columns[0].HeaderText = "ID";
                grid.Columns[1].HeaderText = "Nome";
                grid.Columns[2].HeaderText = "Telefone";
                grid.Columns[3].HeaderText = "Celular";
                grid.Columns[4].HeaderText = "CPF";
                grid.Columns[5].HeaderText = "Endereço";
                grid.Columns[6].HeaderText = "Bairro";
                grid.Columns[7].HeaderText = "Cidade";
                grid.Columns[8].HeaderText = "E-mail";


                //FORMATAR COLUNA PARA MOEDA
                //grid.Columns[3].DefaultCellStyle.Format = "Date";
                //grid.Columns[4].DefaultCellStyle.Format = "C2";

                grid.Columns[0].Visible = false;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao formatar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }

        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM clientes where nome LIKE @nome order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscarNome.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();

            FormatarDG();
        }


        private void Listar()
        {
            try
            {

                con.AbrirCon();
                sql = "SELECT * FROM clientes order by nome asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharCon();

                FormatarDG();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }







        private void btnNovo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para adicionar um novo cliente abra o form 'Cadastro de Cliente'");
        }

        


        private void FrmClientes_Load(object sender, EventArgs e)
        {
            
            try
            {
                Listar();
                              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar clientes! \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void grid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            fp.txtCliente.Text = grid.CurrentRow.Cells[1].Value.ToString();
            Close();
            

        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            fp.txtCliente.Text = grid.CurrentRow.Cells[1].Value.ToString();
            Close();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fp.txtCliente.Text = grid.CurrentRow.Cells[1].Value.ToString();
            Close();
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fp.txtCliente.Text = grid.CurrentRow.Cells[1].Value.ToString();
            Close();
        }
    }
}
