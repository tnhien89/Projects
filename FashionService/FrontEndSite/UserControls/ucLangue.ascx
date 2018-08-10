<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLangue.ascx.cs" Inherits="FrontEndSite.UserControls.ucLangue" %>

<div class="langue-bar-item">
    <img id="imgVN" src="../Images/Langue/vn.gif" data-id="VN" />
</div>
<div class="langue-bar-item">
    <img id="imgEN" src="../Images/Langue/en.gif" data-id="EN" />
</div>

<script type="text/javascript">
    $(".langue-bar").on('click', 'img', function () {
        var id = $(this).data('id');
        var url = window.location.pathname;
        var params = window.location.href.split('?')[1];

        if (params !== undefined && params.length > 0) {
            url += '?' + params;
        }
        else {
            params = window.location.href.split('#')[1];
            if (params !== undefined && params.length > 0) {
                url += '#' + params;
            }
        }

        if (id === 'EN') {
            if (url.indexOf('/EN') === 0) {
                return false;
            }

            url = '/EN' + url;
            window.location.replace(url);

            return true;
        }

        if (url.indexOf('/EN') < 0) {
            return false;
        }

        url = '/' + url.split('/')[2];
        window.location.replace(url);
    });
</script>
