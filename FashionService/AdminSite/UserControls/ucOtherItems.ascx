<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucOtherItems.ascx.cs" Inherits="AdminSite.UserControls.ucOtherItems" %>
<%@ Register Src="~/UserControls/ucSortOther.ascx" TagPrefix="uc1" TagName="ucSortOther" %>


<div class="panel-heading panel-heading-custom">
    <label id="Label1" runat="server">Other</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <input type="button" id="btnDelete" value="Delete" class="btn btn-danger btn-danger-custom" />
        <input type="button" id="btnSortOther" value="Sắp xếp" class="btn btn-primary btn-primary-custom" data-toggle="modal" data-target="#modalSortOther"/>
    </div>
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false" />
    </div>

    <asp:GridView ID="grvOtherItems" runat="server" GridLines="none" CssClass="gridview-custom" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="20" ClientIDMode="Static" OnPageIndexChanging="grvOtherItems_PageIndexChanging" OnSorting="grvOtherItems_Sorting" OnRowDataBound="grvOtherItems_RowDataBound">
        <EmptyDataTemplate>
            <div class="NoDataStyle">
                No data found.
            </div>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" id="chkRowItem" runat="server" class="checkbox-custom chkRowItem" onclick="checkRow();" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Id" SortExpression="Id" ShowHeader="false" AccessibleHeaderText="Id">
                <ItemTemplate>
                    <label id="lbId" runat="server"><%# Eval("Id") %></label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="Name_VN" runat="server" DataTextField="Name_VN" HeaderText="Name" SortExpression="Name_VN" Text='<%# Eval("Name_VN") %>' NavigateUrl='<%# string.Format("~/Other.aspx?Id={0}", Eval("Id")) %>' AccessibleHeaderText="Name" />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" SortExpression="UpdatedDate"/>
        </Columns>
    </asp:GridView>
</div>

<div class="modal fade" id="modalSortOther" tabindex="-1" role="dialog" aria-hidden="true">
    <uc1:ucSortOther runat="server" id="ucSortOther" />
</div>

<script type="text/javascript">
    $(function () {
        $("#chkRowItem").each(function () {
            var $chx = $(this);
            if ($chx.is(":checked")) {
                $chx.prop("checked", false);
            }
        });

        $("#btnDelete").prop("disabled", true);
    });

    var chxChecked = false;

    $(".chkRowItem").on('click', function () {
        debugger;
        chxChecked = true;
    });

    $("#grvOtherItems").on('click', 'tr', function () {
        debugger;
        if (chxChecked) {
            //--
            checkRowsChecked();
            //---
            chxChecked = false;
            return;
        }

        var idx = $(this).index();
        if (idx > 0) {
            var $chx = $(this).find("input:checkbox");
            if ($chx.is(":checked")) {
                $chx.prop("checked", false);
                checkRowsChecked();
            }
            else if ($chx.is(':visible')) {
                $chx.prop("checked", true);
                //----
                $("#btnDelete").prop("disabled", false);
            }
        }
    });

    $(document).ready(function () {

        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete?")) {
                __doPostBack('', 'DeleteItems');
            }
        });
    });

    function checkRowsChecked() {
        var changed = false;

        $(".chkRowItem").each(function () {
            debugger;
            var $chx = $(this);
            if ($chx.is(':visible') && $chx.is(":checked")) {

                $("#btnDelete").prop("disabled", false);
                changed = true;

                return;
            }
        });

        if (!changed) {
            $("#btnDelete").prop("disabled", true);
        }
    }
</script>