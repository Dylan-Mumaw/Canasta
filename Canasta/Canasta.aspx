<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Canasta.aspx.cs" Inherits="Canasta.Canasta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="C#" runat="server">

        void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            //TextBox1.Text = "test";
        }

   </script>
   <link rel="stylesheet" type="text/css" href="../StyleSheet/Canasta_Style.css"/>
</head>
<body>
    <form id = "form1" runat="server">
        <asp:Panel ID = "TestImages" runat="server">
        </asp:Panel>
        <br />
        <asp:Panel ID = "MeldPanel" runat = "server">
        </asp:Panel>
        <br />
        <asp:Label ID = "MeldLabel" Text = "Meld Test" runat = "server"></asp:Label>
        <br />        
        <asp:ImageButton ID = "PickUpPileImage" runat="server" ImageUrl="~/Images/CardBack.png" Height="200" OnClick="PickUpPileImage_Click" />
        <br />
        <asp:Label ID = "PickUpPileLabel" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:ImageButton ID = "DiscardPileImage" runat="server" Height="200" CausesValidation = "false" OnClick="DiscardPileImage_Click" />
        <br />
        <asp:Button ID= "MeldButton" Text = "Meld Cards" runat = "server" OnClick="MeldButton_Click" />
    </form>
</body>
</html>
