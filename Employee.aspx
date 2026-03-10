<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="WebApplication1.Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
    <style>
        body {
            font-family: Arial;
            margin: 40px;
        }
        table {
            border-collapse: collapse;
        }
        td {
            padding: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee / User Management</h2>

            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            <br /><br />

            <asp:GridView ID="gvUsers" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                OnRowEditing="gvUsers_RowEditing"
                OnRowCancelingEdit="gvUsers_RowCancelingEdit"
                OnRowUpdating="gvUsers_RowUpdating"
                OnRowDeleting="gvUsers_RowDeleting"
                BorderWidth="1"
                CellPadding="5">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="Contact" HeaderText="Contact" />
                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>

            <br />
            <h3>Add New User</h3>

            <table>
                <tr>
                    <td>Username:</td>
                    <td><asp:TextBox ID="txtNewUsername" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Retype Password:</td>
                    <td><asp:TextBox ID="txtNewRetype" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Name:</td>
                    <td><asp:TextBox ID="txtNewName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Address:</td>
                    <td><asp:TextBox ID="txtNewAddress" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Contact:</td>
                    <td><asp:TextBox ID="txtNewContact" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>