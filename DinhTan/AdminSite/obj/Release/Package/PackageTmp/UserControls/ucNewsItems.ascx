<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewsItems.ascx.cs" Inherits="AdminSite.UserControls.ucNewsItems" %>

<div class="panel-heading panel-heading-custom">
        <label id="lbHeader" runat="server">Danh Sách Tin</label>
    </div>
    <div class="panel-body panel-body-custom">
        <div class="col-lg-12">
            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info btn-info-custom" OnClick="btnAdd_Click"/>
            <input type="button" id="btnDelete" value="Delete" class="btn btn-danger btn-danger-custom" />
        </div>
        <div class="col-lg-12">
            <label id="lbError" runat="server" class="errMsg" visible="false"/>
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
                    <asp:HyperLinkField DataTextField="Name_VN" HeaderText="Name" SortExpression="Name_VN" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="{0}" AccessibleHeaderText="Name"/>
                    
                    <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" SortExpression="UpdatedDate"/>
                </Columns>
            </asp:GridView>
    </div>

<script type="text/javascript">
    $(function () {
        $(".chkRowItem").each(function () {
            var $chx = $(this);
            if ($chx.is(":checked"))
            {
                $chx.prop("checked", false);
            }
        });

        $("#btnDelete").prop("disabled", true);
    });

    $(".chkRowItem").on('click', function () {
        debugger;
        chxChecked = true;
    });

    $("#grvIntroItems").on('click', 'tr', function () {
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

        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete?")) {
                __doPostBack('', 'DeleteNewsItems');
            }
        });
    });
</script>