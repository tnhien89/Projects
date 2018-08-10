<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenuItems.ascx.cs" Inherits="AdminSite.UserControls.ucMenuItems" %>
<div class="panel-heading panel-heading-custom">
        <label id="lbHeaderIntroMenu" runat="server">Danh mục</label>
    </div>
    <div class="panel-body panel-body-custom">
        <div class="col-lg-12">
            <asp:Button Id="btnAdd" runat="server" Text="Add" CssClass="btn btn-info btn-info-custom" OnClick="btnAdd_Click"/>
            <input type="button" id="btnDelete" value="Delete" class="btn btn-danger btn-danger-custom" disabled="disabled"/>
        </div>
        <div class="col-lg-12">
            <label id="lbError" class="errMsg" runat="server" visible="false"></label>
        </div>
        <asp:GridView ID="grvIntroMenu" runat="server" CssClass="gridview-custom" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="5" ClientIDMode="Static" OnPageIndexChanging="grvIntroMenu_PageIndexChanging" OnSorting="grvIntroMenu_Sorting">
            <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" class="chxRows" id="chkRowMenu" runat="server" onClick="checkBoxRowClick();"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                <%--<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id"/>--%>
                <asp:HyperLinkField DataTextField="Name_VN" HeaderText="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/MenuDetails.aspx?MenuId={0}" SortExpression="Name_VN"/>
                <asp:BoundField DataField="Description_VN" HeaderText="Description" SortExpression="Description_VN"/>
            </Columns>
        </asp:GridView>
    </div>

<script type="text/javascript">
    $(function () {
        $(".chxRows").each(function () {
            if ($(this).is(":checked")) {
                $(this).prop("checked", false);
            }
        });
    });

    var chxChecked = false;

    $(document).ready(function () {
        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete?")) {
                __doPostBack("", "DeleteMenu");
            }
        });

        $("#grvIntroMenu tr").click(function () {

            if (!chxChecked) {
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
                        //-----
                        $("#btnDelete").prop("disabled", false);
                    }
                }
            }
            else {
                chxChecked = false;
            }
        });
    });

    function checkRowsChecked() {
        $("#chkRowMenu").each(function () {
            var $chx = $(this);
            if ($chx.is(":checked")) {

                $("#btnDelete").prop("disabled", false);
                return;
            }
        });

        $("#btnDelete").prop("disabled", true);
    }

    var checkedRows = 0;

    function checkBoxRowClick() {
        var isChecked = false;

        $(".chxRows").each(function () {
            if ($(this).is(":checked"))
            {
                isChecked = true;
                return;
            }
        });

        $("#btnDelete").prop("disabled", !isChecked);
        chxChecked = true;
    }
</script>