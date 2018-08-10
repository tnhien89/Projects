<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProjectDetail.aspx.cs" Inherits="AdminSite.ProjectDetail" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../Scripts/ckeditor/ckeditor.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="panel-heading panel-heading-custom">
        <label id="lbHeader" runat="server">Thông Tin Dự Án</label>
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
                    <div class="form-group">
                        <label class="control-label col-lg-2">Ghi chú:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="tbxDesVN" runat="server" CssClass="form-control form-control-custom" TextMode="MultiLine" Rows="5" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-2">Địa chỉ:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="tbxAddress_VN" runat="server" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-2">Nội dung:</label>
                        <label id="lbRequiredContentVN" class="required" hidden="hidden">*</label>
                        <div class="col-lg-10">
                            <CKEditor:CKEditorControl ID="txaContentVN" runat="server" BasePath="Scripts/ckeditor/"></CKEditor:CKEditorControl>
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
                    <div class="form-group">
                        <label class="control-label col-lg-2">Description:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="tbxDesEN" runat="server" CssClass="form-control form-control-custom" TextMode="MultiLine" Rows="5" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-2">Address:</label>

                        <div class="col-lg-8">
                            <asp:TextBox ID="tbxAddress_EN" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-2">Content:</label>
                        <div class="col-lg-10">
                            <CKEditor:CKEditorControl ID="txaContentEN" runat="server" BasePath="Scripts/ckeditor/" ClientIDMode="Static"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-lg-2">Ngày khởi công</label>
                <div class="col-lg-4">
                    <asp:TextBox ID="tbxStartDate" runat="server" CssClass="form-control" placeholder="mm/dd/yyyy" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Ngày hoàn thành:</label>
                <div class="col-lg-4">
                    <asp:TextBox ID="tbxEndDate" runat="server" CssClass="form-control" placeholder="mm/dd/yyyy" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
            <%--<div class="form-group">
                <label class="control-label col-lg-2">Liên kết:</label>
                <div class="col-lg-6">
                    <asp:TextBox ID="tbxRedirectLink" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-lg-4 checkbox">
                    <asp:CheckBox ID="chkChooseCategory" runat="server" ClientIDMode="Static" />
                    <img src="Images/Help Circle.png" data-toggle="tooltip" data-placement="right" title="Nếu chọn chức năng này, dự án hiển thị sẽ được liên kết đến một mục dự án được chọn." />
                </div>
            </div>--%>

            <div id="groupImages" class="form-horizontal">
                <asp:HiddenField ID="hfErrors" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfListImage" runat="server" ClientIDMode="Static" />

                <div class="form-group">
                    <label class="control-label col-lg-2">Ảnh minh họa:</label>
                    <div id="project-detail-images" class="col-lg-10 project-detail-images">
                        <asp:ListView ID="lvProjectImages" runat="server" OnItemDataBound="lvProjectImages_ItemDataBound">
                            <ItemTemplate>
                                <div class="col-lg-3 project-detail-images-item">
                                    <asp:Image ID="itemImage" runat="server" />
                                    <div class="project-detail-images-button">
                                        <asp:HiddenField ID="hfImageLink" runat="server" ClientIDMode="Static" />
                                        <input type="button" value="X" class="btn btn-danger" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-lg-2">Upload ảnh:</label>

                    <div class="col-lg-6">
                        <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control form-control-custom file-upload-new" ClientIDMode="Static" />
                        <div class="regexInvalid" style="display: none;">
                            <span>Image is invalid.</span>
                        </div>
                        <%--<asp:RegularExpressionValidator ID="revImage" runat="server" ErrorMessage="Image is invalid." ControlToValidate="FileUploadImage" ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" CssClass="regexInvalid" SetFocusOnError="true" ValidationGroup="Save"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-lg-4">
                        <input type="button" id="btnAddImage" value="+" class="btn btn-success btn-success-custom btn-add-image" />
                    </div>
                </div>
            </div>
            <div class="form-horizontal">
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

            $(function () {
                $('#tbxStartDate').datepicker({
                    format: "mm/dd/yyyy"
                });

                $('#tbxEndDate').datepicker({
                    format: "mm/dd/yyyy"
                });
            });

            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();

                $("#btnSubmit").click(function () {

                    //----
                    var errors = $("#hfErrors").val();
                    if (errors !== undefined && errors !== '' && parseInt(errors) > 0) {
                        return false;
                    }
                    //---
                    var listImage = $("#hfListImage").val();
                    var formFiles = new FormData();
                    var i = 0;
                    //---
                    $("#groupImages .file-upload-new").each(function () {

                        formFiles.append('file' + i, this.files[0]);
                        i++;
                    });

                    //var data = {
                    //    'Files': formFiles,
                    //    'Dir': 'ImageProjectsDir'
                    //};

                    $.ajax({
                        url: 'WebHandler/UploadMultipleFiles.ashx',
                        type: 'POST',
                        async: false,
                        data: formFiles,
                        processData: false,
                        contentType: false,
                        success: function (result) {
                            if (result === undefined) {
                                return;
                            }

                            if (result.Code < 0) {
                                alert(result.Message);
                                return;
                            }

                            if (listImage === '') {
                                $("#hfListImage").val(result.Data);
                            }
                            else if (result.Data !== '') {
                                listImage += '|' + result.Data;

                                $("#hfListImage").val(listImage);
                            }

                        },
                        error: function (rhx, opt, err) {
                            alert(err);
                        }
                    });

                    if ($('#chkChooseCategory').is(':checked')) {
                        $("#lbRequiredContentVN").prop("hidden", true);
                        return;
                    }

                    CKEDITOR.instances['<%= txaContentVN.ClientID %>'].updateElement();
                    var val = CKEDITOR.instances['<%= txaContentVN.ClientID %>'].getData();

                    if (val == '') {
                        $("#lbRequiredContentVN").prop("hidden", false);
                        return false;
                    }

                    $("#lbRequiredContentVN").prop("hidden", true);
                });
            });

            $("#project-detail-images").on('click', ':button', function () {
                if (!confirm('Are you sure you want to delete?')) {
                    return;
                }

                var imgUrl = $(this).parent().children('#hfImageLink').val();
                var listImage = $('#hfListImage').val();
                //---
                listImage = listImage.replace(imgUrl, '');
                listImage = listImage.replace('||', '|');
                if (listImage[0] === '|') {
                    listImage = listImage.substring(1);
                }
                //--
                $('#hfListImage').val(listImage);

                $(this).parent().parent().remove();
            });

            $("#groupImages").on("click", ".btn-add-image, .btn-remove-image", function () {

                if ($(this).val() === '-') {
                    var idx = $(this).index();
                    $("#groupImages .form-group")[idx + 1].remove();
                    //---
                    if ($("#groupImages .form-group").length === 1) {
                        $("#btnAddImage").show();
                    }

                    return;
                }


                var newFileUpload = '<div class="form-group">' +
                    '<div class="col-lg-2"></div>' +
                    '<div class="col-lg-6">' +
                    '<input class="form-control form-control-custom file-upload-new" type="file" />';

                var rex = '<div class="regexInvalid" style="display: none;">' +
                    '<span>Image is invalid.</span>' +
                    '</div>' +
                    '</div>';

                var newButtons = '<div class="col-lg-4">' +
                    '<input type="button" value="-" class="btn btn-danger btn-danger-custom btn-remove-image" />' +
                    '<input type="button" value="+" class="btn btn-success btn-success-custom btn-add-image" />' +
                    '</div>' +
                    '</div>';
                //---
                $("#groupImages").append(newFileUpload + rex + newButtons);

                $(this).hide();
            });

            $("#groupImages").on("change", ".file-upload-new", function (index, item) {
                var pattern = /.*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])/;

                var fileName = $(this).val();

                var errors = $("#hfErrors").val();
                var $lbErr = $(this).parent().children('.regexInvalid');

                if (!fileName.match(pattern)) {

                    if ($lbErr.is(':visible')) {
                        return;
                    }

                    if (errors === undefined || errors === '') {
                        errors = 1;
                    }
                    else {
                        errors = parseInt(errors) + 1;
                    }

                    //---
                    //$("#groupImages .regexInvalid").eq(idx + 1).show();
                    $lbErr.show();
                }
                else {

                    if (!$lbErr.is(':visible')) {
                        return;
                    }

                    $lbErr.hide();

                    if (errors !== undefined && errors !== '' && parseInt(errors) > 0) {
                        errors = parseInt(errors) - 1;
                    }
                }
                //----
                $("#hfErrors").val(errors);
            });

            $('#chkChooseCategory').click(function () {
                if ($(this).is(':checked')) {
                    CKEDITOR.instances['<%= txaContentVN.ClientID %>'].setReadOnly(true);
                    $("#btnAddImage").hide();
                    //$('#rfvRedirectLink').prop('disabled', false);
                    //ValidatorEnable($('#rfvRedirectLink'), true);
                }
                else {
                    CKEDITOR.instances['<%= txaContentVN.ClientID %>'].setReadOnly(false);
                    $("#btnAddImage").show();
                    //$('#rfvRedirectLink').prop('disabled', true);
                    //ValidatorEnable($('#rfvRedirectLink'), false);
                }
            });
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

