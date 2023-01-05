export default class FastDeployInputTokens {
    private static _instance: FastDeployInputTokens;

    token: JQuery;

    constructor(token: any) {
        this.token = token;

        if (!this.token) {
            this.token = $('<div></div>', {
                'class': 'token'
            });
        }
    }

    public static get sharedInstance() {
        return this._instance || (this._instance = new this(null));
    }

    init(): void {
       $('.token-input').on({
            keydown: this.inputTokenOnKeyDown,
            paste: this.inputTokenOnPaste
       });
    }

    inputTokenOnKeyDown(e: any) {
        let input: any = $(e.currentTarget);
        switch (e.keyCode) {
            case 13: // Enter
            case 188: // ,
                e.preventDefault();

                if (input.val() || input.val() != '') {
                    let tk: JQuery = FastDeployInputTokens.sharedInstance.createToken(input.val());
                    tk.insertBefore(input);
                    //---
                    input.val('');
                }
                
                break;
            
            case 8: // Backspace
                if (!input.val() || input.val() == '') {
                    e.preventDefault();
                    input.prev('.token').remove();
                }
                
                break;
        }
    }

    inputTokenOnPaste(e: any) {
        e.preventDefault();

        let input = e.currentTarget as HTMLInputElement,
            text = e.originalEvent.clipboardData.getData('text');

        if (text?.length) {
            if ((input.selectionEnd ?? 0) > (input.selectionStart ?? 0)) {
                text = `${input.value.substring(0, input.selectionStart ?? 0)}${text}${input.value.substring(input.selectionEnd ?? 0)}`
            }
            else {
                input.value.replace
            }

            text = `${input.value}${text}`;
        }

        if (text?.indexOf(',') >= 0) {
            let arr = text.split(',');
            if (arr?.length) {
                for (let i = 0; i < arr.length - 1; i++) {
                    if (arr[i]?.length) {
                        let tk = FastDeployInputTokens.sharedInstance.createToken(arr[i]);
                        tk.insertBefore(input);
                    }
                }
            }

            input.value = arr[arr.length - 1] ?? '';
        }
    }

    createToken(text: string): JQuery {
        let tk = FastDeployInputTokens.sharedInstance.token.clone();
        tk.html(text);
        tk.append($('<i class="fa fa-xmark"></i>'));
        //---
        tk.children('i').on('click', function (e) {
            e.stopPropagation();
            $(e.currentTarget).parent().remove();
        });

        return tk;
    }
}