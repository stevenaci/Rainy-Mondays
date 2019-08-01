<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="LibraryEnterprise.homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div id="main_container">
            <br />
            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:Label ID="lb_user" runat="server"></asp:Label>
                        <asp:Label ID="lb_timestamp" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
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
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="subtitle" Text="Account Management"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnemploy" runat="server" Text="View Employees" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnpatron" runat="server" Text="View Patrons" />
                    </td>
                </tr>
            </table>
            <br />
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="subtitle" Text="Library Database Management"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btncheckout" runat="server" Text="Check Out" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btncheckin" runat="server" Text="Check In" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnadd" runat="server" Text="Add Book" />
                    </td>
                    <td>
                        <asp:Button ID="btnupdate" runat="server" Text="Update Book" />
                    </td>
                    <td>
                        <asp:Button ID="btndelete" runat="server" Text="Delete Book" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            
        </div>
    </form>
</body>
</html>
