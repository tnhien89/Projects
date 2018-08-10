<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBanner.ascx.cs" Inherits="FrontEndSite.UserControls.ucBanner" %>

<div id="banner-carousel" class="carousel slide" data-ride="carousel">
    <div style="background: #000; opacity: 0.8; width: 100%; height: 100%; display: block; position: absolute;"></div>
    <!-- Indicators -->
    <ol id="olbanners" runat="server" class="carousel-indicators">
        
    </ol>

    <!-- Wrapper for slides -->
    <div id="BannerCarouselInner" runat="server" class="carousel-inner carousel-inner-images-custom" role="listbox">
    </div>
</div>
