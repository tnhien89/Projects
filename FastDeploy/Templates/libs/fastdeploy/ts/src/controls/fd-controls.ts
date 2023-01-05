import FastDeploySelector from "./fd-selector";
import FastDeployInputTokens from "./fd-input-tokens";
import FastDeployCheckControl from "./fd-check-control";

export function initControls(): void {
    (new FastDeploySelector()).init();
    FastDeployInputTokens.sharedInstance.init();

    (new FastDeployCheckControl()).init();
}