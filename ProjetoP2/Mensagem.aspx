<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mensagem.aspx.cs" Inherits="ProjetoP2.WebForm1" %>

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
      <%-- <asp:Button ID="btnuser" Text="Adicionar usuário" runat="server" CssClass="botao" OnClick="openAddUser" CausesValidation="False" />--%>
   </div>
    <div id="div_msg" runat="server">
    <fieldset>
        <legend>Envio de mensagem</legend>
        <br/>
        <table id="tblMsg">
           <%-- <tr>
                <td>Código:</td>
                <td><asp:TextBox runat="server" ID="txtcodigo" ></asp:TextBox></td>
            </tr>--%>
            <tr>
                <td>Título:</td>
                <td><asp:TextBox ID="txttitulo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredTitulo" runat="server" ErrorMessage="Favor inserir um título!" ControlToValidate="txttitulo"></asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td>Mensagem</td>
                <td>
                    <asp:TextBox ID="txtmensagem" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredMensagem" runat="server" ErrorMessage="Favor inserir uma mensagem!" ControlToValidate="txtmensagem"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>De:</td>
                <td>
                    <asp:DropDownList ID="dropDe" runat="server">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rangeDropDe" runat="server" ErrorMessage="Selecione um usuário!" ControlToValidate="dropDe" MaximumValue="999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                   
                </td>
            </tr>
            <tr>
                <td>Para:</td>
                <td> <asp:DropDownList ID="dropPara" runat="server">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rangeDropPara" runat="server" ErrorMessage="Selecione um usuário!" ControlToValidate="dropPara" MaximumValue="999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>Tipo:</td>
                <td> <asp:DropDownList ID="dropTipo" runat="server">
                    </asp:DropDownList>
                <asp:RangeValidator ID="rangeTipo" runat="server" ErrorMessage="Selecione um tipo!" ControlToValidate="dropTipo" MaximumValue="999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>Status</td>
                <td><asp:DropDownList ID="dropStatus" runat="server">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rangeStatus" runat="server" ErrorMessage="Selecione um status!" ControlToValidate="dropStatus" MaximumValue="999999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnenviar" CssClass="botao" Text="Enviar" runat="server" OnClick="enviarEmail" />
                </td>
            </tr>
        </table>
    </fieldset>
    </div>
        <asp:Label ID="lblinfo" runat="server" Text=""></asp:Label>
        <br/>
        <div id="div_enviadas" runat="server">
            <fieldset>
                <legend>Mensagens enviadas:</legend>
                <br/>
                <asp:GridView ID="gdMsgs" runat="server"></asp:GridView>
            </fieldset>
        </div>
       
    </form>
</body>
</html>
