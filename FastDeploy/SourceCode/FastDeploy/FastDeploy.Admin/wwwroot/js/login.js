$('.icon-show-hide-password').on('click', function (e) {
    e.stopPropagation();
    let el = $(e.currentTarget);
    if (el.hasClass('fa-eye-slash')) {
        el.attr('class', 'fa fa-eye icon-show-hide-password');
        //--
        $('#tbx_password').attr('type', 'text');
    }
    else {
        el.attr('class', 'fa fa-eye-slash icon-show-hide-password');
        //----
        $('#tbx_password').attr('type', 'password');
    }
});
function form_login_submit(e) {
    e.stopPropagation();
    $('#form_login').trigger('submit', function () {
        console.log('form submit');
        return true;
    });
}
//# sourceMappingURL=login.js.map