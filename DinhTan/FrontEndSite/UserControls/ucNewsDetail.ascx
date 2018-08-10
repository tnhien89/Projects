<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewsDetail.ascx.cs" Inherits="FrontEndSite.UserControls.ucNewsDetail" %>

<div class="news-detail">
    <label id="lbError" runat="server" class="contentError" visible="false"></label>
    <div class="news-detail-date">
        <label id="lbUpdatedDate" runat="server"></label>
    </div>
    <div class="news-detail-title">
        <label id="lbTitle" runat="server"></label>
    </div>
    <div class="new-detail-content">
        <asp:Literal ID="ltrContent" runat="server" />
    </div>
</div>