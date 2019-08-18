<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout_history.aspx.cs" Inherits="LibraryEnterprise.checkout_history" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Library Management System</title>
    <link rel="stylesheet" href="css/main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main_container">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="return" runat="server" PostBackUrl="~/homepage.aspx" Text="Return" CssClass="btn" />
            <br />
            <h2 id="book_database_title" class="title2">Checkout History</h2>
            <table id="main_table_container">
                <tr>
                    <td>
                        <div id="checkout_container">
                            <table id="search_books_table">
                                <tr>
                                    <th colspan="2" class="title2">Search
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_email" runat="server" Text="Email"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_isbn" runat="server" Text="ISBN: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_isbn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="btn_search_container">
                                            <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" CssClass="btn" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="gridview_container_large">
                <asp:GridView ID="gridview_books" runat="server"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
