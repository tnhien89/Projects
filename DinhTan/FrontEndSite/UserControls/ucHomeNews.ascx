<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHomeNews.ascx.cs" Inherits="FrontEndSite.UserControls.ucHomeNews" %>

<div class="homeListRight">
    <h4 class="homeTitleRight">News</h4>
    <div class="topHomeNews">

        <asp:ListView ID="lvHomeNews" runat="server" OnItemDataBound="lvHomeNews_ItemDataBound">
            <EmptyDataTemplate>
                <div class="NoDataStyle">No data was returned.</div>
            </EmptyDataTemplate>
            <EmptyItemTemplate>
                <div class="NoDataStyle">No data was returned.</div>
            </EmptyItemTemplate>

            <ItemTemplate>
                <div class="topHomeNews-item">
                    <div class="img-news">
                        <asp:Image ID="imgNews" runat="server" ImageUrl='<%# Eval("ImageLink") %>'/>
                    </div>
                    <div class="title-news">
                        <asp:HyperLink ID="hplName" runat="server" Text='<%# Eval("Name_VN") %>'
                        NavigateUrl='<%# string.Format("~/{0}?MenuId={1}&Id={2}", Eval("RedirectUrl"), Eval("MenuId"), Eval("Id")) %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>
