<%@ Page Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="ContactInfo.aspx.cs" Inherits="FrontEndSite.EN.ContactInfo" %>

<%@ Register Src="~/UserControls/ucContactInfo.ascx" TagPrefix="uc1" TagName="ucContactInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucContactInfo runat="server" ID="ucContactInfo" />
</asp:Content>
