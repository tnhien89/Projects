<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAddGroupOther.ascx.cs" Inherits="AdminSite.UserControls.ucAddGroupOther" %>

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title">Thêm nhóm</h4>
        </div>
        <div class="modal-body">
            <div class="col-lg-12">
                <label id="lbError" class="errMsg" hidden="hidden"></label>
            </div>

            <input type="hidden" id="hfParentId" />

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
                            <label class="control-label col-lg-2">Tên:</label>
                            <asp:requiredfieldvalidator id="rfvNameVN" runat="server" errormessage="*" controltovalidate="tbxNameVN" setfocusonerror="true" cssclass="required" validationgroup="Submit"></asp:requiredfieldvalidator>
                            <div class="col-lg-6">
                                <asp:textbox id="tbxNameVN" runat="server" cssclass="form-control form-control-custom" clientidmode="Static" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-2">Ghi chú:</label>
                            <div class="col-lg-10">
                                <asp:textbox id="tbxDesVN" runat="server" textmode="MultiLine" rows="3" cssclass="form-control form-control-custom" clientidmode="Static" />
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
                                <asp:textbox id="tbxNameEN" runat="server" cssclass="form-control form-control-custom" clientidmode="Static" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-2">Description:</label>
                            <div class="col-lg-10">
                                <asp:textbox id="tbxDesEN" runat="server" textmode="MultiLine" rows="3" cssclass="form-control form-control-custom" clientidmode="Static" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-lg-2">Ảnh minh họa:</label>
                        <div class="col-lg-5">
                            <div class="detail-image">
                                <asp:image id="imgImage" runat="server" clientidmode="Static" />
                            </div>
                            <asp:fileupload id="FileUploadImage" runat="server" cssclass="form-control form-control-custom" clientidmode="Static" />
                            <asp:regularexpressionvalidator id="revImage" runat="server" errormessage="Image is invalid." controltovalidate="FileUploadImage" validationexpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" cssclass="regexInvalid" setfocusonerror="true" validationgroup="Save"></asp:regularexpressionvalidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-10">
                            <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-info-custom" ClientIDMode="Static" ValidationGroup="Submit"/>--%>
                            <input type="button" id="btnSubmit" value="Submit" runat="server" class="btn btn-info btn-info-custom" onclick="checkSubmit()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript" src="../Scripts/CustomValidate.js"></script>
<script type="text/javascript">
    $('#modalAddSubMenu').on('shown.bs.modal', function () {
        $('#imgImage').attr('src', '');
        $('#FileUploadImage').val('');
    });

    var processing = false;
    var imagePath = '';

    $('#FileUploadImage').on('change', function (e) {
        processing = true;
        e.preventDefault();
        var formData = new FormData();
        formData.append('file', e.target.files[0]);

        $.ajax({
            type: 'POST',
            url: 'WebHandler/UploadFile2.ashx',
            data: formData,
            contentType: false,
            processData: false,
            cache: false,
            success: function (data) {
                if (!data || data.Code < 0 || !data.Data) {
                    return;
                }

                imagePath = data.Data;
                processing = false;
            },
            error: function (xhr, status, error) {
                alert(error);
                processing = false;
            }
        });

        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgImage').attr('src', e.target.result);
        }
        reader.readAsDataURL(e.target.files[0]);
    });

    function checkSubmit() {
        if (checkPageValid() == true) {
            var menuObj = {
                Name_VN: $("#tbxNameVN").val(),
                Description_VN: $("#tbxDesVN").val(),
                Name_EN: $("#tbxNameEN").val(),
                Description_EN: $("#tbxDesEN").val(),
                Image: imagePath,
                ParentId: $("#hfParentId").val()
            };

            while (processing) {
                setTimeout(null, 100);
            }

            $.ajax({
                url: "WebHandler/AddGroupOtherHandler.ashx",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(menuObj),
                success: function (result) {
                    debugger;
                    if (result !== undefined) {
                        if (result.Code < 0) {
                            // error
                            alert(result.Message);
                        }
                        else {
                            //$("#ddlSubMenu").append(
                            //    $("<option></option>").val(result.Id).html(result.Name));

                            location.reload();
                        }
                    }
                },
                error: function (xhr, status, error) {
                    debugger;
                    alert("Error code: " + xhr.status + " - " + error);
                }
            });
        }

        return false;
    }
</script>