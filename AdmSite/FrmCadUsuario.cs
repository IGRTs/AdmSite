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
    public partial class FrmCadUsuario : Form
    {
        public FrmCadUsuario()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            // if (PesquisaUsuario(txtLogin.Text) == 0)
            // {
            try
            {
                Conexao c = new Conexao();
                c.conectar();
                String sql = "insert into usuario " + "(login, senha) values(@login,@senha)";
                String login = txtLogin.Text.Trim();
                String senha = txtSenha.Text.Trim();
                c.command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                c.command.Parameters.Add("@senha", SqlDbType.VarChar).Value = senha;
                c.command.CommandText = sql;
                c.command.ExecuteNonQuery();
                c.fechaConexao();
                MessageBox.Show("Dados gravados com sucesso", "Inserção de Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                    catch (SqlException ex) {
                      if (ex.Message.Contains("UNIQUE"))
                     {
                       MessageBox.Show("Usuario já Existe", "Incriçaõ de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }

                
            }
   
        }

        
        private int PesquisaUsuario (String login)
        {
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            DataSet dt = new DataSet();
            Conexao c = new Conexao();
            c.conectar();
            String sql = " select count(*) as qtde from " + " usuario where login=@login ";
            c.command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
            c.command.CommandText = sql;
            dAdapter.SelectCommand = c.command;
            dAdapter.Fill(dt);
            c.fechaConexao();
            int quantidade = Convert.ToInt32(dt.Tables[0].DefaultView[0].Row["qtde"].ToString());
            return quantidade;
        }

        private void FrmCadUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
