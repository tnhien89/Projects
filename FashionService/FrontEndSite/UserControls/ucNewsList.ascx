<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewsList.ascx.cs" Inherits="FrontEndSite.UserControls.ucNewsList" %>

<div class="news-list">
    <asp:ListView ID="lvNews" runat="server" OnItemDataBound="lvNews_ItemDataBound">
        <EmptyDataTemplate>
            <div class="NoDataStyle">No data was returned.</div>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <div class="NoDataStyle">No data was returned.</div>
        </EmptyItemTemplate>

        <ItemTemplate>
            <div class="news-list-item">
                <div class="news-list-item-image">
                    <asp:Image ID="imgImageLink" runat="server" ImageUrl='<%# Eval("ImageLink") %>'/>
                </div>
                <div class="news-list-item-title">
                    <asp:HyperLink ID="hplName" runat="server" Text='<%# Eval("Name_VN") %>' NavigateUrl='<%# Eval("Id") %>' />
                </div>
                <div class="news-list-item-des">
                    <asp:Label ID="lbDes" runat="server" Text='<%# Eval("Description_VN") %>'></asp:Label>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</div>
