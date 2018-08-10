function checkPageValid()
{
    if (typeof (Page_ClientValidate) == 'function') {
        Page_ClientValidate();
    }

    if (Page_IsValid) {
        // do something
        return true;
    }
    
    return false
}