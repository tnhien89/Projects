<%@ control language="C#" autoeventwireup="true" codebehind="ucOtherInfo.ascx.cs" inherits="AdminSite.UserControls.ucOtherInfo" %>
<%@ register src="~/UserControls/ucSubOtherInfo.ascx" tagprefix="uc1" tagname="ucSubOtherInfo" %>
<%@ register src="~/UserControls/ucAddGroupOther.ascx" tagprefix="uc1" tagname="ucAddGroupOther" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server">Thông tin banner</label>
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
        <asp:hiddenfield id="hfId" runat="server" visible="false" />
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Tên:</label>
                    <asp:requiredfieldvalidator id="rfvName" runat="server" controltovalidate="tbxNameVN" errormessage="*" cssclass="required" setfocusonerror="true" display="Dynamic" validationgroup="Submit"></asp:requiredfieldvalidator>
                    <div class="col-lg-6">
                        <asp:textbox id="tbxNameVN" runat="server" cssclass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Ghi chú:</label>
                    <div class="col-lg-8">
                        <asp:textbox id="tbxDesVN" runat="server" textmode="MultiLine" rows="3" cssclass="form-control form-control-custom" />
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
                        <asp:textbox id="tbxNameEN" runat="server" cssclass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Description:</label>
                    <div class="col-lg-8">
                        <asp:textbox id="tbxDesEN" runat="server" textmode="MultiLine" rows="3" cssclass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div id="formSubMenu" runat="server" class="form-group">
                <label class="control-label col-lg-2">Nhóm:</label>
                <div class="col-lg-6">
                    <asp:dropdownlist id="ddlSubMenu" runat="server" cssclass="form-control form-control-custom" clientidmode="Static"></asp:dropdownlist>
                </div>
                <div class="col-lg-4">
                    <input type="button" id="btnAddSubMenu" runat="server" class="btn btn-success btn-success-custom" style="margin-left: -10px;" value="+" data-toggle="modal" data-target="#modalAddSubMenu" />
                    <input type="button" id="btnDeleteSubMenu" runat="server" clientidmode="Static" class="btn btn-danger btn-danger-custom" value="x" onclick="startDeleteSubMenu()" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-2"></div>
                <div class="col-lg-10">
                    <asp:button id="btnUpdate" runat="server" text="Update" cssclass="btn btn-info btn-info-custom" OnClick="btnUpdate_Click" />
                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-danger-custom" OnClick="btnCancel_Click" Visible="false"/>--%>
                </div>
            </div>

        </div>
    </div>
</div>

<div class="modal fade" id="modalAddSubMenu" tabindex="-1" role="dialog" aria-hidden="true">
    <%--<uc1:ucsubotherinfo runat="server" id="ucSubOtherInfo" />--%>
    <uc1:ucaddgroupother runat="server" id="ucAddGroupOther" />
</div>

<script type="text/javascript">
    $(function () {
        var len = $("#ddlSubMenu > option").length;
        if (len > 0) {
            $("#btnDeleteSubMenu").prop("disabled", false);
        }
        else {
            $("#btnDeleteSubMenu").prop("disabled", true);
        }
    });

    $(document).on("click", ".btn-success-custom", function () {
        debugger;
        var parentId = $(this).data("id");
        $(".modal-body #hfParentId").val(parentId);
    });

    $('#FileUploadImage').on('change', function (e) {
        e.preventDefault();
        var formData = new FormData();
        formData.append('file', e.target.files[0]);

        //$.ajax({
        //    type: 'POST',
        //    url: 'WebHandler/UploadFile2.ashx',
        //    data: formData,
        //    contentType: false,
        //    processData: false,
        //    cache: false,
        //    success: function (data) {
        //        if (!data || data.Code < 0 || !data.Data) {
        //            return;
        //        }

        //        $('#hfImage').val(data.Data);
        //    },
        //    error: function (xhr, status, error) {
        //        alert(error);
        //    }
        //});

        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgImage').attr('src', e.target.result);
        }
        reader.readAsDataURL(e.target.files[0]);
    });

    function startDeleteSubMenu() {
        var subId = $("#ddlSubMenu").val();
        if (subId <= 0) {
            return;
        }

        if (!confirm("Are you sure you want to delete?")) {
            return;
        }

        $.ajax({
            url: "WebHandler/DeleteSubMenuHandler.ashx",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: { 'Id': subId },
            success: function (result) {
                if (result.Code < 0) {
                    alert(result.Message);
                }
                else {
                    location.reload();
                }
            },
            error: function (xhr, status, error) {
                alert("Error code: " + xhr.status + " - " + error);
            }
        });
    }
</script>
