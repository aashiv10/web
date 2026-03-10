<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login Page</title>
    <style>
        body {
            font-family: Arial;
            margin: 40px;
        }
        .box {
            width: 350px;
            padding: 20px;
            border: 1px solid #ccc;
        }
        .row {
            margin-bottom: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>Login Page</h2>

            <div class="row">
                Username:<br />
                <asp:TextBox ID="txtUsername" runat="server" Width="250px"></asp:TextBox>
            </div>

            <div class="row">
                Password:<br />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
            </div>

            <div class="row">
                Retype Password:<br />
                <asp:TextBox ID="txtRetype" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
            </div>

            <div class="row">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                &nbsp;
                <asp:LinkButton ID="lnkShowReset" runat="server" OnClick="lnkShowReset_Click">Reset Password</asp:LinkButton>
            </div>

            <div class="row">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <asp:Panel ID="pnlReset" runat="server" Visible="false" Style="margin-top:15px; border:1px solid #ccc; padding:10px;">
                <h4>Reset Password</h4>

                <div class="row">
                    Username:<br />
                    <asp:TextBox ID="txtResetUsername" runat="server" Width="250px"></asp:TextBox>
                </div>

                <div class="row">
                    New Password:<br />
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                </div>

                <div class="row">
                    Retype New Password:<br />
                    <asp:TextBox ID="txtNewRetype" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                </div>

                <div class="row">
                    <asp:Button ID="btnReset" runat="server" Text="Reset Password" OnClick="btnReset_Click" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>