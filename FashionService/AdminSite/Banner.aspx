<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="AdminSite.Banner" %>

<%@ Register Src="~/UserControls/ucBannerInfo.ascx" TagPrefix="uc1" TagName="ucBannerInfo" %>
<%@ Register Src="~/UserControls/ucBannerList.ascx" TagPrefix="uc1" TagName="ucBannerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucBannerInfo runat="server" id="ucBannerInfo" />
    <uc1:ucBannerList runat="server" id="ucBannerList" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
