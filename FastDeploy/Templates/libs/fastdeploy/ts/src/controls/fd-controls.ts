import FastDeploySelector from "./fd-selector";
import FastDeployInputTokens from "./fd-input-tokens";
import FastDeployCheckControl from "./fd-check-control";
import { IFastDeployFilePicker, FastDeployFilePicker, FastDeployImagePicker } from "./fd-file-picker";

export function initControls(): void {
    (new FastDeploySelector()).init();
    FastDeployInputTokens.sharedInstance.init();

    (new FastDeployCheckControl()).init();
}

export function initImagePicker(container: JQuery<HTMLElement>): void {
    let picker: IFastDeployFilePicker = new FastDeployImagePicker();
    picker.init(container);
}