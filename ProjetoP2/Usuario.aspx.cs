using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

namespace ProjetoP2
{
    public partial class Usuario : System.Web.UI.Page
    {
        string scc = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\basedados\\basedados.accdb;Persist Security Info=False;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                               
                this.buildDropTipo();
                this.buildDropStatus();
                this.buildGridUsuarios();
                
            }
        }
        protected void limpar(object sender, EventArgs e)
        {
            this.txtcodigo.Text = "";
            this.txtemail.Text = "";
            this.txtnome.Text = "";
            this.txtsenha.Text = "";
            this.txtTelefone.Text = "";
            this.cboStatus.SelectedValue= "0";
            this.cboTipo.SelectedValue = "0";
        }
        protected void alterar(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conexao = new OleDbConnection(scc);
                conexao.Open();

                String sql = "update usuario set nome='{1}', email='{2}', senha='{3}', " +
                            " telefone='{4}',tipousuario='{5}',status='{6}' where codigo={0}";
                sql = String.Format(sql,txtcodigo.Text, txtnome.Text, txtemail.Text, txtsenha.Text, txtTelefone.Text,
                                    cboTipo.SelectedItem.Value, cboStatus.SelectedItem.Value);
                OleDbCommand comando = new OleDbCommand(sql, conexao);
                comando.ExecuteNonQuery();
                info_msg.Text = "Registro Alterado com sucesso!";
                conexao.Close();
                limpar(form1, null);
                this.buildGridUsuarios();
            }
            catch
            {

            }
        }
        protected void openSendMessage(object sender, EventArgs e)
        {
            Response.Redirect("Mensagem.aspx");
        }
        protected void Inserir(object sender, EventArgs e)
        {


            try
            {
                OleDbConnection conexao = new OleDbConnection(scc);
                conexao.Open();
                string sql = "insert into usuario(nome,email,senha,telefone,tipousuario,status)" +
                    " values('{0}','{1}','{2}','{3}','{4}','{5}')";
                sql = String.Format(sql, txtnome.Text, txtemail.Text, txtsenha.Text, txtTelefone.Text, cboTipo.SelectedItem.Value,cboStatus.SelectedItem.Value);
                OleDbCommand command = new OleDbCommand(sql, conexao);
                command.ExecuteNonQuery();
                this.info_msg.Text = "Registro inserido com  sucesso!";
                conexao.Close();
                limpar(this.form1, null);
                buildGridUsuarios();
            }
            catch (Exception err)
            {
                info_msg.Text = err.Message;
            }

        }
        protected void excluir(object sender, EventArgs e)
        {
            try
            {

                if (this.txtcodigo.Text == "")
                {
                    throw new Exception("O campo código não pode ser nulo!");
                }

                OleDbConnection conexao = new OleDbConnection(scc);
                conexao.Open();

                string sql = "delete from usuario where codigo=" + txtcodigo.Text;
                OleDbCommand command = new OleDbCommand(sql, conexao);
                command.ExecuteNonQuery();
                conexao.Close();
                info_msg.Text = "Registro excluido com sucesso!!";
                limpar(this.form1, null);
                this.buildGridUsuarios();
            }
            catch (Exception err)
            {
                this.info_msg.Text = err.Message;
            }
        }
        protected void consultar(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conexao = new OleDbConnection(scc);
                conexao.Open();

                string sql = "select * from usuario where codigo=" + txtcodigo.Text;
                OleDbCommand comando = new OleDbCommand(sql, conexao);
                OleDbDataReader retorno = comando.ExecuteReader();
                if (retorno.Read())
                {
                    txtcodigo.Text = retorno["codigo"].ToString();
                    txtnome.Text = retorno["nome"].ToString();
                    txtemail.Text = retorno["email"].ToString();
                    txtsenha.Text = retorno["senha"].ToString();
                    txtTelefone.Text = retorno["telefone"].ToString();
                    cboTipo.SelectedValue= retorno["tipoUsuario"].ToString();
                    cboStatus.SelectedValue = retorno["status"].ToString();
                    
                }
                else
                {
                    throw new Exception("Registro não encontrado!");
                    //limpar(form1, null);
                }
            }
            catch (Exception err)
            {
                info_msg.Text = err.Message;
            }
        }
        protected void buildDropTipo()
        {
            OleDbConnection conexao = new OleDbConnection(scc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "select * from tipoUsuario order by nomeTipoUsuario";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();
            //preenchendo o combo
            cboTipo.DataSource = ds;
            cboTipo.DataValueField = "codigo";
            cboTipo.DataTextField = "nomeTipoUsuario";
            cboTipo.DataBind();

            cboTipo.Items.Insert(0, new ListItem("Selecione um tipo !", "0"));
        }
        protected void buildDropStatus()
        {
            OleDbConnection conexao = new OleDbConnection(scc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "select * from status order by nomeStatus";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();
            //preenchendo o combo
            cboStatus.DataSource = ds;
            cboStatus.DataValueField = "codigo";
            cboStatus.DataTextField = "nomeStatus";
            cboStatus.DataBind();

            cboStatus.Items.Insert(0, new ListItem("Selecione um status !", "0"));
        }
        protected void buildGridUsuarios()
        {
            OleDbConnection conexao = new OleDbConnection(scc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "SELECT u.codigo as Código";
            sql += ",u.nome as Nome ,u.email as Email";
            sql += ",u.senha as Senha";     
            sql += ",u.telefone as Telefone";
            sql += ",t.nomeTipoUsuario as Tipo";
            sql += ",s.nomeStatus as Status";
            sql += " from usuario u ,tipousuario t, status s";
            sql += " where u.tipoUsuario = t.codigo";
            sql += " and u.status = s.codigo";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();

            gdUsers.DataSource = ds;
            gdUsers.DataBind();
        }
    }
}