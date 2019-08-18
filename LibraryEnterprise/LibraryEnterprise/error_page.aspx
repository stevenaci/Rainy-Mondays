<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error_page.aspx.cs" Inherits="LibraryEnterprise.error_page" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Library Management System</title>
    <link rel="stylesheet" href="css/main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main_container">
            <br />
            <table id="main_table_container">
                <div id="error">
                    <tr>
                        <td>
                            <h3 id="error_title" runat="server" class="title">TEST</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p id="error_message" runat="server" class="title2">message</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btn_return" runat="server" OnClick="btn_return_Click">Return</asp:LinkButton>
                        </td>
                    </tr>
                </div>
            </table>
            <br />
            <br />
        </div>
    </form>
</body>
</html>