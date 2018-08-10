<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucProjectDetail.ascx.cs" Inherits="FrontEndSite.UserControls.ucProjectDetail" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server"></label>
</div>
<div class="panel-body panel-body-custom">
    <label id="lbError" runat="server" class="contentError" visible="false"></label>
    <label id="lbUpdatedDate" runat="server" class="UpdatedDate"></label>
    <asp:Literal ID="ltrContent" runat="server"/>
</div>