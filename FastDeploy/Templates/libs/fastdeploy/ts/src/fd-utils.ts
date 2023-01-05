export default class FastDeployUtils {
    private static _instance: FastDeployUtils;

    constructor() {}

    public static get shared() {
        return this._instance || (this._instance = new FastDeployUtils());
    }

    private _p8(s: boolean): string {
        let p = (Math.random().toString(16)+"000000000").substring(2,8);

        return s ? "-" + p.substring(0,4) + "-" + p.substring(4,4) : p ;
    }

    createGuid(): string {
        return this._p8(false) + this._p8(true) + this._p8(true) + this._p8(false);
    }
}