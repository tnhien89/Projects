<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLoggedUser.ascx.cs" Inherits="AdminSite.UserControls.ucLoggedUser" %>

<link href="../Content/user-profile.css" rel="stylesheet" />

<div class="form-logged-user">
    <div class="logged-user-avatar">
        <img id="imgAvatar" runat="server" src="~/Images/user-no-image.jpg"/>
    </div>
    <div class="logged-user-info">
        <div class="logged-user-name">
            <label id="lbFullName" runat="server">Hi, Guest</label>
        </div>
        <div class="logged-user-change">
            <ul>
                <li>
                    <asp:HyperLink ID="hplUserProfile" runat="server">Thông tin</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplLogout" runat="server">Log out</asp:HyperLink>
                </li>
            </ul>
        </div>
    </div>
</div>