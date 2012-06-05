<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WhoIsThis.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <title>Who Is This?</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <h1>Who Is This?</h1>
    <form id="form1" runat="server" defaultbutton="btnFind">
        <label for="txtUser">AD username:</label>
        <br />
        <asp:TextBox ID="txtUser" runat="server" />
        <br />
        <asp:Button ID="btnFind" runat="server" Text="Find" onclick="btnFind_Click" />
    </form>
    <p>
        <asp:Literal ID="litAnswer" runat="server" />
    </p>
</body>
</html>
