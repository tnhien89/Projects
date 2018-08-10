<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Introductions.aspx.cs" Inherits="AdminSite.WebForm2" %>

<%@ Register Src="~/UserControls/ucMenuItems.ascx" TagPrefix="uc1" TagName="ucMenuItems" %>
<%@ Register Src="~/UserControls/ucNewsItems.ascx" TagPrefix="uc1" TagName="ucNewsItems" %>
<%@ Register Src="~/UserControls/ucMenuDetail.ascx" TagPrefix="uc1" TagName="ucMenuDetail" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucMenuDetail runat="server" id="ucMenuDetail" />

    <uc1:ucNewsItems runat="server" id="ucNewsItems" />

    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
