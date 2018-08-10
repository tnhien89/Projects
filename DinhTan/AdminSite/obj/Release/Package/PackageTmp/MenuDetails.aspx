<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MenuDetails.aspx.cs" Inherits="AdminSite.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        h4
        {
            text-align:center;
            font-weight:bold;
            padding:15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="panel-heading panel-heading-custom">aaaaa</div>
    <div class="panel-body panel-body-custom">
        <div class="col-lg-6">
            <h4>Tiếng Việt</h4>
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="tbxNameVN" class="control-label col-lg-3">Tên:</label>
                    <div class="col-lg-7">
                        <asp:TextBox ID="tbxNameVN" runat="server" CssClass="form-control form-control-custom"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="tbxNameVN" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="tbxDesVN" class="control-label col-lg-3">Chú thích:</label>
                    <div class="col-lg-9">
                        <asp:TextBox ID="tbxDesVN" runat="server" CssClass="form-control form-control-custom" TextMode="MultiLine" Rows="5"/>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-9">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-custom" OnClick="btnSubmit_Click" ValidationGroup="Save"/>
                        <input type="button" id="btnCancel" value="Cancel" class="btn btn-danger btn-custom" onclick="__doPostBack('', 'GoBackPage');"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <h4>English</h4>
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="tbxNameEN" class="control-label col-lg-3">Name:</label>
                    <div class="col-lg-7">
                        <asp:TextBox ID="tbxNameEN" runat="server" CssClass="form-control form-control-custom"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="tbxDesEN" class="control-label col-lg-3">Description:</label>
                    <div class="col-lg-9">
                        <asp:TextBox ID="tbxDesEN" runat="server" CssClass="form-control form-control-custom" TextMode="MultiLine" Rows="5"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
