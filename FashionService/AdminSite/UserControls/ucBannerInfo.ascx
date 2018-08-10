<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBannerInfo.ascx.cs" Inherits="AdminSite.UserControls.ucBanner" %>
<%@ Register Src="~/UserControls/ucAddSubMenu.ascx" TagPrefix="uc1" TagName="ucAddSubMenu" %>


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
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Tên:</label>
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
                <label class="control-label col-lg-2">Link:</label>
                <div class="col-lg-10">
                    <asp:TextBox ID="tbxLink" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Ảnh minh họa:</label>
                <div class="col-lg-8">
                    <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="form-control form-control-custom" />
                </div>
            </div>
            <div id="formType" runat="server" class="form-group">
                <label class="control-label col-lg-2">Loại:</label>
                <div class="col-lg-6">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control form-control-custom" ClientIDMode="Static"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-2"></div>
                <div class="col-lg-10">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-info-custom" ClientIDMode="Static" OnClick="btnSubmit_Click" />
                </div>
            </div>

        </div>
    </div>
</div>

<div class="modal fade" id="modalAddSubMenu" tabindex="-1" role="dialog" aria-hidden="true">
    <uc1:ucAddSubMenu runat="server" ID="ucAddSubMenu" />
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