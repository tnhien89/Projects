<%@ Page Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="ContactForm.aspx.cs" Inherits="FrontEndSite.EN.ContactForm" %>

<%@ Register Src="~/UserControls/ucContactForm.ascx" TagPrefix="uc1" TagName="ucContactForm" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucContactForm runat="server" id="ucContactForm" />
</asp:Content>
