﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrontEnd.Master" AutoEventWireup="true" CodeBehind="ServiceInfo.aspx.cs" Inherits="FrontEndSite.EN.ServiceInfo" %>

<%@ Register Src="~/UserControls/ucNewsDetail.ascx" TagPrefix="uc1" TagName="ucNewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucNewsDetail runat="server" ID="ucNewsDetail" />
</asp:Content>
