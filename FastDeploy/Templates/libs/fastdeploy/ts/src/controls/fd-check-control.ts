export default class FastDeployCheckControl {
    constructor() {}

    init(): void {
        $('.fd-checkbox > span').on('click', checkControlOnClick);
        $('.fd-radio > span').on('click', checkControlOnClick);
    }
}

function checkControlOnClick(e: JQuery.ClickEvent): void {
    e.stopPropagation();

    let el = e.currentTarget as HTMLSpanElement;
    if (el) {
        $(el).parent().children('input').trigger('click');
    }
}