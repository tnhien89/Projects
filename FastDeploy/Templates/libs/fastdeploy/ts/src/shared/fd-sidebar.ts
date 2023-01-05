export default class FastDeploySideBar {
    constructor() {
    }

    init(): void {
        // fix page reload when menu item click
        $('.menu-item > ul').toggle();
        //---
        $('#toggleLeftSideBar').on('click', toggleLeftSideBarOnClick);

        $('.menu-item > a').on('click', menuItemOnClick);
    }

    public static lostFocus(): void {
        if ($('#leftSideBar').hasClass('closed')) {
            let expands = $('li[expand-sub-menu="true"]');
            expands.attr('expand-sub-menu', 'false');
            expands.find('ul').toggle();
        }
    }
}

function toggleLeftSideBarOnClick(e: JQuery.ClickEvent): void {
    e.stopPropagation();

    let sideBar = $('#leftSideBar');
    if (sideBar.hasClass('closed')) {
        sideBar.animate({
            'width': '240px'
        }, function () {
            $('.left-side-bar--menu').removeClass('hide-arrow');
        });

        $('.dashboard-content').animate({
            'margin-left': '240px'
        });

        sideBar.removeClass('closed');
    }
    else {
        $('.left-side-bar--menu').addClass('hide-arrow');

        sideBar.animate({
            'width': '50px'
        }, function () {
            sideBar.addClass('closed');
        });

        $('.dashboard-content').animate({
            'margin-left': '50px'
        });
    }

    let expands = $('li[expand-sub-menu="true"]');
    expands.attr('expand-sub-menu', 'false');
    expands.find('ul').hide(300);
}

function menuItemOnClick(e: JQuery.ClickEvent) {
    e.stopPropagation();

    let el = $(e.currentTarget).closest('.menu-item'),
        expanded = el.attr('expand-sub-menu');
    if (expanded != 'true') {
        let expands = $('li[expand-sub-menu="true"]');
        expands.attr('expand-sub-menu', 'false');
        expands.find('ul').toggle();
    }

    let ul = el.find('ul');
    if (ul && ul.length) {
        if (el.attr('expand-sub-menu') == 'true') {
            el.attr('expand-sub-menu', 'false');
        }
        else {
            el.attr('expand-sub-menu', 'true');
        }

        ul.toggle();
    }
}