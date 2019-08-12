<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Site.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/main.css" type="text/css" rel="stylesheet" />
    <link rel="icon" href="imgs/icon.ico" />
    <title>Kurosawa Dia</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lbId" runat="server" Text="Id do Servidor:"></asp:Label>
        <asp:TextBox ID="txId" runat="server"></asp:TextBox>
        <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click1" />
        <br />
        <br />
        <asp:Label ID="lbPrefix" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
