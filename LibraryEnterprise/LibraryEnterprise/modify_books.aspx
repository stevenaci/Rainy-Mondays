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
            
            <table id="modify_books_table">
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
                </table>
            
            <br />
            <div id="gridview_container">
                <asp:GridView ID="gridview_books" runat="server"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
