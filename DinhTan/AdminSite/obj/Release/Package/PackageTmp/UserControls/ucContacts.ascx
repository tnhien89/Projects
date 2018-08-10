<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucContacts.ascx.cs" Inherits="AdminSite.UserControls.ucContacts" %>

<div class="panel-heading panel-heading-custom">
        <label id="lbHeader" runat="server">Danh Sách Liên Hệ</label>
    </div>
    <div class="panel-body panel-body-custom">
        <div class="col-lg-12">
            <input type="button" id="btnDelete" value="Delete" class="btn btn-danger btn-danger-custom" />
        </div>
        <div class="col-lg-12">
            <label id="lbError" runat="server" class="errMsg" visible="false"/>
        </div>
        
            <asp:GridView ID="grvContactsItems" runat="server" GridLines="none" CssClass="gridview-custom" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="20" ClientIDMode="Static" OnPageIndexChanging="grvContactsItems_PageIndexChanging" OnSorting="grvContactsItems_Sorting"
                 OnRowDataBound="grvContactsItems_RowDataBound">

                <EmptyDataTemplate>
                    <div class="NoDataStyle">
                        No data was returned.
                    </div>
                </EmptyDataTemplate>

                

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" id="chkRowItem" runat="server" class="checkbox-custom" onclick="checkRow();"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" ShowHeader="false" AccessibleHeaderText="Id">
                        <ItemTemplate>
                            <label id="lbId" runat="server"><%# Eval("Id") %></label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="Subject_VN" HeaderText="Tiêu đề" SortExpression="Subject_VN" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/ContactDetail.aspx?Id={0}" AccessibleHeaderText="Name"/>
                    <asp:BoundField DataField="Name_VN" HeaderText="Họ và tên" SortExpression="Name_VN"/>
                    <asp:BoundField DataField="Address_VN" HeaderText="Địa chỉ" SortExpression="Address_VN"/>
                    <asp:BoundField DataField="Phone" HeaderText="Điệnt thoại" SortExpression="Phone"/>
                    <%--<asp:BoundField DataField="Name_VN" HeaderText="Name" SortExpression="Name_VN"/>--%>
                    <%--<asp:BoundField DataField="Description_VN" HeaderText="Description" SortExpression="Description_VN" AccessibleHeaderText="Description"/>--%>
                </Columns>
            </asp:GridView>
    </div>