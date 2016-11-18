<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TeachersAssistant.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Admin Panel</h2>
    
    <br />
    <br />
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <form id="form1" runat="server">
        <asp:Button ID="ButtonSignOut" runat="server" Text="Sign Out" OnClick="ButtonSignOut_Click" CausesValidation="False" />
    <div>
        <table>
            <tr>                
                <td><asp:Button ID="ButtonAddTeacher" runat="server" Text="Add Teacher" OnClick="ButtonAddTeacher_Click" UseSubmitBehavior="False" CausesValidation="False" /></td>
                <td><asp:Button ID="ButtonViewTeacher" runat="server" Text="View Teachers" OnClick="ButtonViewTeacher_Click" CausesValidation="False" /></td>
                <td><asp:Button ID="ButtonAddStudent" runat="server" Text="Add Student" OnClick="ButtonAddStudent_Click" UseSubmitBehavior="False" CausesValidation="False" /></td>
                <td><asp:Button ID="ButtonViewStudent" runat="server" Text="View Students" OnClick="ButtonViewStudent_Click" CausesValidation="False" /></td>
            </tr>
        </table><br />
        <asp:MultiView ID="MultiViewAdmin" runat="server">
            <asp:View ID="ViewAddTeacher" runat="server">
                <table>
                    <tr>
                        <td><asp:Label ID="LabelTeacherId" runat="server" Text="Teacher ID"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxTeacherId" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorTeacherID" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxTeacherId" Display="Dynamic" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidatorTeacherID" runat="server" ErrorMessage="Invalid ID format" ControlToValidate="TextBoxTeacherId" ValidationExpression="[1][1]-[0-9]{5}-[0-9]{2}" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelTeacherPassword" runat="server" Text="Password"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxTeacherPassword" TextMode="Password" runat="server" ToolTip="Password must contain 8 charachters with atleast one numeric and one special charachter"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorTeacherPassword" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxTeacherPassword" ForeColor="Red" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidatorTeacherPassword" runat="server" ErrorMessage="Invalid password format" ControlToValidate="TextBoxTeacherPassword" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&amp;])[A-Za-z\d$@$!%*#?&amp;]{8,}$" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelTeacherName" runat="server" Text="Name"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxTeacherName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorTeacherName" runat="server" ErrorMessage="Required" Text="*" ControlToValidate="TextBoxTeacherName" ForeColor="Red"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelTeacherEmail" runat="server" Text="Email"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxTeacherEmail" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorTeacherEmail" runat="server" ErrorMessage="Required" Text="*" SetFocusOnError="True" Display="Dynamic" ControlToValidate="TextBoxTeacherEmail" ForeColor="Red"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidatorTeacherEmail" runat="server" ErrorMessage="Invalid e-mail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxTeacherEmail" ForeColor="Red"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="ButtonAddNewTeacher" runat="server" Text="Add" OnClick="ButtonAddNewTeacher_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewViewTeachers" runat="server">
                <asp:GridView ID="GridViewTeachers" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCancelingEdit="GridViewTeachers_RowCancelingEdit" OnRowDeleting="GridViewTeachers_RowDeleting" OnRowEditing="GridViewTeachers_RowEditing" OnRowUpdating="GridViewTeachers_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Password" HeaderText="Password" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:CommandField HeaderText="Options" ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                </asp:GridView><br />
                <asp:Button ID="ButtonConfirmViewTeacher" runat="server" Text="Confirm" OnClick="ButtonConfirmViewTeacher_Click" CausesValidation="False" /><asp:Button ID="ButtonUndoViewTeacher" runat="server" Text="Undo" OnClick="ButtonUndoViewTeacher_Click" CausesValidation="False" />
            </asp:View>
            <asp:View ID="ViewAddStudent" runat="server">
                  <table>
                    <tr>
                        <td><asp:Label ID="LabelStudentID" runat="server" Text="Student ID"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxStudentID" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentId" runat="server" ErrorMessage="Required" Text="*" ControlToValidate="TextBoxStudentID" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelStudentPassword" runat="server" Text="Password"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxStudentPassword" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentPassword" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBoxStudentPassword" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidatorStudentPassword" runat="server" ErrorMessage="Invalid password format" ControlToValidate="TextBoxStudentPassword" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&amp;])[A-Za-z\d$@$!%*#?&amp;]{8,}$" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelStudentName" runat="server" Text="Name"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxStudentName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentName" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxStudentName" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelStudentGender" runat="server" Text="Gender"></asp:Label></td>
                        <td>
                            <asp:RadioButton ID="RadioButtonMale" runat="server" GroupName="GenderGroup" Text="Male" /><asp:RadioButton ID="RadioButtonFemale" runat="server" GroupName="GenderGroup" Text="Female" /></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="LabelStudentEmail" runat="server" Text="Email"></asp:Label></td>
                        <td><asp:TextBox ID="TextBoxStudentEmail" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentEmail" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxTeacherEmail" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidatorStudentEmail" runat="server" ErrorMessage="Invalid e-mail" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxStudentEmail"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="ButtonAddNewStudent" runat="server" Text="Add" OnClick="ButtonAddNewStudent_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewViewStudents" runat="server">
                <asp:GridView ID="GridViewStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCancelingEdit="GridViewStudents_RowCancelingEdit" OnRowDeleting="GridViewStudents_RowDeleting" OnRowEditing="GridViewStudents_RowEditing" OnRowUpdating="GridViewStudents_RowUpdating">

                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Password" HeaderText="Password" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Cgpa" HeaderText="CGPA" />
                        <asp:BoundField DataField="CreditsCompleted" HeaderText="Credits Completed" />
                        <asp:BoundField DataField="Semester" HeaderText="Semester" />
                        <asp:CommandField HeaderText="Options" ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>

                    <RowStyle HorizontalAlign="Center" />

                </asp:GridView>
                <asp:Button ID="ButtonConfirmViewStudent" runat="server" Text="Confirm" OnClick="ButtonConfirmViewStudent_Click" CausesValidation="False" /><asp:Button ID="ButtonUndoViewStudent" runat="server" Text="Undo" OnClick="ButtonUndoViewStudent_Click" CausesValidation="False" />
            </asp:View>
            
        </asp:MultiView>
        
    </div>
    </form>
</body>
</html>
