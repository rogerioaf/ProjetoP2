<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ProjetoP2.Usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <div>
       <%--<asp:Button ID="btnmsg" Text="Enviar mensagem" runat="server" CssClass="botao" OnClick="openAddUser" CausesValidation="False" />--%>
   </div>
    <div>
    <table>
        <tr>
            <td>Código: </td>
            <td><asp:TextBox ID="txtcodigo" runat="server"></asp:TextBox>
            <asp:Button ID="btnConsultar" runat="server" CssClass="botao" Text="Consultar" OnClick="consultar" /></td>
        </tr>
        <tr>
            <td>Nome: </td>
            <td><asp:TextBox ID="txtnome" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email: </td>
            <td><asp:TextBox ID="txtemail" runat="server" TextMode="Email"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Senha: </td>
            <td><asp:TextBox ID="txtsenha" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Telefone: </td>
            <td><asp:TextBox ID="txtTelefone" runat="server" TextMode="Phone"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Tipo: </td>
            <td><asp:DropDownList ID="cboTipo" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Status: </td>
            <td><asp:DropDownList ID="cboStatus" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAlterar" CssClass="botao" runat="server" Text="Alterar" OnClick="alterar" BorderStyle="None" />
                <asp:Button ID="btnIncluir" CssClass="botao" runat="server" OnClick="Inserir" Text="Inserir" BorderStyle="None" />
                 <asp:Button ID="btnExcluir" CssClass="botao" runat="server" Text="Excluir" OnClick="excluir" BorderStyle="None" />
                <asp:Button ID="btnLimpar" CssClass="botao" runat="server" Text="Limpar" OnClick="limpar" BorderStyle="None" />
             </td>
        </tr>
    </table>
    </div>
        <asp:Label ID="info_msg" runat="server"></asp:Label>
    <div>
        <fieldset>
                <legend>Usuários:</legend>
                <br/>
                <asp:GridView ID="gdUsers" runat="server"></asp:GridView>
            </fieldset>
    </div>
    </form>
</body>
</html>
