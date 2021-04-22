function setIframeHeight(iframe) {
	if (iframe) {
		var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
		if (iframeWin.document.body) {
			iframe.height = iframeWin.document.body.scrollHeight + 50;
			$(".layui-tab").css("height", (iframeWin.document.body.scrollHeight) + 50);
		}
	}
};
// iframeWin.document.documentElement.scrollHeight || 
window.onload = function() {
	setIframeHeight(document.getElementById('external-frame'));
};
layui.use('element', function() {
	var element = layui.element;

	//一些事件监听
	element.on('tab(docDemoTabBrief)', function(data) {
		switch (data.index) {
			//case 0:
			//	$("iframe").attr("src", "Self_Introduction");
			//	break;
			case 0:
				$("iframe").attr("src", "Design_Document");
				break;
			case 1:
				$("iframe").attr("src", "BlogIntroduction");
				break;
			case 2:
				$("iframe").attr("src", "Timeline");
				break;
			default:
				break;
				// 	case 1:
				// 		$("iframe").attr("src", "ChatRoom.html");
				// 		break;
				// 		case 
				// 	case 4:
				// 		$("iframe").attr("src", "Timeline.html");
				// 		break;
		}
	});
});
