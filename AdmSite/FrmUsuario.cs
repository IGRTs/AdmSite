using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdmSite
{
    public partial class FrmUsuario : Form
    {
        public object SqlDbtype { get; private set; }

        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {

        }
        public void pesquisa()
        {
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            DataSet dt = new DataSet();
            Conexao c = new Conexao();
            c.conectar();
            String login = "%" + txtLogin.Text + "%";
            String sql = "select * from servico where nome like @serv";
            c.command.CommandText = sql;
            c.command.Parameters.Add("@serv", SqlDbType.VarChar).
                Value = login;
            dAdapter.SelectCommand = c.command;
            dAdapter.Fill(dt);
            c.fechaConexao();
            dataGrid1.DataSource = dt.Tables;
            

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Excluir o registro?",
              "Remoção",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
            {


                int codUsuario = Convert.ToInt32(
                    dataGrid1.SelectedRows[0].Cells[0].Value);
                Conexao c = new Conexao();
                c.conectar();
                c.command.CommandText = "delete from usuario " +
                    "where codigo=@cod";
                c.command.Parameters.Add("@cod", SqlDbType.Int).Value = codUsuario;
                c.command.ExecuteNonQuery();
                c.fechaConexao();
                pesquisa();
                MessageBox.Show("Usuário removido com sucesso",
                    "Remoção", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            String s = Microsoft.VisualBasic.
                Interaction.InputBox("Digite a nova senha");
            if (s.Length > 3)
            {
                int codUsuario = Convert.ToInt32(dataGrid1.SelectedRows[0].
                    Cells[0].Value);
                Conexao c = new Conexao();
                c.conectar();
                c.command.CommandText = "update usuario set " +
                    " senha=@senha where codigo=@codigo";
                c.command.Parameters.Add("@senha",
                    SqlDbType.VarChar).Value = s;
                c.command.Parameters.Add("@codigo",
                    SqlDbType.Int).Value = codUsuario;
                c.command.ExecuteNonQuery();
                MessageBox.Show("Senha atualizada com sucesso! ", 
                    "Alteração de senha",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Digite uma senha " +
                    "com mais de 3 caracteres", "Senha incorreta", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
