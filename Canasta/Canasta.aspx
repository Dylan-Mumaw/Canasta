<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Canasta.aspx.cs" Inherits="Canasta.Canasta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="TestImages" runat="server">
        </asp:Panel>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:ImageButton ID="PickUpPileImage" runat="server" ImageUrl="~/Images/CardBack.png" Height="200" CausesValidation="False" OnClick="PickUpPileImage_Click" />
        <br />
        <asp:Label ID="PickUpPileLabel" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:ImageButton ID="DiscardPileImage" runat="server" Height="200" />
    </form>
</body>
</html>
