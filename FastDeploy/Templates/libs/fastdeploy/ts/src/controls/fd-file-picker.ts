export interface IFastDeployFilePicker {
    init(container: JQuery<HTMLElement>): void;
}

export class FastDeployFilePicker implements IFastDeployFilePicker {
    init(container: JQuery<HTMLElement>): void {
        
    }
}

export class FastDeployImagePicker implements IFastDeployFilePicker {
    init(container: JQuery<HTMLElement>): void {
        container.find('.img-preview').on('click', imagePreviewOnClick);
        container.find('.file-upload').on('change', imagePickerFileChanged);
    }
}

function imagePreviewOnClick(e: Event) {
    e.stopPropagation();
    
    let el = $(e.currentTarget as HTMLElement),
        container = el.parents('.upload-container');

    container.find('.file-upload').trigger('click');
}

function imagePickerFileChanged(e: Event) {
    e.stopPropagation();

    let el = (e.currentTarget as HTMLInputElement);
    if (el.files && el.files.length) {
        let file = el.files[0],
            reader = new FileReader();

        reader.readAsDataURL(file);
        reader.onload = (ev: any) => {
            if (ev.target) {
                let img = $(el).parents('.upload-container').find('.img-preview');
                img.attr('src', ev.target.result);
            }
        };
    }
}

// export default { FastDeployFilePicker, FastDeployImagePicker }