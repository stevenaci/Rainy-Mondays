<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="LibraryEnterprise.checkout" %>

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
    <div id="main_container">
        <form id="form1" runat="server">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="return" runat="server" PostBackUrl="~/homepage.aspx" Text="Return" CssClass="btn" />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:Label ID="Label2" runat="server" Text="Checkout" CssClass="subtitle"></asp:Label></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Search User</td>
                    <td>Search Book</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">First Name</td>
                    <td class="auto-style1">ISBN</td>
                    <td class="auto-style1">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tb1" runat="server"></asp:TextBox>
                    </td>
                    <td>

                        <asp:TextBox ID="tb3" runat="server"></asp:TextBox>

                    </td>
                    <td>
                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn" />
                    </td>
                </tr>
                <tr>
                    <td>Last Name</td>
                    <td>Title</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tb2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb4" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddpatrons" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddbooks" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btncheck" runat="server" Text="Checkout" OnClick="btncheck_Click" CssClass="btn" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="debug" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
        </form>
    </div>
</body>
</html>
