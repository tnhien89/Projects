<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMainRight.ascx.cs" Inherits="FrontEndSite.UserControls.ucMainRight" %>
<%@ Register Src="~/UserControls/ucMainRightItem.ascx" TagPrefix="uc1" TagName="ucMainRightItem" %>
<%@ Register Src="~/UserControls/ucOnlineVisitors.ascx" TagPrefix="uc1" TagName="ucOnlineVisitors" %>



<asp:ListView ID="lvMainRight" runat="server" OnItemDataBound="lvMainRight_ItemDataBound">
    <ItemTemplate>
        <uc1:ucMainRightItem runat="server" id="ucMainRightItem" />
    </ItemTemplate>
</asp:ListView>

<uc1:ucOnlineVisitors runat="server" id="ucOnlineVisitors" />
