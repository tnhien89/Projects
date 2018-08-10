<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAboutMain.ascx.cs" Inherits="FrontEndSite.UserControls.ucAboutMain" %>

<link href="../Content/About.css" rel="stylesheet" />

<div class="About-List">
    <label id="lbError" runat="server" class="errMsg" visible="false"></label>
    <asp:ListView ID="lvAbout" runat="server" OnItemDataBound="lvAbout_ItemDataBound">
        <EmptyDataTemplate>
            <div class="NoDataStyle">No data was returned.</div>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <div class="NoDataStyle">No data was returned.</div>
        </EmptyItemTemplate>

        <ItemTemplate>
            <div class="About-Item">
                <div id='<%# Eval("Id") %>' class="About-Header">
                    <asp:Label ID="lbName" runat="server"><%# Eval("Name_VN") %></asp:Label>
                </div>
                <div class="About-Content">
                    <asp:Literal ID="ltrContent" runat="server" Text='<%# Eval("Content_VN") %>' />
                </div>
            </div>

        </ItemTemplate>
    </asp:ListView>
</div>

