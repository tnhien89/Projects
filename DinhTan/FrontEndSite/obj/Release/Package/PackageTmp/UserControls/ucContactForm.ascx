<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucContactForm.ascx.cs" Inherits="FrontEndSite.UserControls.ucContactForm" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<script type="text/javascript" src="../Scripts/ckeditor/ckeditor.js"></script>

<style type="text/css">
    .btn-info-custom {
        background: #388e3c;
    }
</style>

<label id="lbContentHeader" runat="server" class="col-md-12 content-header">Form Liên Hệ</label>

<div class="col-md-12 contact-form">
    <label id="lbError" runat="server" class="errMsg" visible="false"></label>
    <div class="form-horizontal">
        <div class="form-group">
            <label id="lbName" runat="server" class="control-label col-md-3">Tên:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbxName" CssClass="required" ClientIDMode="Static" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
            <div class="col-md-6">
                <asp:TextBox ID="tbxName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label id="lbAddress" runat="server" class="control-label col-md-3">Địa chỉ:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbxAddress" SetFocusOnError="true" CssClass="required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
            <div class="col-md-7">
                <asp:TextBox ID="tbxAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ClientIDMode="Static"/>
            </div>
        </div>
        <div class="form-group">
            <label id="lbPhone" runat="server" class="control-label col-md-3">Điện thoại:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="tbxPhone" CssClass="required" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
            <div class="col-md-4">
                <asp:TextBox ID="tbxPhone" runat="server" CssClass="form-control" ClientIDMode="Static"/>
            </div>
        </div>
        <div class="form-group">
            <label id="lbEmail" runat="server" class="control-label col-md-3">Email:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="tbxEmail" CssClass="required" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
            <div class="col-md-6">
                <asp:TextBox ID="tbxEmail" runat="server" CssClass="form-control" ClientIDMode="Static"/>
            </div>
        </div>
        <div class="form-group">
            <label id="lbSubject" runat="server" class="control-label col-md-3">Tiêu đề:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="tbxSubject" CssClass="required" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
            <div class="col-md-7">
                <asp:TextBox ID="tbxSubject" runat="server" CssClass="form-control" ClientIDMode="Static"/>
            </div>
        </div>
        <div class="form-group">
            <label id="lbContent" runat="server" class="control-label col-md-3">Nội dung:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="ckeContent" SetFocusOnError="true" CssClass="required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
            <div class="col-md-9">
                <asp:TextBox ID="ckeContent" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-3"></div>
            <div class="col-md-9">
                <asp:Button ID="btnSubmit" runat="server" Text="Gửi" CssClass="btn btn-success" ValidationGroup="Submit" OnClick="btnSubmit_Click"/>
                <asp:Button ID="btnClear" runat="server" Text="Soạn lại" CssClass="btn btn-danger btn-danger-custom" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</div>