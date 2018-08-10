// Check xem trình duyệt là IE6 hay IE7
var isIE		= (navigator.userAgent.toLowerCase().indexOf("msie") == -1 ? false : true);
var isIE6	= (navigator.userAgent.toLowerCase().indexOf("msie 6") == -1 ? false : true);
var isIE7	= (navigator.userAgent.toLowerCase().indexOf("msie 7") == -1 ? false : true);
var ie45,ns6,ns4,dom;
if (navigator.appName=="Microsoft Internet Explorer") ie45=parseInt(navigator.appVersion)>=4;
else if (navigator.appName=="Netscape"){  ns6=parseInt(navigator.appVersion)>=5;  ns4=parseInt(navigator.appVersion)<5;}
dom=ie45 || ns6;

function getobj(id) {
	el =  document.getElementById(id);
	return el;
}
function showobj(id) {
obj=getobj(id);
els = dom ? obj.style : obj;
 	if (dom){
	    els.display = "";
    } else if (ns4){
        els.display = "show";
  	}
}

function hideobj(id) {
obj=getobj(id);
els = dom ? obj.style : obj;
 	if (dom){
	    els.display = "none";
    } else if (ns4){
        els.display = "hide";
  	}
}
// khong the phong to cua so
function openPopUp(url, windowName, w, h, scrollbar) {
   var winl = (screen.width - w) / 2;
   var wint = (screen.height - h) / 2;
   winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars='+scrollbar ;
   win = window.open(url, windowName, winprops);
   if (parseInt(navigator.appVersion) >= 4) { 
       	win.window.focus(); 
   } 
}

// co the phong to cua so
var win=null;
function NewWindow(mypage,myname,w,h,scroll,pos){
if(pos=="random"){LeftPosition=(screen.width)?Math.floor(Math.random()*(screen.width-w)):100;TopPosition=(screen.height)?Math.floor(Math.random()*((screen.height-h)-75)):100;}
if(pos=="center"){LeftPosition=(screen.width)?(screen.width-w)/2:100;TopPosition=(screen.height)?(screen.height-h)/2:100;}
else if((pos!="center" && pos!="random") || pos==null){LeftPosition=0;TopPosition=20}
settings='width='+w+',height='+h+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=yes';
win=window.open(mypage,myname,settings);}

// is_num
function is_num(event,f){
	if (event.srcElement) {kc =  event.keyCode;} else {kc =  event.which;}
	if ((kc < 47 || kc > 57) && kc != 8 && kc != 0) return false;
	return true;
}

// bookmarksite 
function bookmarksite(title,url){
	if (window.sidebar) // firefox
		window.sidebar.addPanel(title, url, "");
	else if(window.opera && window.print){ // opera
		var elem = document.createElement('a');
		elem.setAttribute('href',url);
		elem.setAttribute('title',title);
		elem.setAttribute('rel','sidebar');
		elem.click();
	} 
	else if(document.all)// ie
		window.external.AddFavorite(url, title);
}

// setHomepage 
function setHomepage(url)
{
	if (document.all)	{
		document.body.style.behavior='url(#default#homepage)';
		document.body.setHomePage(url);	
	}
	else if (window.sidebar)
	{
		if(window.netscape)
		{
			try			{
			netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
			}
			catch(e)			{
			alert("this action was aviod by your browser if you want to enable please enter about:config in your address line,and change the value of signed.applets.codebase_principal_support to true");
			}
		}
		var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components. interfaces.nsIPrefBranch);
		prefs.setCharPref('browser.startup.homepage',url);
	}
} 

 
/*--------------- Link advertise ----------------*/
 window.rwt = function (obj, type, id) {
	try {
		if (obj === window) {
			obj = window.event.srcElement;
			while (obj) {
				if (obj.href) break;
				obj = obj.parentNode
			}
		}		 
		obj.href = ROOT+'?'+cmd+'=mod:advertise|type:'+type+'|lid:'+id;
		obj.onmousedown = ""
	} catch(o) {}
	return true ;
};

//function load_Statistics ()
//{
//	$.ajax({
//		async: true,
//		dataType: 'json',
//		url: ROOT+"load_ajax.php?do=statistics",
//		type: 'POST',
//		success: function (data) {
//			$("#stats_online").html(data.online);
//			$("#stats_totals").html(data.totals);
//			$("#stats_member").html(data.mem_online);
//		}
//	}) ; 
	 
//} 


//function show_popupBanner ()
//{	 
//	var mydata =  "lang=vn"  ; 
//	$.ajax({
//		async: true,
//		dataType: 'json',
//		url: ROOT+'load_ajax.php?do=popupBanner',
//		type: 'POST',
//		data: mydata ,
//		success: function (data) {
//			if(data.show==1)
//			{
//				vnTRUST.show_overlay_popup('popupBanner', '', data.html,
//				{
//					background: {'background-color' : 'transparent'},
//					border: {
//						'background-color' : 'transparent',
//						'padding' : '0px'
//					},
//					title: {'display' : 'none'},
//					content: {
//						'padding' : '0px',
//						'width' : data.width+'px'
//					} ,
//					pos_type: 'fixed',
//					position : 'center-center'
//				}); 
//			}
//		}
//	}) ; 			
	 
//}

$("#banners-middle .nivoSlider a").click(function(){
	return false;
});

$(".pagination .btn-prev").click(function(){
	if( $(".pagination .pagecur").html() != 1 ) {
		var href_direct = $(".pagination .pagecur").prev("a").attr("href");
		window.location = href_direct;
	}
	return false;
});
$(".pagination .btn-next").click(function(){
	if( $(".pagination .pagecur").html() != $(".pagination .pagetotal").html() ) {
		var href_direct = $(".pagination .pagecur").next("a").attr("href");
		window.location = href_direct;
	}
	return false;
});

function fix_min_height_(){
	var main_height = $("#vnt-container").height();
	var sidebar_height = -150;
			sidebar_height += $(".logo").height();
			sidebar_height += $("#vnt-main-menu").height()
			sidebar_height += $("#box-cat").height() ;
			sidebar_height += $("#banner-left-slider").height();
			sidebar_height += $("#box-banner-left").height();
			sidebar_height += $(".box-sidebar-bottom").height();
			//sidebar_height -= $("#box-site-doc").height();
	
	$("#box-cat .accordion ul").css("height", 0);
	$("#box-banner-left").css("padding-bottom",  $(".box-sidebar-bottom").height() + "px");
		sidebar_height = $("#vnt-sidebar").height();
	$("#box-cat .accordion ul").css("height", "");
	//alert( main_height + ' - ' + sidebar_height);
	if( main_height < sidebar_height ) {
		//$("#vnt-container .inner-main").css("min-height", sidebar_height + "px");
		$("#vnt-container .mid-content").css("min-height", ( sidebar_height - 350 - 200 - $("#box-site-doc").height() ) + "px");
	}
	//var sidebar_height = $("#vnt-main-menu").height()+$("#box-cat").height()+ $("#box-banner-left").height();
	//alert('sidebar_height = '+sidebar_height)
	
};

function fix_min_height() {
	$("#vnt-main").css("min-height", "");
	$("#vnt-sidebar").css("bottom", "");
	// box-site-doc
	var box_sitedoc = 0;
	if( $("#box-site-doc").height() != null )
		box_sitedoc = $("#box-site-doc").height() + parseInt( $("#box-site-doc").css("padding-top").replace("px", "") );
	// logo
	var logo_height = 0
	if( $(".logo").height() != null )
		logo_height = $(".logo").height();
	// menu main
	var vnt_main_menu = 0;
	if( $("#vnt-main-menu").height() != null )
		vnt_main_menu = $("#vnt-main-menu").height();
	// box cat
	var box_cat = 0;
	if( $("#box-cat").height() != null )
		box_cat = $("#box-cat").height();
	
	// banner left slider
	var banner_left_slider = 0;
	if ($('.banner-left') && $('.banner-left').length)
	{
	    $('.banner-left').each(function (idx, item) {
	        if ($(item) && $(item).height()) {
	            banner_left_slider += $(item).height();
	        }
	    });
    }

	//$.each('.banner-left', function (idx, item) {
	//    if ($(item) && $(item).height()) {
	//        banner_left_slider += $(item).height();
	//    }
	//});
	
	//if( $("#banner-left-slider").height() != null )
	//	banner_left_slider = $("#banner-left-slider").height();
    
	//var slideIdx = 1;
	//while ($("#banner-left-slider-" + slideIdx).height() != null)
	//{
	//    banner_left_slider += $("#banner-left-slider-" + slideIdx).height();
	//    slideIdx++;
	//}
	//var banner_left_slider_2 = 0;
	//if( $("#banner-left-slider-2").height() != null )
	//	banner_left_slider_2 = $("#banner-left-slider-2").height();
	//// banner left slider 3
	//var banner_left_slider_3 = 0;
	//if( $("#banner-left-slider-3").height() != null )
	//	banner_left_slider_3 = $("#banner-left-slider-3").height();
	
	// banner left
	var banner_left = 0;
	if( $("#box-banner-left").height() != null )
		banner_left = $("#box-banner-left").height() + parseInt( $("#box-banner-left").css("padding-top").replace("px", "") ) ;
	// box-sidebar-bottom
	var box_sidebar_bottom = 0;
	if( $(".box-sidebar-bottom").height() != null )
		box_sidebar_bottom = $(".box-sidebar-bottom").height() - parseInt( $(".box-sidebar-bottom").css("bottom").replace("px", "") );
	
	var vnt_main_height = $("#vnt-main").height() - box_sitedoc;
	
	
	$("#vnt-sidebar").css("top", "");
	$("#vnt-sidebar").css("bottom", "");
	var sidebar_top = parseInt( $("#vnt-sidebar").css("top").replace("px", "") );
	var sidebar_bot = parseInt( $("#vnt-sidebar").css("bottom").replace("px", "") );
	
	$("#vnt-sidebar").css("height", "0");
	var sidebar_height = logo_height;
			sidebar_height += vnt_main_menu;
			sidebar_height += box_cat;
			sidebar_height += banner_left_slider;
			//sidebar_height += banner_left_slider_2;
			//sidebar_height += banner_left_slider_3;
			sidebar_height += banner_left;
			sidebar_height += box_sidebar_bottom;
			
			sidebar_height += sidebar_top - sidebar_bot;
	
			//sidebar_height -= parseInt($("#vnt-sidebar").css("top").replace("px", ""));
	if( vnt_main_height > sidebar_height ) {
		//alert("main")
		var fix_height = vnt_main_height;
	} else {
		//alert("sidebar")
		var fix_height = sidebar_height;
	}

	console.log('fix_height: ' + fix_height);
	
	var main_page = $("#main-page").height();
	if (main_page != null) {
		$("#main-page").find("#box-news #tab-news").css("padding-bottom", ( fix_height - main_page ) + "px")
	} else {
		main_page = fix_height - $(".box_mid").height();
		//		main_page += parseInt( $(".mid-content").css("padding-top").replace("px", "") ) + parseInt( $(".mid-content").css("padding-bottom").replace("px", "") ) ;
		//$(".mid-content").css("padding-bottom", main_page + "px");
	}

	//main_page += parseInt($(".mid-content").css("padding-top").replace("px", "")) + parseInt($(".mid-content").css("padding-bottom").replace("px", ""));
	//$(".mid-content").css("padding-bottom", main_page + "px");
	
	fix_height += box_sitedoc;
	$("#vnt-sidebar").css("height", "");
	$("#vnt-main").css("min-height", fix_height + "px");
	$("#vnt-sidebar").css("bottom", ( box_sitedoc + sidebar_bot ) + "px");
}

$("#box-support .skype-support a").hover(
	function(){
		var src = $(this).find("img").attr("src").replace(".png", "_hover.png");
		$(this).find("img").attr("src", src);
	},function(){
		var src = $(this).find("img").attr("src").replace("_hover.png", ".png");
		$(this).find("img").attr("src", src);
	}
);

function initLoaded(){	 
	//load_Statistics();	
}