<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="IntroductionDetail.aspx.cs" Inherits="AdminSite.IntroductionDetail" %>

<%@ Register Src="~/UserControls/ucNewsDetail.ascx" TagPrefix="uc1" TagName="ucNewsDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function RequiredValidate(sender, e) {
            debugger;
            if (e.Value.length == 0) {
                e.IsValid = false;
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucNewsDetail runat="server" ID="ucNewsDetail" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
