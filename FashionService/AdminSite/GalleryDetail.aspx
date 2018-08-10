<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GalleryDetail.aspx.cs" Inherits="AdminSite.WebForm5" %>

<%@ Register Src="~/UserControls/ucNewsDetail.ascx" TagPrefix="uc1" TagName="ucNewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucNewsDetail runat="server" ID="ucNewsDetail" HideContent="true" />
</asp:Content>
