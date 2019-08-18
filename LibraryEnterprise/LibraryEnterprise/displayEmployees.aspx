<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="displayEmployees.aspx.cs" Inherits="LibraryEnterprise.displayEmployees" %>

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
            <h2 id="book_database_title" class="title2">Employee Database</h2>
            <table id="main_table_container">
                <tr>
                    <td>
                        <div id="search_books_container">
                            <table id="search_books_table">
                                <tr>
                                    <th colspan="2" class="title2">Search
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_fn" runat="server" Text="First Name: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_fn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_ln" runat="server" Text="Last Name: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_ln" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_email" runat="server" Text="Email Address: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_phone" runat="server" Text="Phone Number: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_phone" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_address" runat="server" Text="Address: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_address" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_balance" runat="server" Text="Balance: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_balance" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="btn_search_container">
                                            <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" CssClass="btn" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <div id="delete_container">
                                            <table id="delete_books_table">
                                                <tr>
                                                    <th colspan="3" class="title2">Delete</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="ID"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_delete_id" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="btn_delete_Click" CssClass="btn" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <div id="modify_books_container">
                            <table id="modify_books_table">
                                <tr>
                                    <th colspan="2" class="title2">Add / Modify</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Modify ID:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_modify_id" runat="server" Width="150px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="First Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_isbm3" CssClass="add_modify" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Last Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_author3" CssClass="add_modify" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_title3" CssClass="add_modify" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Password: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_password" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Phone:"></asp:Label>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="dd_genre" CssClass="add_modify" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Address:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_language3" CssClass="add_modify" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Role:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_year3" CssClass="add_modify" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_Click" CssClass="btn" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_add" CssClass="add_modify btn" runat="server" Text="Add" OnClick="btn_add_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div id="gridview_container">
                <asp:GridView ID="gridview_books" runat="server"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
