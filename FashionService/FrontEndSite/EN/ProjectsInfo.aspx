<%@ Page Language="C#" MasterPageFile="~/ShowProjects.Master" AutoEventWireup="true" CodeBehind="ProjectsInfo.aspx.cs" Inherits="FrontEndSite.EN.ProjectsInfo" %>

<%@ Register Src="~/UserControls/ucProjectsInfo.ascx" TagPrefix="uc1" TagName="ucProjectsInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucProjectsInfo runat="server" id="ucProjectsInfo" />
</asp:Content>
