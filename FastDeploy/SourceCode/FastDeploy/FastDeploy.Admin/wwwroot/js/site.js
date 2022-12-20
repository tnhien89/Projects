$(() => {
});
let currentShowMenu = null;
let menuStatus = 0;
$('#toggle_col_left').on('click', function (e) {
    e.stopPropagation();
    let el = $(e.currentTarget);
    if (menuStatus == 0) {
        $('#col_left').css('width', '50px');
        $('.company-info').css('height', '60px');
        $('.company-info').find('img').css({
            'width': '30px',
            'height': '30px',
            'margin-left': '5px',
            'top': '14px'
        });
        $('.company-info').find('h3').hide();
        // $('#menu_show_hide').css('margin-left', '-26px');
        $('#menu_show_hide').find('i').css('left', '-30px');
        $('#menu_show_hide').find('span').hide();
        $('#menu_show_hide').find('h4').hide();
        $('#menu_show_hide').find('ul').attr('style', '');
        $('#menu_show_hide').find('ul').toggleClass('show-on-hover');
        menuStatus = 1;
    }
    else {
        $('#col_left').css('width', '260px');
        $('.company-info').attr('style', '');
        $('.company-info').find('img').attr('style', '');
        $('.company-info').find('h3').show();
        // $('#menu_show_hide').css('margin-left', '-26px');
        $('#menu_show_hide').find('i').attr('style', '');
        $('#menu_show_hide').find('span').attr('style', '');
        $('#menu_show_hide').find('h4').attr('style', '');
        $('#menu_show_hide').find('ul').hide();
        $('#menu_show_hide').find('ul').toggleClass('show-on-hover');
        menuStatus = 0;
    }
    currentShowMenu = null;
});
$('.menu-item').on('click', function (e) {
    e.stopPropagation();
    let ul = $($(e.currentTarget).find('ul'));
    if (ul.is(':hidden')) {
        if (currentShowMenu) {
            currentShowMenu.hide();
        }
        ul.show();
        currentShowMenu = ul;
    }
    else {
        ul.hide();
        currentShowMenu = null;
    }
});
//# sourceMappingURL=site.js.map