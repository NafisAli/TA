<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TeachersAssistant.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Home</h2>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2"><asp:Label ID="LabelStudentName" runat="server" Text=""></asp:Label><br /></td>
            </tr>
            <tr>
                <td><asp:Button ID="ButtonStudentProfile" runat="server" Text="My Profile" OnClick="ButtonStudentProfile_Click" /></td><td><asp:Button ID="ButtonLogout" runat="server" Text="Sign Out" OnClick="ButtonLogout_Click" /></td>
            </tr>
        </table>
    </div>    
    </form>
</body>
</html>
