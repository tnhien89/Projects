<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucOtherInfo.ascx.cs" Inherits="AdminSite.UserControls.ucOtherInfo" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server">Thông tin banner</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false"/>
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
        <asp:HiddenField ID="hfId" runat="server" Visible="false"/>
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Tên:</label>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="tbxNameVN" ErrorMessage="*" CssClass="required" SetFocusOnError="true" Display="Dynamic" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tbxNameVN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Ghi chú:</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="tbxDesVN" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div id="tabEN" class="tab-pane fade">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Name:</label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tbxNameEN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Description:</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="tbxDesEN" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-lg-2"></div>
                <div class="col-lg-10">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-info btn-info-custom" ClientIDMode="Static" OnClick="btnSubmit_Click" ValidationGroup="Submit"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-danger-custom" OnClick="btnCancel_Click" Visible="false"/>
                </div>
            </div>

        </div>
    </div>
</div>