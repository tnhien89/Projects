<%@ Page Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="Introductions.aspx.cs" Inherits="FrontEndSite.Introductions" %>

<%@ Register Src="~/UserControls/ucNewsMain.ascx" TagPrefix="uc1" TagName="ucNewsMain" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucNewsMain runat="server" id="ucNewsMain" />
</asp:Content>
