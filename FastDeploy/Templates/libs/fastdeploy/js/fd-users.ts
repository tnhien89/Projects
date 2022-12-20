function userChoosedAvatar(e: Event): void {
    let el = (e.target as HTMLInputElement);

    if (el.files && el.files.length) {
        let file = el.files[0],
            reader = new FileReader();

        reader.readAsDataURL(file);
        reader.onload = (ev: any) => {
            if (ev.target) {
                $('.upload-avatar-container').find('img').attr('src', ev.target.result);
            }
        };
    }
}