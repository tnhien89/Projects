<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewsDetail.ascx.cs" Inherits="AdminSite.UserControls.ucNewsDetail" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<script type="text/javascript" src="../Scripts/ckeditor/ckeditor.js">
</script>


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
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Tiêu đề:</label>
                    <asp:RequiredFieldValidator ID="rfvNameVN" runat="server" ErrorMessage="*" ControlToValidate="tbxNameVN" CssClass="required" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <div class="col-lg-5">
                        <asp:TextBox ID="tbxNameVN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                
                <div id="groupVacancyVN" class="form-group" runat="server" visible="false">
                    <label class="control-label col-lg-2">Vị trí:</label>
                    <asp:RequiredFieldValidator ID="rfvVacancy" runat="server" ControlToValidate="ddlVacancy" ErrorMessage="*" CssClass="required" SetFocusOnError="true" ValidationGroup="Save" Enabled="false"></asp:RequiredFieldValidator>
                    <div class="col-lg-4">
                        <asp:DropDownList ID="ddlVacancy" runat="server" CssClass="form-control form-control-custom" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Ghi chú:</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="tbxDesVN" runat="server" CssClass="form-control form-control-custom" TextMode="MultiLine" Rows="5" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Nội dung:</label>
                    <label id="lbRequiredContentVN" class="required" hidden="hidden">*</label>
                    <div class="col-lg-10">
                        <CKEditor:CKEditorControl ID="txaContentVN" runat="server" BasePath="/Scripts/ckeditor/" ClientIDMode="Static"></CKEditor:CKEditorControl>
                    </div>
                </div>
            </div>
        </div>

        <div id="tabEN" class="tab-pane fade">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Title:</label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="tbxNameEN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                <div id="groupVacancyEN" class="form-group" runat="server" visible="false">
                    <label class="control-label col-lg-2">Vacancy:</label>
                    <div class="col-lg-4">
                        <asp:DropDownList ID="ddlVacancyEN" runat="server" CssClass="form-control form-control-custom" Enabled="false" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Description:</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="tbxDesEN" runat="server" CssClass="form-control form-control-custom" TextMode="MultiLine" Rows="5" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Content:</label>
                    <div class="col-lg-10">
                        <CKEditor:CKEditorControl ID="txaContentEN" runat="server" BasePath="/Scripts/ckeditor/" ClientIDMode="Static"></CKEditor:CKEditorControl>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="form-group">
                    <label class="control-label col-lg-2">Ảnh minh họa:</label>
                    <div class="col-lg-5">
                        <div class="detail-image">
                            <asp:Image ID="imgImage" runat="server" Visible="false"/>
                        </div>
                        <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control form-control-custom" />
                        <asp:RegularExpressionValidator ID="revImage" runat="server" ErrorMessage="Image is invalid." ControlToValidate="FileUploadImage" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" CssClass="regexInvalid" SetFocusOnError="true" ValidationGroup="Save"></asp:RegularExpressionValidator>
                    </div>
                </div>
        <div class="form-group">
        <div class="col-lg-2"></div>
        <div class="col-lg-10">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-info-custom" OnClick="btnSubmit_Click" ValidationGroup="Save" ClientIDMode="Static" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-danger-custom" OnClick="btnCancel_Click" />
        </div>
    </div>
    </div>
    
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $("#ddlVacancy").change(function () {
            var value = $(this).val();
            $("#ddlVacancyEN").val(value);
        });

        $("#btnSubmit").click(function () {
            debugger;
            CKEDITOR.instances['<%= txaContentVN.ClientID %>'].updateElement();
            var val = CKEDITOR.instances['<%= txaContentVN.ClientID %>'].getData();
            if (val == '') {
                $("#lbRequiredContentVN").prop("hidden", false);
                return false;
            }
            else {
                $("#lbRequiredContentVN").prop("hidden", true);
            }
        });
    });
</script>
