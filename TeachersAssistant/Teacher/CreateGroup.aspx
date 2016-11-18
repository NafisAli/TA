<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="TeachersAssistant.Teacher.CreateGroup" %>

<%@ Register TagPrefix="sc" TagName="WebUserControlStudentCard" Src="~/Controls/WebUserControlStudentCard.ascx" %>

<%@ Reference Control="~/Controls/WebUserControlStudentCard.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Selected Students</h2>
        <asp:Panel ID="PanelCard" runat="server"></asp:Panel><br />
        <h2>Group Information</h2>
        <table>
            <tr>
                <td><asp:Label ID="LabelGroupId" runat="server" Text="Group Id: "></asp:Label></td>
                <td><asp:TextBox ID="TextBoxGroupId" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorGroupId" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBoxGroupId" ForeColor="Red">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidatorGroupId" runat="server" ErrorMessage="Invalid Group ID" ControlToValidate="TextBoxGroupId" ForeColor="Red" ValidationExpression="^([1-9][0-9]{0,2}|1000)$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelGroupName" runat="server" Text="Group Name: "></asp:Label></td>
                <td><asp:TextBox ID="TextBoxGroupName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorGroupName" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxGroupName" EnableTheming="True" ForeColor="Red">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelGroupDescription" runat="server" Text="Group Description"></asp:Label></td>
                <td><asp:TextBox ID="TextBoxGroupDescription" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorGroupDesc" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxGroupDescription" ForeColor="Red">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Button ID="ButtonCancelGroup" runat="server" Text="Cancel" CausesValidation="False" OnClick="ButtonCancelGroup_Click" /></td>
                <td><asp:Button ID="ButtonCreate" runat="server" Text="Create" OnClick="ButtonCreate_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
