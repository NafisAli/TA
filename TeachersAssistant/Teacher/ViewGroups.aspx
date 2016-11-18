<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewGroups.aspx.cs" Inherits="TeachersAssistant.Teacher.ViewGroups" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="ButtonBack" runat="server" Text="Back" OnClick="ButtonBack_Click" style="height: 26px; width: 46px" />
        <asp:GridView ID="GridViewGroupDetails" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
