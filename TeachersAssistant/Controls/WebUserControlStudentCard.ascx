<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStudentCard.ascx.cs" Inherits="TeachersAssistant.WebUserControlStudentCard" EnableTheming="True" %>


<asp:Table ID="TableCardLayout" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Dashed" GridLines="Both">
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Image ID="ImageStudent" runat="server" /></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelStudentName" runat="server" Text=""></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelStudentId" runat="server" Text=""></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelStudentGender" runat="server" Text=""></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelStudentEmail" runat="server" Text=""></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelStudentCgpa" runat="server" Text="CGPA: "></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelStudentSemester" runat="server" Text="Semester: "></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow HorizontalAlign="Center">
        <asp:TableCell><asp:Label ID="LabelCreditsCompleted" runat="server" Text="Credits Completed: "></asp:Label></asp:TableCell>
    </asp:TableRow>   
</asp:Table>