<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucContactInfo.ascx.cs" Inherits="FrontEndSite.UserControls.ucContactInfo" %>
<%@ Register Src="~/UserControls/ucGoogleMaps.ascx" TagPrefix="uc1" TagName="ucGoogleMaps" %>


<label id="lbContentHeader" runat="server" class="col-md-12 content-header">Thông Tin Liên Hệ</label>

<div class="col-md-12 contact-info">
    <label id="lbName" runat="server" class="contact-info-header"></label>

    <div class="col-md-2 contact-info-left">
        <label id="lbAddress" runat="server">Địa chỉ:</label>
    </div>
    <div class="col-md-10 contact-info-right">
        <label id="lbAddressInfo" runat="server">aaggagdagag</label>
    </div>
    <div class="col-md-2 contact-info-left">
        <label id="lbPhone" runat="server">Điện thoại:</label>
    </div>
    <div class="col-md-10 contact-info-right">
        <label id="lbPhoneInfo" runat="server">asgasgagag</label>
    </div>
    <div class="col-md-2 contact-info-left">
        <label id="lbFax" runat="server">Fax:</label>
    </div>
    <div class="col-md-10 contact-info-right">
        <label id="lbFaxInfo" runat="server">agagagag</label>
    </div>
    <div class="col-md-2 contact-info-left">
        <label id="lbEmail" runat="server">Email:</label>
    </div>
    <div class="col-md-10 contact-info-right">
        <label id="lbEmailInfo" runat="server">asdgasgg</label>
    </div>
</div>

<label id="lbGoogleMapsHeader" runat="server" class="col-md-12 content-header">Bản đồ đường đi</label>
<div class="col-md-12 contact-info">
    <uc1:ucGoogleMaps runat="server" id="ucGoogleMaps" />
</div>
