$(document).ready(function() {

    body_sizer();

    $(function() {
        $('.scroll-sidebar').slimscroll({
            height: '100%',
            color: 'rgba(155, 164, 169, 0.68)',
            size: '6px'
        });
    });

    $(function() {
        $('#sidebar-menu').hover(function(){
            $('#page-sidebar').toggleClass('sidebar-hover');
        });
    });

});

$(window).on('resize', function() {
    body_sizer();
});

function body_sizer() {


        var windowHeight = $(window).height();
        var headerHeight = $('#page-header').height();
        var contentHeight = windowHeight - headerHeight;

        $('#page-sidebar').css('height', windowHeight);
        $('.scroll-sidebar').css('height', contentHeight);
        $('#page-content').css('min-height', contentHeight);

};

function pageTransitions() {

    var transitions = ['.pt-page-moveFromLeft', 'pt-page-moveFromRight', 'pt-page-moveFromTop', 'pt-page-moveFromBottom', 'pt-page-fade', 'pt-page-moveFromLeftFade', 'pt-page-moveFromRightFade', 'pt-page-moveFromTopFade', 'pt-page-moveFromBottomFade', 'pt-page-scaleUp', 'pt-page-scaleUpCenter', 'pt-page-flipInLeft', 'pt-page-flipInRight', 'pt-page-flipInBottom', 'pt-page-flipInTop', 'pt-page-rotatePullRight', 'pt-page-rotatePullLeft', 'pt-page-rotatePullTop', 'pt-page-rotatePullBottom', 'pt-page-rotateUnfoldLeft', 'pt-page-rotateUnfoldRight', 'pt-page-rotateUnfoldTop', 'pt-page-rotateUnfoldBottom'];
    for (var i in transitions) {
        var transition_name = transitions[i];
        if ($('.add-transition').hasClass(transition_name)) {

            $('.add-transition').addClass(transition_name + '-init page-transition');

            setTimeout(function() {
                $('.add-transition').removeClass(transition_name + ' ' + transition_name + '-init page-transition');
            }, 1200);
            return;
        }
    }

};

$(document).ready(function() {

    pageTransitions();

    /* Sidebar menu */
    $(function() {

        $('#sidebar-menu').superclick({
            animation: {
                height: 'show'
            },
            animationOut: {
                height: 'hide'
            }
        });

    });

    /* Colapse sidebar */
    $(function() {

        $('#close-sidebar').click(function() {
            $('body').toggleClass('closed-sidebar');
            $('.glyph-icon', this).toggleClass('icon-outdent').toggleClass('icon-indent');
        });

    });

    /* Sidebar scroll */



});