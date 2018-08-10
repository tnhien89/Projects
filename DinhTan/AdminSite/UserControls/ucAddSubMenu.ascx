<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAddSubMenu.ascx.cs" Inherits="AdminSite.UserControls.ucAddSubMenu" %>

<div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Thêm danh mục con</h4>
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
                                <asp:RequiredFieldValidator ID="rfvNameVN" runat="server" ErrorMessage="*" ControlToValidate="tbxNameVN" SetFocusOnError="true" CssClass="required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="tbxNameVN" runat="server" CssClass="form-control form-control-custom" ClientIDMode="Static"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-2">Ghi chú:</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="tbxDesVN" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-custom" ClientIDMode="Static"/>
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
                                    <asp:TextBox ID="tbxNameEN" runat="server" CssClass="form-control form-control-custom" ClientIDMode="Static"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-2">Description:</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="tbxDesEN" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-custom" ClientIDMode="Static"/>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-10">
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info btn-info-custom" ClientIDMode="Static" ValidationGroup="Submit"/>--%>
                                <input type="button" id="btnSubmit" value="Submit" runat="server" class="btn btn-info btn-info-custom" onclick="checkSubmit()"/>
                            </div>
                        </div>
            
                    </div>
                </div> 
            </div>
        </div>
</div>

<script type="text/javascript" src="../Scripts/CustomValidate.js"></script>
<script type="text/javascript">
    function checkSubmit()
    {
        if (checkPageValid() == true) {
            var menuObj = {
                Name_VN: $("#tbxNameVN").val(),
                Description_VN: $("#tbxDesVN").val(),
                Name_EN: $("#tbxNameEN").val(),
                Description_EN: $("#tbxDesEN").val(),
                ParentId: $("#hfParentId").val()
            };

            $.ajax({
                url: "WebHandler/AddSubMenuHandler.ashx",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(menuObj),
                success: function (result) {
                    debugger;
                    if (result !== undefined)
                    {
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