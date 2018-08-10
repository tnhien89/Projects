<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="Recruitment.aspx.cs" Inherits="FrontEndSite.Recruitment" %>

<%@ Register Src="~/UserControls/ucNewsList.ascx" TagPrefix="uc1" TagName="ucNewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label id="lbContentHeader" runat="server" class="col-md-12 content-header">Tuyển Dụng</label>
    <uc1:ucNewsList runat="server" id="ucNewsList" />
</asp:Content>
