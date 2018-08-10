<%@ Page Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="FrontEndSite.EN.About" %>

<%@ Register Src="~/UserControls/ucAboutMain.ascx" TagPrefix="uc1" TagName="ucAboutMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucAboutMain runat="server" id="ucAboutMain" />
</asp:Content>
