<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TeachersAssistant.Teacher.Dashboard" %>

<%@ Register TagPrefix="sc" TagName="WebUserControlStudentCard" Src="~/Controls/WebUserControlStudentCard.ascx" %>

<%@ Reference Control="~/Controls/WebUserControlStudentCard.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Dashboard</h2>
    <form id="form1" runat="server">
    <asp:Button ID="ButtonViewGroups" runat="server" Text="View Groups" OnClick="ButtonViewGroups_Click" CausesValidation="False" /><asp:Button ID="ButtonTeacherSignout" runat="server" Text="Sign Out" OnClick="ButtonTeacherSignout_Click" CausesValidation="False" />
    <div style="float: left; margin-right: 8px; width: 350px; table-layout: auto; border-spacing: 2px;">
        <h4>Filters</h4>
        <table>
            <tr>
                <td><asp:Label ID="LabelSearchCourse" runat="server" Text="Course"></asp:Label></td>
                <td><asp:DropDownList ID="DropDownListCourseList" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelSearchSemester" runat="server" Text="Semester"></asp:Label></td>
                <td><asp:DropDownList ID="DropDownListSemesterList" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelSearchByCgpa" runat="server" Text="CGPA"></asp:Label></td>
                <td><asp:RadioButton ID="RadioButtonCgpaGreaterThan" runat="server" GroupName="CgpaComparison" Text="&gt;" Checked="True" /><asp:RadioButton ID="RadioButtonCgpaLessThan" runat="server" GroupName="CgpaComparison" Text="&lt;" /><asp:Label ID="LabelText1" runat="server" Text=" than "></asp:Label><asp:TextBox ID="TextBoxSearchByCgpa" runat="server"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidatorSearchCgpa" runat="server" ErrorMessage="?" ControlToValidate="TextBoxSearchByCgpa" ForeColor="Red" ToolTip="Invalid CGPA" ValidationExpression="^(?:[1-9]\d*|0)?(?:\.\d+)?$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="LabeSearchByAbsence" runat="server" Text="Absence"></asp:Label></td>
                <td><asp:RadioButton ID="RadioButtonAbsenceGreaterThan" runat="server" GroupName="AbsenceComparison" Text="&gt;" Checked="True" /><asp:RadioButton ID="RadioButtonAbsenceLessThan" runat="server" GroupName="AbsenceComparison" Text="&lt;" /><asp:Label ID="LabelText2" runat="server" Text=" than "></asp:Label><asp:TextBox ID="TextBoxSearchByAbsence" runat="server"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidatorSearchAbsence" runat="server" ErrorMessage="?" ControlToValidate="TextBoxSearchByAbsence" ForeColor="Red" ToolTip="Invalid number" ValidationExpression="^([1-9][0-9]{0,2}|1000)$"></asp:RegularExpressionValidator></td>
            </tr>            
            <tr>
                <td><asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" /></td>
                <td><asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click" CausesValidation="False" /></td>
            </tr>
        </table>
    </div>
    <div style="margin-left: 358px; height: 256px; overflow: scroll;">
        <h4>List</h4>
        <asp:GridView ID="GridViewStudentSearch" runat="server" DataKeyNames="StudentId" OnSelectedIndexChanged="GridViewStudentSearch_SelectedIndexChanged" AllowSorting="True" OnSorting="GridViewStudentSearch_Sorting">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>    
    <div>
        <h2>Compare Window</h2>
        <asp:Button ID="ButtonResetCompare" runat="server" Text="Reset" OnClick="ButtonResetCompare_Click" CausesValidation="False" />
        <asp:Button ID="ButtonCreateGroup" runat="server" Text="Create Group" OnClick="ButtonCreateGroup_Click" CausesValidation="False" />
        <asp:Panel ID="PanelCard" runat="server">
            
        </asp:Panel>
    </div>
    </form>
</body>
</html>
