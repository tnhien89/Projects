<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucOnlineVisitors.ascx.cs" Inherits="FrontEndSite.UserControls.ucOnlineVisitors" %>

<style>
    .visitors-info
    {
        list-style-type: none;
        margin-left: -35px;
        display: block;
        position: relative;
        margin-top: 10px;
    }

    .visitors-info li
    {
        display: block;
        padding-left: 0;
    }

    .visitors-info li:first-child
    {
        margin-top: 50px;
    }

    .visitors-info-left
    {
        text-align: left;
        font-weight: normal;
        float: left;
    }

    .visitors-info-right
    {
        text-align: right;
        font-weight: bold;
        display: block;
    }
</style>

<div id="content-right">
    <div class="content-right-title">
        <asp:Label ID="lbTitle" runat="server">Online Visitors</asp:Label>
    </div>
    <div class="content-right-content">
        <ul class="visitors-info">
            <li>
                <label id="lbOnlineVisitors" runat="server" class="visitors-info-left">Đang trực tuyến</label>
                <label id="lbOnlineVisitorsValue" runat="server" class="visitors-info-right">1</label>
            </li>
            <li>
                <label id="lbVisitorsInDay" runat="server" class="visitors-info-left">Hôm nay</label>
                <label id="lbVisitorsInDayValue" runat="server" class="visitors-info-right">1</label>
            </li>
            <li>
                <label id="lbVisitorsInMonth" runat="server" class="visitors-info-left">Trong tháng</label>
                <label id="lbVisitorsInMonthValue" runat="server" class="visitors-info-right">1</label>
            </li>
            <li>
                <label id="lbVisitorsInYear" runat="server" class="visitors-info-left">Trong năm</label>
                <label id="lbVisitorsInYearValue" runat="server" class="visitors-info-right">1</label>
            </li>
        </ul>
    </div>
</div>