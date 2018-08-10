$(function () {

	var url = window.location.pathname;

	if (url.indexOf('/EN/') == 0) {
		document.cookie = 'SelectedLangue=EN';
	}
	else {
		document.cookie = 'SelectedLangue=VN';
	}
});