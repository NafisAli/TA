<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TeachersAssistant.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Login</h2>
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td><asp:Label ID="LabelId" runat="server" Text="User ID"></asp:Label></td>
                <td><asp:TextBox ID="TextBoxId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelPass" runat="server" Text="Password"></asp:Label></td>
                <td><asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="ButtonLogin" runat="server" Text="Login" OnClick="ButtonLogin_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
