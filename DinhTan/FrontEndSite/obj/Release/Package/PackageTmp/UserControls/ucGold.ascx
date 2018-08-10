<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGold.ascx.cs" Inherits="FrontEndSite.UserControls.ucGold" %>


<div id="goldHeader" runat="server" class="box-right-header">Gold</div>
<div class="box-right-content">
    <table class="table">
        <tr>
            <th></th>
            <th id="thBuy" runat="server">Mua</th>
            <th id="thSell" runat="server">Bán</th>
        </tr>
        <tr>
            <td>SBJ</td>
            <td id="sbjBuy" runat="server"></td>
            <td id="sbjSell" runat="server"></td>
        </tr>
        <tr>
            <td>SJC</td>
            <td id="sjcBuy" runat="server"></td>
            <td id="sjcSell" runat="server"></td>
        </tr>
    </table>
</div>