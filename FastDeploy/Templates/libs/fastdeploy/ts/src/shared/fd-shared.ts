import FastDeploySideBar from "./fd-sidebar";
import FastDeployTopBar from "./fd-topbar";
import FastDeployList from "./fd-list";

export default class FastDeployShared {
    constructor() {}

    init(): void {
        (new FastDeploySideBar()).init();
        (new FastDeployTopBar()).init();

        (new FastDeployList()).initEvents();
    }

    public static lostFocus(): void {
        FastDeploySideBar.lostFocus();
        FastDeployTopBar.lostFocus();
    }
}