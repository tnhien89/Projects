export default class FastDeploySelector {
    constructor() {
    }

    init() {
        this.createSelectContainer();

        $('.fd-select--button').on('click', function (e) {
            e.stopPropagation();

            let el = $(e.currentTarget),
                ul = el.parent().find('ul');

            el.toggleClass('focus');

            if (ul && ul.length) {
                ul.slideToggle();
            }
        });

        $('.fd-select--dropdown > li').on('click', function (e) {
            e.stopPropagation();

            let el = $(e.currentTarget),
                val = el.attr('data-value'),
                container = el.closest('.fd-select'),
                btn = container.children('.fd-select--button');

            if (btn && btn.length) {
                btn.html(el.html());
                btn.trigger('click');

                if (val) {
                    container.children('select').val(val);
                }

                if (!el.hasClass('selected')) {
                    container.find('li.selected').removeClass('selected');
                    el.addClass('selected');
                }
            }
        });
    }

    createSelectContainer(): void {
        $('.fd-select').each(function (){
            let selectEl = $(this);

            let container = $('<div></div>', {
                'class': selectEl.attr('class')
            });

            let btn = $('<a></a>', {
                    'class': 'fd-select--button',
                    'data-value': '',
                    'html': selectEl.find('.selected').html()
                });

            let ul = $('<ul></ul>', {
                    'class': 'fd-select--dropdown',
                    'style': 'display: none;'
                });

            selectEl.attr('class', '');
            selectEl.hide();

            $.each(selectEl.children(), function () {
                let item = $(this),
                    li = $('<li></li>', {
                        'class': item.attr('class'),
                        'data-value': item.val(),
                        'html': item.html()
                    });

                ul.append(li);
            });

            container.append(btn);
            container.append(ul);
            container.append(selectEl.clone());

            selectEl.replaceWith(container);
        });
    }
}