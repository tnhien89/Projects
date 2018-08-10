<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FrontEndSite.Home" %>

<%@ Register Src="~/UserControls/ucHomeProjects.ascx" TagPrefix="uc1" TagName="ucHomeProjects" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucHomeProjects runat="server" ID="ucHomeProjects" />
</asp:Content>
