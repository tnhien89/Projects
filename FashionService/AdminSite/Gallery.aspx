<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="AdminSite.WebForm4" %>

<%@ register src="~/UserControls/ucGalleryItems.ascx" tagprefix="uc1" tagname="ucGalleryItems" %>
<%@ register src="~/UserControls/ucMenuDetail.ascx" tagprefix="uc1" tagname="ucMenuDetail" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucMenuDetail runat="server" id="ucMenuDetail" />
    <uc1:ucGalleryItems runat="server" id="ucGalleryItems" />
</asp:Content>
