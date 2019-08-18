<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="patronhomepage.aspx.cs" Inherits="LibraryEnterprise.patronhomepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }

.title {
    font-size: 3em;
    color: dimgrey;
    text-align: center;
}

        .auto-style2 {
            height: 156px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style1">
                        <asp:Label ID="Label2" runat="server" CssClass="title" Text="Homepage"></asp:Label>
                    </td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">All Books Owed</td>
                <td class="auto-style2">Overdue Books</td>
                <td class="auto-style2">Total Balance Owed:</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:GridView ID="gv1" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style2">&nbsp;<asp:GridView ID="gv2" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lblbal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style2">
                        <asp:Button ID="Button1" runat="server" Text="Search Library" OnClick="Button1_Click" />
                    </td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
