<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUserInfo.ascx.cs" Inherits="AdminSite.UserControls.ucUserInfo" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server">Thông tin User</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false" />
    </div>
    <ul class="nav nav-tabs nav-tabs-custom">
        <li class="active">
            <a data-toggle="tab" href="#tabVN">Tiếng Việt</a>
        </li>
        <li>
            <a data-toggle="tab" href="#tabEN">English</a>
        </li>
    </ul>

    <div class="tab-content">
        <asp:HiddenField ID="hfId" runat="server" Visible="false" />
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Họ:</label>
                    <div class="col-lg-4">
                        <asp:TextBox ID="tbxFirstNameVN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                    <label class="control-label col-lg-2">Tên:</label>
                    <div class="col-lg-4">
                        <asp:TextBox ID="tbxLastNameVN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div id="tabEN" class="tab-pane fade">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">First Name:</label>
                    <div class="col-lg-4">
                        <asp:TextBox ID="tbxFirstNameEN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                    <label class="control-label col-lg-2">Last Name:</label>
                    <div class="col-lg-4">
                        <asp:TextBox ID="tbxLastNameEN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-lg-2">Username:</label>
                <div class="col-lg-5">
                    <asp:TextBox ID="tbxUsername" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Password:</label>
                <div class="col-lg-5">
                    <asp:TextBox ID="tbxPassword" runat="server" CssClass="form-control form-control-custom" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Email:</label>
                <div class="col-lg-5">
                    <asp:TextBox ID="tbxEmail" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Phone:</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="tbxPhone" runat="server" CssClass="form-control form-control-custom" TextMode="Phone" MaxLength="11" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Ngày sinh:</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control form-control-custom" ClientIDMode="static" TextMode="Date" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Giới tính:</label>
                <div class="col-lg-2">
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control form-control-custom">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <div class="col-lg-2"></div>
                <div class="col-lg-10">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-info btn-info-custom" ClientIDMode="Static" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-danger-custom" OnClick="btnCancel_Click" Visible="false" />
                </div>
            </div>

        </div>
    </div>
</div>

<script type="text/javascript"></script>
