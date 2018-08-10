<%@ Page Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="FrontEndSite.Projects" %>

<%@ Register Src="~/UserControls/ucProjectsList.ascx" TagPrefix="uc1" TagName="ucProjectsList" %>
<%@ Register Src="~/UserControls/ucProjectDetail.ascx" TagPrefix="uc1" TagName="ucProjectDetail" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label id="lbContentHeader" runat="server" class="col-md-12 content-header">Dự Án</label>

    <%--<uc1:ucProjectDetail runat="server" id="ucProjectDetail" />--%>
    <uc1:ucProjectsList runat="server" id="ucProjectsList" />
</asp:Content>