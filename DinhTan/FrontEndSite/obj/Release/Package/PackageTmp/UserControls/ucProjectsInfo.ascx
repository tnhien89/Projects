<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucProjectsInfo.ascx.cs" Inherits="FrontEndSite.UserControls.ucProjectsInfo" %>

<link href="../Content/ProjectsInfo.css" rel="stylesheet" />

<div class="Projects-Slider">
    <div style="height: 55px; background: #F0F0F0;"></div>
    <div class="Projects-Slider-Content">
        <div id="projectsCarousel" class="carousel carousel-custom">
            <div class="carousel-inner carousel-inner-custom">
                <asp:ListView ID="lvProjectsCarousel" runat="server" OnItemDataBound="lvProjectsCarousel_ItemDataBound" ClientIDMode="Static">
                    <ItemTemplate>
                        <div id="divItem" runat="server" class="item">
                            <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' ClientIDMode="Static" />
                            <asp:HiddenField ID="hfName" runat="server" Value='<%# Eval("Name_VN") %>' />
                            <div class="col-md-5 Projects-Slider-Prev-Title">
                                <asp:Label ID="lbPrevTitle" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2 ProjectsInfo-itemCount" style="text-align: center;">
                                <asp:Label ID="lbItemsCount" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-5 Projects-Slider-Next-Title">
                                <asp:Label ID="lbNextTitle" runat="server"></asp:Label>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:ListView>

            </div>

            <a class="left carousel-control" href="#projectsCarousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#projectsCarousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
</div>

<div class="Projects-Image-Large">
    <div id="ProjectImagesCarousel" class="carousel slide" data-ride="carousel">
        <div style="background: #000; opacity: 0.8; width: 100%; height: 100%; display: block; position: absolute;"></div>
        <!-- Indicators -->
        <ol id="olProjectIndicators" runat="server" class="carousel-indicators">
        </ol>

        <!-- Wrapper for slides -->
        <div id="ProjectImagesCarouselInner" runat="server" class="carousel-inner carousel-inner-images-custom" role="listbox">
        </div>
    </div>
</div>

<div class="container">
    <div class="row row-project-detail">
        <div class="col-md-4 project-detail-info">
            <div class="views-group">
                <label id="lbProjectName" runat="server" class="views-group-label">Tên dự án</label>
                <label id="lbProjectNameInfo" runat="server" class="views-group-content"></label>
            </div>
            <div class="views-group">
                <label id="lbProjectAddress" runat="server" class="views-group-label">Địa chỉ</label>
                <label id="lbProjectAddressInfo" runat="server" class="views-group-content"></label>
            </div>
            <div class="views-group">
                <label id="lbStartDate" runat="server" class="views-group-label">Ngày khởi công</label>
                <label id="lbStartDateInfo" runat="server" class="views-group-content"></label>
            </div>
            <div class="views-group">
                <label id="lbEndDate" runat="server" class="views-group-label">Ngày hoàn thành</label>
                <label id="lbEndDateInfo" runat="server" class="views-group-content"></label>
            </div>
        </div>
        <div class="col-md-8">
            <label id="lbError" runat="server" class="errMsg" visible="false"></label>
            <div class="projects-detail-content">
                <asp:Label ID="lbContent" runat="server" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $('#projectsCarousel').carousel({
        interval: false
    });

    $('#ProjectImagesCarousel').carousel({
        interval: 6000
    });

    $('#projectsCarousel').on('slid.bs.carousel', function () {

        // This variable contains all kinds of data and methods related to the carousel
        //var carouselData = $(this).data('bs.carousel');
        // EDIT: Doesn't work in Boostrap >= 3.2
        //var currentIndex = carouselData.getActiveIndex();
        debugger;
        //var currentIndex = carouselData.getItemIndex(carouselData.$element.find('.item.active'));

        var $Id = $(this).find(".active #hfId");
        window.location.replace("ProjectsInfo.aspx?Id=" + $Id.val());

        //var $name = $(this).find(".active #hfName");
        //var $imgSrc = $(this).find(".active #hfImage");
        //var $content = $(this).find(".active #hfContent");
        //--
        //window.location.replace("ProjectsInfo.aspx?Id=" + $Id.val());
        //$("#imgCurrentProject").attr('src', $imgSrc.val());
        //$("#lbContent").html($content.val());

        //var total = carouselData.$items.length;

        // Create the text we want to display.
        // We increment the index because humans don't count like machines
        //var text = (currentIndex + 1) + " of " + total;

        //alert(text);

        // You have to create a HTML element <div id="carousel-index"></div>
        // under your carousel to make this work
        //$('#carousel-index').text(text);
    });

</script>
