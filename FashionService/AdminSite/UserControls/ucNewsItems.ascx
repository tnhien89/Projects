<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewsItems.ascx.cs" Inherits="AdminSite.UserControls.ucNewsItems" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server">Danh Sách Tin</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info btn-info-custom" OnClick="btnAdd_Click" />
        <input type="button" id="btnDelete" value="Delete" class="btn btn-danger btn-danger-custom" />
    </div>
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false" />
    </div>

    <asp:GridView ID="grvIntroItems" runat="server" GridLines="none" CssClass="gridview-custom" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="20" ClientIDMode="Static" OnPageIndexChanging="grvIntroItems_PageIndexChanging" OnSorting="grvIntroItems_Sorting" OnRowDataBound="grvIntroItems_RowDataBound">
        <EmptyDataTemplate>
            <div class="NoDataStyle">
                No data was returned.
            </div>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" id="chkRowItem" runat="server" class="checkbox-custom chkRowItem" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Id" SortExpression="Id" ShowHeader="false" AccessibleHeaderText="Id">
                        <ItemTemplate>
                            <label id="lbId" runat="server"><%# Eval("Id") %></label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

            <asp:HyperLinkField DataTextField="Name_VN" HeaderText="Name" SortExpression="Name_VN" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="{0}" AccessibleHeaderText="Name" />
            <asp:TemplateField HeaderText="Priority">
                <ItemTemplate>
                    <input type="text" id="tbxPriority" class="priority-item" data-id="<%# Eval("Id") %>" value="<%# Eval("Priority") %>" style="width: 100px;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Disabled">
                <ItemTemplate>
                    <input type="checkbox" id="chkDisable" runat="server" class="disable-item" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" SortExpression="UpdatedDate" />
        </Columns>
    </asp:GridView>
</div>

<script type="text/javascript">
    var chxChecked = false;

    $(function () {
        $(".chkRowItem").each(function (idx, item) {
            var $chx = $(item);
            if ($chx.is(":checked")) {
                $chx.prop("checked", false);
            }
        });

        $("#btnDelete").prop("disabled", true);
    });

    $(".chkRowItem").on('click', function () {
        chxChecked = true;
        
    });

    $('.priority-item').on('click', function (e) {
        e.preventDefault();

        return false;
    });

    //$('.disable-item').on('click', function (e) {
    //    e.preventDefault();
    //    $cbx = $(e.target);
    //    if ($cbx.is(':checked'))
    //        $cbx.prop('checked', true);
    //    else
    //        $cbx.prop('checked', false);

    //    //return false;
    //});

    $('.disable-item').on('change', function (e) {
        e.preventDefault();
    });

    $('.priority-item').on('blur', function (e) {
        e.preventDefault();
        var $tbx = $(e.target);
        console.log('data-id: ' + $tbx.attr('data-id'));
        var priority = $tbx.val();
        if (priority)
        {
            var obj = {
                Id: $tbx.attr('data-id'),
                Priority: priority
            };

            $.ajax({
                url: "WebHandler/UpdateNewsHandler.ashx",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (result) {
                    if (result.Code < 0) {
                        alert(result.ErrorMessage);
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error code: " + xhr.status + " - " + error);
                }
            });
        }

        return false;
    });

    $("#grvIntroItems").on('click', 'tr', function (e) {
        $target = $(e.target);
        if ($target.hasClass('disable-item')) {
            disableItemProcess($target);
        }
        else {
            if (chxChecked) {
                //--
                checkRowsChecked();
                //---
                chxChecked = false;
                return;
            }

            var idx = $(this).index();
            if (idx > 0) {
                var $chx = $(this).find(".chkRowItem");
                if ($chx.is(":checked")) {
                    $chx.prop("checked", false);
                    checkRowsChecked();
                }
                else {
                    $chx.prop("checked", true);
                    //----
                    $("#btnDelete").prop("disabled", false);
                }
            }
        }
    });

    function disableItemProcess($cbx) {
        var disable = false;
        if ($cbx.is(":checked")) {
            disable = true;
        }

        var obj = {
            Id: $cbx.attr('data-id'),
            Disable: disable
        };

        $.ajax({
            url: "WebHandler/UpdateNewsHandler.ashx",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(obj),
            success: function (result) {
                if (result.Code < 0) {
                    alert(result.ErrorMessage);
                }
            },
            error: function (xhr, status, error) {
                alert("Error code: " + xhr.status + " - " + error);
            }
        });
    }

    function checkRowsChecked() {
        var changed = false;

        $(".chkRowItem").each(function (idx, item) {
            var $chx = $(item);
            if ($chx.is(":checked")) {

                $("#btnDelete").prop("disabled", false);
                changed = true;

                return;
            }
        });

        if (!changed) {
            $("#btnDelete").prop("disabled", true);
        }
    }

    $(document).ready(function () {

        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete?")) {
                __doPostBack('', 'DeleteNewsItems');
            }
        });
    });
</script>
