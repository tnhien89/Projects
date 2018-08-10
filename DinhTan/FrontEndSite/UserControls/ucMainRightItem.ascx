<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMainRightItem.ascx.cs" Inherits="FrontEndSite.UserControls.ucMainRightItem" %>

<div id="content-right">
    <div class="content-right-title">
        <asp:Label ID="lbTitle" runat="server"></asp:Label>
    </div>
    <div class="content-right-content">

        <asp:ListView ID="lvMainRightLinkItems" runat="server" OnDataBound="lvMainRightLinkItems_DataBound" OnItemDataBound="lvMainRightLinkItems_ItemDataBound">
            <ItemTemplate>
                <div class="content-right-content-item">
                    <div class="content-right-content-item-image">
                        <asp:Image ID="imgImageLink" runat="server" ImageUrl='<%# Eval("ImageLink") %>' />
                    </div>
                    <div class="content-right-content-item-link">
                        <asp:HyperLink ID="hplLink" runat="server" NavigateUrl='<%# Eval("Link") %>' Target="_blank" Text='<%# Eval("Name_VN") %>'></asp:HyperLink>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>

    </div>
</div>
