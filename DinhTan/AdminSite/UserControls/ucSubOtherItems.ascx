<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSubOtherItems.ascx.cs" Inherits="AdminSite.UserControls.ucSubOtherItems" %>

<div class="panel-heading panel-heading-custom">
    <label id="Label1" runat="server">Other</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <input type="button" id="btnDelete" value="Delete" class="btn btn-danger btn-danger-custom" />
    </div>
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false" />
    </div>

    <asp:GridView ID="grvSubOtherItems" runat="server" GridLines="none" CssClass="gridview-custom" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="20" ClientIDMode="Static" OnPageIndexChanging="grvSubOtherItems_PageIndexChanging" OnSorting="grvSubOtherItems_Sorting" OnRowDataBound="grvSubOtherItems_RowDataBound">
        <EmptyDataTemplate>
            <div class="NoDataStyle">
                No data found.
            </div>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" id="chkRowItem" runat="server" class="checkbox-custom chkRowItem"/>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Id" SortExpression="Id" ShowHeader="false" AccessibleHeaderText="Id">
                <ItemTemplate>
                    <label id="lbId" runat="server"><%# Eval("Id") %></label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="ImageLink" runat="server" HeaderText="Image" ImageUrl='<%# Eval("ImageLink") %>' CssClass="ImageRow"></asp:Image>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="Name_VN" runat="server" HeaderText="Name" SortExpression="Name_VN" NavigateUrl='<%# Eval("Id") %>' Text='<%# Eval("Name_VN") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:HyperLinkField DataTextField="Link" HeaderText="Link" SortExpression="Link" DataNavigateUrlFields="Link" DataNavigateUrlFormatString="{0}" Target="_blank"/>
            <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" SortExpression="UpdatedDate"/>
        </Columns>
    </asp:GridView>
</div>

<script type="text/javascript">
    $(function () {
        $(".chkRowItem").each(function () {
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

    $("#grvSubOtherItems").on('click', 'tr', function () {
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
            else {
                $chx.prop("checked", true);
                //----
                $("#btnDelete").prop("disabled", false);
            }
        }
    });

    function checkRowsChecked() {
        var changed = false;

        $(".chkRowItem").each(function () {
            debugger;
            var $chx = $(this);
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

        //$("#grvSubOtherItems tr").click(function () {
        //    if (chxChecked) {
        //        chxChecked = false;
        //        return;
        //    }

        //    var idx = $(this).index();
        //    if (idx > 0) {
        //        var $chx = $(this).find("input:checkbox");
        //        if ($chx.is(":checked")) {
        //            $chx.prop("checked", false);
        //            checkRowsChecked();
        //        }
        //        else {
        //            $chx.prop("checked", true);
        //            //----
        //            $("#btnDelete").prop("disabled", false);
        //        }
        //    }
        //});

        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete?")) {
                __doPostBack('', 'DeleteItems');
            }
        });
    });
</script>