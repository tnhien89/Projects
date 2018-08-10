<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OtherDetail.aspx.cs" Inherits="AdminSite.OtherDetail" %>

<%@ Register Src="~/UserControls/ucSubOtherInfo.ascx" TagPrefix="uc1" TagName="ucSubOtherInfo" %>
<%@ Register Src="~/UserControls/ucSubOtherItems.ascx" TagPrefix="uc1" TagName="ucSubOtherItems" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucSubOtherInfo runat="server" id="ucSubOtherInfo" />
    <uc1:ucSubOtherItems runat="server" id="ucSubOtherItems" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
