<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leave.aspx.cs" Inherits="Leave.leave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Enter Employee Name :--"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <br />
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Enter Employee ID :--"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Your Selected Date"></asp:Label>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="final" runat="server" Text="Submit" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>