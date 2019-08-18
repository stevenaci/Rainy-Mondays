<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="patronloginpage.aspx.cs" Inherits="LibraryEnterprise.patronloginpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div id="main_container">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="return" runat="server" PostBackUrl="~/index.aspx" Text="Return" CssClass="btn" />
            <table style="width:100%;">
                <br />
                <br />
                <br />
                <br />
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Patron Log in"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Email</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;&nbsp;<asp:TextBox ID="tb1" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Password</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="tb2" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btn1" runat="server" OnClick="btn1_Click" Text="Login" CssClass="btn" />
                        <asp:Button ID="btn2" runat="server" OnClick="btn2_Click" Text="Register" PostBackUrl="~/registration.aspx" CssClass="btn" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="lbldebug" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
