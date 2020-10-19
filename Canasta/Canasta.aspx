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
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="TestImages" runat="server">
        </asp:Panel>        
        <br />
        <asp:ImageButton ID="PickUpPileImage" runat="server" ImageUrl="~/Images/CardBack.png" Height="200" CausesValidation="False" OnClick="PickUpPileImage_Click" />
        <br />
        <asp:Label ID="PickUpPileLabel" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:ImageButton ID="DiscardPileImage" runat="server" Height="200" />
    </form>

</body>
</html>
