<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkin.aspx.cs" Inherits="LibraryEnterprise.checkin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }

        .auto-style2 {
            height: 33px;
        }

        .auto-style3 {
            height: 193px;
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
                    <td><asp:Label runat="server" CssClass="subtitle" Text="Check In"></asp:Label></td>
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
                    <td>First Name</td>
                    <td>ISBN </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tb1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb3" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Last Name</td>
                    <td class="auto-style2">Title</td>
                    <td class="auto-style2">
                        <asp:Button ID="btn1" runat="server" Text="Search" OnClick="btn1_Click" CssClass="btn" />
                    </td>
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
                <tr>
                    <td class="auto-style1">
                        <asp:DropDownList ID="ddpatrons" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="ddbooks" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="debug" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btn2" runat="server" Text="Check In" OnClick="btn2_Click" CssClass="btn" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>


