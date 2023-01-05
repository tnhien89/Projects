import FastDeployShared from "./shared/fd-shared";
import { initControls } from "./controls/fd-controls";

interface IFastDeploy {
    initialize(): void;
}

class FastDeployModel implements IFastDeploy {
    readonly _selectorClass: string = "";

    constructor() {
    }

    initialize(): void {
        initControls();
        //---
        (new FastDeployShared()).init();

        $(document).on('click', function (e: JQuery.ClickEvent) {
            e.stopPropagation();

            FastDeployShared.lostFocus();

            $('.fd-select--button.focus').trigger('click');
        });
    }
}

export function initialize(): void {
    let model: IFastDeploy = new FastDeployModel();
    model.initialize();
}