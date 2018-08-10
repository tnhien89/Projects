<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ContactDetail.aspx.cs" Inherits="AdminSite.ContactDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="panel-heading panel-heading-custom">
        <label id="lbHeader" runat="server"></label>
    </div>
    <div class="panel-body panel-body-custom">
        <label id="lbError" runat="server" class="errMsg" visible="false"></label>
        <asp:Literal ID="ltrContent" runat="server"/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
