<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="request_page_admin.aspx.cs" Inherits="LibraryEnterprise.request_page_admin" %>

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
            <h2 id="book_database_title" class="title2">Book Request Page</h2>
            <table id="main_table_container">
                <tr>
                    <td>
                        <div id="modify_books_container">
                            <table id="modify_books_table">
                            <tr>
                                <th colspan="2" class="title2">
                                    Book Requests:
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Request ID:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_request_id" CssClass="add_modify" runat="server"></asp:TextBox>
                                </td>
                            </tr>                       
                            <tr>
                                <td>

                                </td>
                                <td>
                                    <asp:Button ID="btn_delete" CssClass="add_modify btn" runat="server" Text="Delete" OnClick="btn_delete_Click"/>
                                </td>
   <%--                             <td>
                                    <asp:Button ID="btn_add" CssClass="add_modify" runat="server" Text="Complete Request" OnClick="btn_add_Click"/>
                                </td>--%>
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
