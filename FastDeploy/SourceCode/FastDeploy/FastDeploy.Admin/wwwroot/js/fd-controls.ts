module JQueryExtensions {
    export function fdControls() {}
}

interface JQuery{
    fdControls(): JQuery<HTMLElement>;
}

(function ($:JQueryStatic) {
    $.fn.fdControls = function () {
        if (this && this.length) {
            this.on('click', function (e) {
                checkboxOnClick(e);
            });
        }

        return this;
    };

    function checkboxOnClick (e) {
        e.stopPropagation();
        //---
        let el = $(e.currentTarget);
        if (el.hasClass('fa-square-check')) {
            if (el.attr('data-check-all') !== undefined) {
                // uncheck all checkbox
                $('.fd-checkbox').attr('class', 'fa-regular fa-square fd-checkbox');
            }
            else {
                el.attr('class', 'fa-regular fa-square fd-checkbox');
            }
        }
        else {
            if (el.attr('data-check-all') !== undefined) {
                // check all checkbox
                $('.fd-checkbox').attr('class', 'fa-regular fa-square-check fd-checkbox');
            }
            else {
                el.attr('class', 'fa-regular fa-square-check fd-checkbox');
            }
        }
    }
})(jQuery);