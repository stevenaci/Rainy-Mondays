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
        .auto-style2 {
            height: 684px;
            margin-bottom: 0px;
        }
        .auto-style3 {
            height: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div id="main_container" class="auto-style2">
            <br />
<%--            <table style="width:100%;">
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
            </table>--%>
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
                    <td class="auto-style3"></td>
                    <td class="auto-style3">
                        <asp:Label ID="Label3" runat="server" CssClass="subtitle" Text="Account Management"></asp:Label>
                    </td>
                    <td class="auto-style3"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnemploy" runat="server" Text="View Employees" OnClick="btnemploy_Click" CssClass="center" PostBackUrl="~/displayEmployees.aspx" />
                    </td>
                    <td>
                        <asp:Image ID="Image1" runat="server" CssClass="icon" ImageUrl="~/resources/images/account.png" />
                    </td>
                    <td>
                        <asp:Button ID="btnpatron" runat="server" Text="View Patrons" OnClick="btnpatron_Click" CssClass="center" PostBackUrl="~/display_patrons.aspx" style="height: 29px" />
                    </td>
                </tr>
<%--                <tr>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="addpatron" runat="server" OnClick="addpatron_Click" Text="Add a Patron" />
                    </td>
                </tr>--%>
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
                    <td>&nbsp;</td>
                    <td>
                        <asp:Image ID="Image2" runat="server" CssClass="icon" ImageUrl="~/resources/images/book.png" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btncheckout" runat="server" Text="Check Out" OnClick="btncheckout_Click" CssClass="center" PostBackUrl="~/checkout.aspx" />
                    </td>
                    <td>
                        <asp:Button ID="btnsearch" runat="server" CssClass="center" Text="Book Database" PostBackUrl="~/books_database_employees.aspx" OnClick="btnsearch_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btncheckin" runat="server" Text="Check In" OnClick="btncheckin_Click" CssClass="center" PostBackUrl="~/checkin.aspx" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:Button ID="btnadd" runat="server" Text="Add Book" CssClass="center" />--%>
                    </td>
                    <td>
                        <asp:Button ID="btnupdate" runat="server" Text="Patron Requests" CssClass="center" PostBackUrl="~/request_page_admin.aspx" />
                    </td>
                    <td>
                        <%--<asp:Button ID="btndelete" runat="server" Text="Delete Book" OnClick="btndelete_Click" CssClass="center" />--%>
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
