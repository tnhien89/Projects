<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Other.aspx.cs" Inherits="AdminSite.Other" %>

<%@ Register Src="~/UserControls/ucOtherInfo.ascx" TagPrefix="uc1" TagName="ucOtherInfo" %>
<%@ Register Src="~/UserControls/ucOtherItems.ascx" TagPrefix="uc1" TagName="ucOtherItems" %>
<%@ register src="~/UserControls/ucSubOtherItems.ascx" tagprefix="uc1" tagname="ucSubOtherItems" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucOtherInfo runat="server" id="ucOtherInfo" />
    <uc1:ucOtherItems runat="server" id="ucOtherItems" />
    <uc1:ucSubOtherItems runat="server" id="ucSubOtherItems" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
