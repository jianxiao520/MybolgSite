$(document).ready(function (){
	var interval = {};
	var GcFunDong = {};

	$("#player").click(function(){
		if($("#player").css("background-position")=="0px 0px"){
			// 开始播放	
			Play_Even();
		}else{
			Stop_Even();
		}
    });
    $(".ion-likeList").click(function () {
        layer.open({
            type: 2,
            area: ['500px', '700px'],
            fixed: false, //不固定
            maxmin: true,
            content: 'Comment',
            title: "留言"
        });
    });

	$(".ion-windows").click(function(){
		var Num = getRndInteger(1,7);
		$("html").css("background","url('img/bz"+Num+".jpg') fixed");
		// $("html").css("background-size","100% 150%");
	})
	function Zm_Roll() {
		$("#Huad").css("top", $('#Huad').position().top - 0.08);
	};
	function Play_Even() {
		var myaudio = $("#myaudio")[0]; 
		$("#player").css("background-position", "-30px 0px");
		myaudio.play(); 
		GcFunDong = setInterval(function() {
			// 判断是否为播放 且 播放时长大于1秒(放到服务器需要加载时间)
			if($("#player").css("background-position")=="-30px 0px" && Math.floor($("#myaudio")[0].currentTime)>1){
				Zm_Roll();
			}
		}, 10);
		interval = setInterval(GetProgress,1000);
	};
	function Stop_Even(){
		$('#YuanPan').stop();
		$("#player").css("background-position", "-0px 0px");
		myaudio.pause();
		clearInterval(interval);//停止
		clearInterval(GcFunDong);//停止
	};
	$(".ion-likeList").click(function(){
		$(".ion-likeList a").css("color","#ff4343");
	});


	function GetProgress(){
		// 获取媒体播放时间
		var Times = Math.floor($("#myaudio")[0].currentTime);
		if (Times<235) {
			var Percent = Times/235*100;
			// 转换成百分比
			$("#Jindu").css("width", Percent +"%");
			// 秒->00:00
			$(".player_music__time").html(secondToMin(Times)+" / 03:55");
			
		} else{
			// 播放完毕后的恢复
			$("#Jindu").css("width", "0%");
			$("#Huad").css("top", "100px");
			$("#myaudio")[0].currentTime=0;
			$(".player_music__time").html("00:00 / 03:55");
			Stop_Even()
		}
	}; //定时任务
	

});
	function secondToMin(s) {
		var MM = Math.floor(s / 60);
		var SS = s % 60;
		if (MM < 10)
			MM = "0" + MM;
		if (SS < 10)
			SS = "0" + SS;
		var min = MM + ":" + SS;
		return min.split('.')[0];
	}
	// 随机范围数
	function getRndInteger(min, max) {
	    return Math.floor(Math.random() * (max - min) ) + min;
	}