<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modify_books.aspx.cs" Inherits="LibraryEnterprise.modify_books" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Library Management System</title>
    <link rel="stylesheet" href="css/main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div id="main_container">
            <br />
            <br />
            <table id="main_table_container">
                <tr>
                    <td>
                        <table id="search_books_table">
                        <tr>
                            <th colspan="2">
                                Search Books
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lb_isbn" runat="server" Text="ISBN"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_isbn" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lb_author" runat="server" Text="Author: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_author" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lb_title" runat="server" Text="Title: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_title" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lb_year" runat="server" Text="Year: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_year" runat="server" TextMode="Number"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="btn_search_container">
                                    <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="delete_container">
                                    <table id="delete_books_table">
                                        <tr>
                                            <th colspan="3">
                                                Delete Books
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="ID"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_delete_id" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="btn_delete_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </td>
<%--                    <td>
                        <table id="delete_books_table">
                            <tr>
                                <th colspan="3">
                                    Delete Books
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_delete_id" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                    <td>
                        <table id="modify_books_table">
                            <tr>
                                <th colspan="2">
                                    Add / Modify Books
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Modify ID:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_modify_id" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="ISBN:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_isbm3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Author:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_author3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Title:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_title3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Genre:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_genre3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Language:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_language3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Year:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_year3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Quantity:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_quantity3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <div id="gridview_container">
                <asp:GridView ID="gridview_books" runat="server"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
