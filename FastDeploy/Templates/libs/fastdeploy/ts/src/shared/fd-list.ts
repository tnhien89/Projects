export default class FastDeployList {
    constructor() {}

    initEvents(): void {
        $('.check-all').on('change', checkboxAllOnChange);
        $('tbody').find('[type="checkbox"]').on('change', checkboxItemOnChange);
    }
}

function checkboxAllOnChange(e: JQuery.ChangeEvent): void {
    e.stopPropagation();

    let el = e.currentTarget as HTMLInputElement;
    if (el.checked) {
        let tbl = $(el).parents('table');
        tbl.find('[type="checkbox"]').prop('checked', true);
    }
    else {
        let tbl = $(el).parents('table');
        tbl.find('[type="checkbox"]').prop('checked', false);
    }
}

function checkboxItemOnChange(e: JQuery.ChangeEvent): void {
    e.stopPropagation();

    let tbl = $(e.currentTarget).parents('table'),
        cbxAll = tbl.find('.check-all'),
        tbody = tbl.find('tbody'),
        isCheckAll = true;

    if (cbxAll && cbxAll.length) {
        $.each(tbody.find('[type="checkbox"]'), function () {
            let cbx = this as HTMLInputElement;
            if (!cbx.checked) {
                (cbxAll[0] as HTMLInputElement).indeterminate = true;
    
                isCheckAll = false;
    
                return false;
            }
        });

        if (isCheckAll) {
            let cbx = cbxAll[0] as HTMLInputElement;
            cbx.indeterminate = false;
            cbx.checked = true;
        }
    }
}