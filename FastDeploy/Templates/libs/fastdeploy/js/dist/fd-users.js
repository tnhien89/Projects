"use strict";
function userChoosedAvatar(e) {
    let el = e.target;
    if (el.files && el.files.length) {
        let file = el.files[0], reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = (ev) => {
            if (ev.target) {
                $('.upload-avatar-container').find('img').attr('src', ev.target.result);
            }
        };
    }
}
//# sourceMappingURL=fd-users.js.map