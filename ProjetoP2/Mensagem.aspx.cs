using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Mail;

namespace ProjetoP2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string sc = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\basedados\\basedados.accdb;Persist Security Info=False;";
        protected void Page_Load(object sender, EventArgs e)
        {
            /*this.div_user.Visible = false;
            this.div_tbluser.Visible = false;
            this.btnEnviarMsg.Visible = false;*/

            //this.hideUser(form1,null);

            if (!Page.IsPostBack)
            {
                this.buildDropDe();
                this.buildDropPara();
                this.buildDropTipo();
                this.buildDropStatus();
                this.buildGridMensagems();
            }

            
        }
        protected void buildDropDe()
        {
            OleDbConnection conexao = new OleDbConnection(sc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "select * from usuario where tipoUsuario = 1 and status =1 order by nome ";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();
            //preenchendo o combo
            dropDe.DataSource = ds;
            dropDe.DataValueField = "codigo";
            dropDe.DataTextField = "nome";
            dropDe.DataBind();

            dropDe.Items.Insert(0, new ListItem("Selecione um usuário!", "0"));
        }
        protected void buildDropPara()
        {
            OleDbConnection conexao = new OleDbConnection(sc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "select * from usuario where tipoUsuario > 1 and status =1 order by nome ";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();
            //preenchendo o combo
            dropPara.DataSource = ds;
            dropPara.DataValueField = "codigo";
            dropPara.DataTextField = "nome";
            dropPara.DataBind();

            dropPara.Items.Insert(0, new ListItem("Selecione um usuário!", "0"));
        }
        protected void buildDropTipo()
        {
            OleDbConnection conexao = new OleDbConnection(sc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "select * from tipomensagem order by nomeTipoMensagem";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();
            //preenchendo o combo
            dropTipo.DataSource = ds;
            dropTipo.DataValueField = "codigo";
            dropTipo.DataTextField = "nomeTipoMensagem";
            dropTipo.DataBind();

            dropTipo.Items.Insert(0, new ListItem("Selecione um tipo !", "0"));
        }
        protected void buildDropStatus()
        {
            OleDbConnection conexao = new OleDbConnection(sc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "select * from status order by nomeStatus";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();
            //preenchendo o combo
            dropStatus.DataSource = ds;
            dropStatus.DataValueField = "codigo";
            dropStatus.DataTextField = "nomeStatus";
            dropStatus.DataBind();

            dropStatus.Items.Insert(0, new ListItem("Selecione um status !", "0"));
        }
        protected void buildGridMensagems()
        {
            OleDbConnection conexao = new OleDbConnection(sc);
            conexao.Open();
            /* Busco todos os usuários excetos os admin*/
            string sql = "SELECT m.codigo as Código";
            sql += ",m.titulo as Titulo ,m.mensagem as Mensagem";
            sql += ",m.datamensagem as Data ";
            sql += ",(select uu.nome from usuario uu where uu.codigo = m.codigoDe ) as De";
            sql += ",(select u.nome from usuario u where u.codigo = m.codigoPara ) as Para";
            sql += ",t.nomeTipoMensagem as Tipo";
            sql += ",s.nomestatus as Status";
            sql += " from mensagem m ,tipomensagem t, status s";
            sql += " where m.tipomensagem = t.codigo";
            sql += " and m.status = s.codigo";
            DataSet ds = new DataSet();
            OleDbDataAdapter conversor = new OleDbDataAdapter(sql, conexao);
            conversor.Fill(ds);
            conexao.Close();

            gdMsgs.DataSource = ds;
            gdMsgs.DataBind();
        }
        protected void hideMsg(object sender, EventArgs e)
        {
          
            this.div_msg.Visible = false;
            this.div_enviadas.Visible = false;
            
        }

        protected void openAddUser(object sender, EventArgs e)
        {
            Response.Redirect("Usuario.aspx");
        }

        protected void enviarEmail(object sender,EventArgs e)
        {
            string de = "";
            string para = "";
            string titulo = this.txttitulo.Text;
            string msg = this.txtmensagem.Text;
            string hoje = DateTime.Today.ToShortDateString();
            string tipomsg = this.dropTipo.SelectedItem.Value;
            string tipomsgtxt = this.dropTipo.SelectedItem.Text;
            string status = this.dropStatus.SelectedItem.Value;
            string statustxt = this.dropStatus.SelectedItem.Text;

            OleDbConnection conexao = new OleDbConnection(sc);
            conexao.Open();

            string sqlde = "select * from usuario where codigo =" + this.dropDe.SelectedItem.Value;

          
            OleDbCommand comandode = new OleDbCommand(sqlde, conexao);
            OleDbDataReader retorno = comandode.ExecuteReader();

            if (retorno.Read())
            {
                de = retorno["email"].ToString();
            }
            

            string sqlpara = "select * from usuario where codigo =" + this.dropPara.SelectedItem.Value;
            OleDbCommand comandopara = new OleDbCommand(sqlpara, conexao);
            OleDbDataReader retornoPara = comandopara.ExecuteReader();

            if (retornoPara.Read())
            {
                para = retornoPara["email"].ToString();
            }

            MailAddress emailde = new MailAddress(de);
            MailAddress emailpara = new MailAddress(para);
            MailMessage mail = new MailMessage();

            mail.From = emailde;
            mail.To.Add(emailpara);

            mail.Subject = titulo;

            string htmlBody = "";
            htmlBody += "<table>";
            htmlBody += "<tr>";
            htmlBody += "<td>Titulo:</td>";
            htmlBody += "<td>" + titulo + "</td>";
            htmlBody += "</tr>";
            htmlBody += "<tr>";
            htmlBody += "<td>Mensagem:</td>";
            htmlBody += "<td>" + msg+ "</td>";
            htmlBody += "</tr>";
            htmlBody += "<tr>";
            htmlBody += "<td>Tipo:</td>";
            htmlBody += "<td>" + tipomsgtxt+ "</td>";
            htmlBody += "</tr>";
            htmlBody += "<tr>";
            htmlBody += "<td>Status:</td>";
            htmlBody += "<td>" + statustxt+ "</td>";
            htmlBody += "</tr>";           
            htmlBody += "</table>";
            mail.Body = htmlBody;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
            try
            {
                smtp.Port = 587;
                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential("fatecpwads2016@outlook.com", "FreiJoao59");
                smtp.Send(mail);
                lblinfo.Text = "Email enviado com sucesso!";
                OleDbConnection conn = new OleDbConnection(sc);
                conn.Open();
                string sqlInsert = "insert into mensagem (titulo,mensagem,dataMensagem,codigoDe,codigoPara,tipoMensagem,status)" +
                    "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                sqlInsert = String.Format(sqlInsert, titulo, msg, hoje, this.dropDe.SelectedItem.Value, this.dropPara.SelectedItem.Value, tipomsg, status);
                OleDbCommand command = new OleDbCommand(sqlInsert, conn);
                command.ExecuteNonQuery();
                conn.Close();
                buildGridMensagems();

            }
            catch
            {
                lblinfo.Text = "Ocorreu um erro no envio de email";
            }

        }
       
       
    }
}