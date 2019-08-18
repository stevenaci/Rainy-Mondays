<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="request_page.aspx.cs" Inherits="LibraryEnterprise.request_page" %>

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
            <asp:Button ID="return" runat="server" PostBackUrl="~/patronhomepage.aspx" Text="Return" CssClass="btn" />
            <br />
            <h2 id="book_database_title" class="title2">Request a Book</h2>
            <table id="main_table_container">
                <tr>
                    <td>
                        <div id="modify_books_container">
                            <table id="modify_books_table">
                            <tr>
                                <th colspan="2" class="title2">
                                    Book Info:
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="ISBN:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_isbn" CssClass="add_modify" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Author:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_author" CssClass="add_modify" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Title:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_title" CssClass="add_modify" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Genre:"></asp:Label>
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="tb_genre3" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="dd_genre" CssClass="add_modify" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Language:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_language" CssClass="add_modify" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Year:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_year" CssClass="add_modify" runat="server" TextMode="Number"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btn_add" CssClass="btn" runat="server" Text="Add" OnClick="btn_add_Click"/>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
