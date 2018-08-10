<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="AdminSite.Contacts" %>

<%@ Register Src="~/UserControls/ucAbout.ascx" TagPrefix="uc1" TagName="ucAbout" %>
<%@ Register Src="~/UserControls/ucContacts.ascx" TagPrefix="uc1" TagName="ucContacts" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucAbout runat="server" ID="ucAbout" />
    <uc1:ucContacts runat="server" id="ucContacts" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
