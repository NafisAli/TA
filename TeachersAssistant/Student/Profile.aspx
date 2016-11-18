<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TeachersAssistant.Student.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLinkBackToStudentHome" runat="server" NavigateUrl="~/Student/Home.aspx">Home</asp:HyperLink><br />
        <br />
        <table>
            <tr>
                <td>ID</td>
                <td><asp:Label ID="LabelStudentID" runat="server" Text=""></asp:Label></td>                
            </tr>            
            <tr>
                <td>Name</td>
                <td><asp:Label ID="LabelStudentName" runat="server" Text=""></asp:Label></td>                
            </tr>
            <tr>
                <td>Gender</td>
                <td><asp:Label ID="LabelStudentGender" runat="server" Text=""></asp:Label></td>                
            </tr>
            <tr>
                <td>Email</td>
                <td><asp:Label ID="LabelStudentEmail" runat="server" Text=""></asp:Label></td>                
            </tr>
            <tr>
                <td>CGPA</td>
                <td><asp:Label ID="LabelStudentCgpa" runat="server" Text=""></asp:Label></td>                
            </tr>
            <tr>
                <td>Semester</td>
                <td><asp:Label ID="LabelStudentSemester" runat="server" Text=""></asp:Label></td>                
            </tr> 
            <tr>
                <td>Credits Completed</td>
                <td><asp:Label ID="LabelStudentCreditsCompleted" runat="server" Text=""></asp:Label></td>                
            </tr>        
        </table>        
    </div>
    </form>
</body>
</html>
