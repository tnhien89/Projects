<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewsMain.ascx.cs" Inherits="FrontEndSite.UserControls.ucNewsMain" %>
<%@ Register Src="~/UserControls/ucNewsList.ascx" TagPrefix="uc1" TagName="ucNewsList" %>
<%@ Register Src="~/UserControls/ucNewsDetail.ascx" TagPrefix="uc1" TagName="ucNewsDetail" %>



<div class="col-md-12">
    <label id="lbHeader" runat="server" class="leftMenuHeader"></label>
</div>
<div class="col-md-12">
    <label id="lbError" runat="server" class="errMsg" visible="false"></label>
</div>
<div class="col-md-3 menuLeft">
    <uc1:ucNewsList runat="server" ID="ucNewsList" />
</div>
<div class="col-md-9 news-Detail">
    <uc1:ucNewsDetail runat="server" ID="ucNewsDetail" />
</div>
