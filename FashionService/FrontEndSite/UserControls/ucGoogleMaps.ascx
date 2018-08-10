<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGoogleMaps.ascx.cs" Inherits="FrontEndSite.UserControls.ucGoogleMaps" %>

<style>
    .google-maps
    {
        width: 100%;
        height: 500px;
        display: block;
        position: relative;
        float: left;
    }
</style>

<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false">
</script>

<script type="text/javascript">
    var map;
    window.onload = function () {

        var latitude = 10.743764;
        var longitude = 106.610083;

        var latlng = new google.maps.LatLng(latitude, longitude);
        var myOptions = {
            zoom: 16,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("googleMaps"), myOptions);
        var marker = new google.maps.Marker
        (
            {
                position: new google.maps.LatLng(latitude, longitude),
                map: map,
                title: 'Công ty cổ phần đầu tư xây dựng Định Tân'
            }
        );
        var infowindow = new google.maps.InfoWindow({
            content: 'Công ty cổ phần đầu tư xây dựng Định Tân' +
                '<br />9 Đường số 32, Bình Tân, Hồ Chí Minh, Vietnam'
        });
        google.maps.event.addListener(marker, 'click', function () {
            // Calling the open method of the infoWindow 
            infowindow.open(map, marker);
        });
    }
</script>

<div id="googleMaps" class="google-maps"></div>