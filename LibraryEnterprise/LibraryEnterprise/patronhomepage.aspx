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

        .auto-style3 {
            height: 64px;
        }

    </style>
</head>
<body>
    <br />
    <br />
    <div id="main_container">
    <form id="form1" runat="server">
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="logout" runat="server" OnClick="logout_Click" PostBackUrl="~/index.aspx" Text="Logout" CssClass="btn" />
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
                        <asp:Label ID="lbl1" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Books must be returned after 14 days! Outstanding books will cost a fee of $0.25 per day.</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        <table style="width:100%;">
            <tr>
                <td class="auto-style3">All Books Owed</td>
                <td class="auto-style3">Overdue Books<br />
                </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:GridView ID="gv1" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style2"><asp:GridView ID="gv2" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style2">
                    Total Balance Owed: 
                    <asp:Label ID="lblbal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <t&nbsp;</td>
                <td class="auto-style2">
                        <asp:Button ID="Button1" runat="server" Text="Search Library" OnClick="Button1_Click" PostBackUrl="~/books_database_patrons.aspx" CssClass="btn" />
                </td>
                <td class="auto-style2">
                    <asp:Button ID="Button2" runat="server" Text="Request Book" PostBackUrl="~/request_page.aspx" CssClass="btn" />
                </td>
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
    </div>
</body>
</html>
