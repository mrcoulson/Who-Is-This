﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WhoIsThis.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <title>Who Is This?</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Who Is This?</h1>
        <p>
            <label for="txtUser">AD username:</label>
            <br />
            <asp:TextBox ID="txtUser" runat="server" />
            <br />
            <asp:Button ID="btnFind" runat="server" Text="Find" onclick="btnFind_Click" />
            <br />
            <asp:Literal ID="litAnswer" runat="server" Text="The answer will go here." />
        </p>
    </div>
    </form>
</body>
</html>
