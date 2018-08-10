<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBannerList.ascx.cs" Inherits="AdminSite.UserControls.ucBannerList" %>

<div class="panel-heading panel-heading-custom">
    <label id="Label1" runat="server">Danh sách banner</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <input type="button" id="Button1" value="Delete" class="btn btn-danger btn-danger-custom" />
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
                    <input type="checkbox" id="chkRowItem" runat="server" class="checkbox-custom" onclick="checkRow();" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Id" SortExpression="Id" ShowHeader="false" AccessibleHeaderText="Id">
                <ItemTemplate>
                    <label id="lbId" runat="server"><%# Eval("Id") %></label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:HyperLinkField DataTextField="Name_VN" HeaderText="Name" SortExpression="Name_VN" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="{0}" AccessibleHeaderText="Name" />
            <%--<asp:BoundField DataField="Name_VN" HeaderText="Name" SortExpression="Name_VN"/>--%>
            <asp:BoundField DataField="Description_VN" HeaderText="Description" SortExpression="Description_VN" AccessibleHeaderText="Description" />
        </Columns>
    </asp:GridView>
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

    $(document).ready(function () {
        $("#grvIntroItems tr").click(function () {
            if (chxChecked) {
                chxChecked = false;
                return;
            }

            var idx = $(this).index();
            if (idx > 0) {
                var $chx = $(this).find("input:checkbox");
                if ($chx.is(":checked")) {
                    $chx.prop("checked", false);
                    //----
                    checkRowsChecked();
                }
                else {
                    $chx.prop("checked", true);
                    //----
                    $("#btnDelete").prop("disabled", false);
                }
            }
        });

        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete?")) {
                __doPostBack('', 'DeleteBannerItems');
            }
        });
    });

    function checkRowsChecked() {
        $("#chkRowItem").each(function () {
            var $chx = $(this);
            if ($chx.is(":checked")) {

                $("#btnDelete").prop("disabled", false);
                return;
            }
        });

        $("#btnDelete").prop("disabled", true);
    }

    function checkRow() {
        var check = true;

        $("#chkRowItem").each(function () {
            var $chx = $(this);
            if ($chx.is(":checked")) {

                check = false;
                return;
            }
        });

        $("#btnDelete").prop("disabled", check);
        chxChecked = true;
    }
</script>