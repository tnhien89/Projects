export default class FastDeployTopBar {
    constructor() {
    }

    init(): void {
        $('.user-profile--button').on('click', userProfileOnClick);
    }

    public static lostFocus(): void {
        $('.user-profile--menu').hide();
    }
}

function userProfileOnClick(e: JQuery.ClickEvent) {
    e.stopPropagation();

    $('.user-profile--menu').toggle();
}