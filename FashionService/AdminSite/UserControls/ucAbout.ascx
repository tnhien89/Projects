<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAbout.ascx.cs" Inherits="AdminSite.UserControls.ucAbout" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server">Thông Tin</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false"></label>
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
        <asp:HiddenField ID="hfId" runat="server" />
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-3">Tên doanh nghiệp:</label>
                    <asp:RequiredFieldValidator ID="rfvNameVN" runat="server" ErrorMessage="*" ControlToValidate="tbxNameVN" CssClass="required" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <div class="col-lg-7">
                        <asp:TextBox ID="tbxNameVN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-lg-3">Địa chỉ:</label>
                    <div class="col-lg-7">
                        <asp:TextBox ID="tbxAddress" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-custom"></asp:TextBox>
                    </div>
                </div>

            </div>
        </div>

        <div id="tabEN" class="tab-pane fade">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-3">Company Name:</label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="tbxNameEN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-3">Address:</label>
                    <div class="col-lg-7">
                        <asp:TextBox ID="tbxAddressEN" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-lg-3">Logo:</label>
            <div class="col-lg-6">
                <div class="detail-image">
                    <asp:Image ID="imgImage" data-id="logo" runat="server" ClientIDMode="Static"/>
                </div>
                <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control form-control-custom" ClientIDMode="Static" />
                <asp:RegularExpressionValidator ID="revImage" runat="server" ErrorMessage="Image is invalid." ControlToValidate="FileUploadImage" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg]|\.[Ss][Vv][Gg])" CssClass="regexInvalid" SetFocusOnError="true" ValidationGroup="Save"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-lg-3">Điện thoại:</label>
            <div class="col-lg-5">
                <asp:TextBox ID="tbxPhone" runat="server" CssClass="form-control form-control-custom" />
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-lg-3">Fax:</label>
            <div class="col-lg-5">
                <asp:TextBox ID="tbxFax" runat="server" CssClass="form-control from-control-custom"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-lg-3">Email:</label>
            <div class="col-lg-5">
                <asp:TextBox ID="tbxEmail" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-lg-3">Web Site:</label>
            <div class="col-lg-5">
                <asp:TextBox ID="tbxWebSite" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-lg-3">Google maps:</label>
            <div class="col-lg-7">
                <asp:TextBox ID="tbxGoogleCode" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control form-control-custom" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-lg-3"></div>
            <div class="col-lg-9">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-info-custom" OnClick="btnSubmit_Click" />

            </div>
        </div>
    </div>

    <%--<div class="form-group">
        <div class="col-lg-2"></div>
        <div class="col-lg-10">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-info-custom" OnClick="btnSubmit_Click" ValidationGroup="Save" ClientIDMode="Static"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-danger-custom" OnClick="btnCancel_Click" />
        </div>
    </div>--%>
</div>


<script type="text/javascript">
    $('#FileUploadImage').on('change', function (e) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgImage').attr('src', e.target.result);
            $('#imgLogo').attr('src', e.target.result);
            $('.detail-image').show();
        }
        reader.readAsDataURL(e.target.files[0]);
    });
</script>

