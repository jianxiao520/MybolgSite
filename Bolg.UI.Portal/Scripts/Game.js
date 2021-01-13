var Seep = 4000; //棍子变速
var NowMark = 0; //当前分数
var Deviation = 10; //允许偏移
var InitialWidth = 250; //初始柱子宽度
var Gz_Left = 50; //棍子位置
var IsTrue_Down = true; //是否可以按下
var IsTrue_Up = true; //是否可以按下

$(function() {
	// 初始化
	Initialize();
	var IsTime = 0;
	//按钮绑定事件
	$(".btnClick").bind({
		mousedown: function() {
			Down();
		},
		mouseup: function() {
			Up();
		}
	});
	$(document).keydown(function(e){
		if(e.keyCode==32){
			Down();
		}
	}).keyup(function(e){
		if(e.keyCode==32){
			Up();
		}
	});
    //获取排行榜
    RefreshUses();
	//按下事务
	function Down() {
		if (IsTrue_Down == true) {
			IsTrue_Down = false;
			//柱子变长
			Transaction_Pillars.Animate(Seep);
			//记录时间
			IsTime = new Date().getTime();
		}
	}

	//松开事务
	function Up() {
		if (IsTrue_Up == true) {
			IsTrue_Up = false;
			//中断并倒下柱子
			Transaction_Pillars.Interrupt();
			//记录按下-松开时间间距
			IsTime = new Date().getTime() - IsTime;
			ManRun();
		}
	}

	//人物起跑
	function ManRun(CallBack) {
		//棍子的长度
		var GzLen = Transaction_Pillars.Getwidth_stick();
		//起跑
		Transaction_Man.Go(GzLen, IsTime, IsSuccess);
	}

	//判断是否成功
	function IsSuccess() {
		//棍子的宽度
		var GzLen = Transaction_Pillars.Getwidth_stick();
		//获取柱子宽度
		var ZzWidth = Transaction_Pillars.Getwidth_pillars();
		//第二个柱子的距离
		var ZzLen = Transaction_Pillars.GetLeft("last");
		if (GzLen >= ZzLen + ZzWidth - Gz_Left || GzLen <= ZzLen - Gz_Left) { //判定标准
            Transaction_Man.Drop(function () {
                //游戏结束判断是否上传分数
                UpScore(NowMark);
				//alert("结束啦~！你最终的得分：" + NowMark);
				Initialize();
			});
		} else {
			Rearrangement();
			NowMark++;
            $(".score").html(NowMark);
		}
	}

	//刷新下一个
	function Rearrangement(Pos_Left) {
		//第二个柱子的距离
		var ZzLen = Transaction_Pillars.GetLeft("last");
		$(".container-Game").animate({
			left: "-" + ZzLen + "px"
		}, 1000, function() {
			//删除第一个柱子
			Transaction_Pillars.Remove_("first");
			//回归原位
			$(".container-Game").css("left", "0px");
			//柱子恢复至最左侧
			Transaction_Pillars.AlterLeft(0);
			//柱子恢复角度,柱子恢复高度
			Transaction_Pillars.ResetPillars();
			//人物恢复原位
			Transaction_Man.Initialize();
			//柱子恢复角度,柱子恢复高度
			Transaction_Pillars.ResetPillars();
			//创建新柱子
			ZzLen = randomNum(350, 500);
			Transaction_Pillars.Add_(ZzLen, InitialWidth);
			InitialWidth -= randomNum(1, 20);
			IsTrue_Down = true;
			IsTrue_Up = true;
		});
    }

    
});

//上传分数事务
function UpScore(NowScore) {
    var UserToken = $.cookie("UserToken");
    if (UserToken != null) {
        //判断是否需要上传：高于本地最高分数
        var Score = $.cookie("UserMaxScore");
        if (NowScore > parseInt(Score) || Score == null) {
            $.post("Game_", {
                Method: "UpScore",
                Score: NowScore,
                UserToken: UserToken
            }, function (Result) {
                if (Result.State == 1) {
                    layer.msg('分数上传成功!', { icon: 1 });
                    $.cookie("UserMaxScore", NowScore.toString());
                    RefreshUses();
                } else {
                    layer.msg('错误: ' + Result.Msg, { icon: 5 });
                }
            });
        } else {
            layer.msg('您未超越自己的最高分~继续努力!!', { icon:4 });
        }
    } else {
        layer.msg('请先前往首页登录再上传分数！', { icon: 5 });
    }
}
function RefreshUses() {
    $.post("Game_", {
        Method: "QueryScoreInfo"
    }, function (Result) {
        UsersScore.items = [];
        $.each(Result, function (i, field) {
            UsersScore.add(
                {
                    UserName: field.UserInfo.Us_NickName,
                    Score: field.ScoreInfo.Game_Score
                });
        });
    });
}

//绑定数据
var UsersScore = new Vue({
    el: '#app',
    data: {
        items: [{
            UserName: "默认显示1",
            Score: "80"
        }, {
            UserName: "默认显示2",
            Score: "70"
        }, {
            UserName: "默认显示3",
            Score: "60"
        }, {
            UserName: "默认显示4",
            Score: "50"
        }, {
            UserName: "默认显示5",
            Score: "40"
        }, {
            UserName: "默认显示6",
            Score: "30"
        },
        {
            UserName: "默认显示7",
            Score: "20"
        },
        {
            UserName: "默认显示8",
            Score: "10"
        },
        {
            UserName: "默认显示9",
            Score: "8"
        },
        {
            UserName: "默认显示10",
            Score: "1"
        },]
    },
    methods: {
        add(Data) {
            this.items.push({
                UserName: Data.UserName,
                Score: Data.Score
            })
        }
    }
});

//初始化
function Initialize() {
	Seep = 4000; //棍子变速
	NowMark = 0; //当前分数
	Deviation = 10; //允许偏移
	InitialWidth = 120; //初始柱子宽度
	Transaction_Pillars.Remove_("all"); //删除全部柱子
	Transaction_Pillars.Reset_(InitialWidth); //重新生成柱子
	Transaction_Pillars.ResetPillars(); //柱子恢复角度,柱子恢复高
	$(".container-Game").css("left", "0px"); //地面恢复
	Transaction_Man.Initialize(); //人物重置
	IsTrue_Down = true; //是否可以按下
	IsTrue_Up = true;
}



//柱子棍子类
var Transaction_Pillars = {
	//获取柱子宽度
	Getwidth_stick: function() {
		return parseInt($(".stick").css("width"));
	},
	//获取棍子宽度
	Getwidth_pillars: function() {
		return parseInt($(".well").css("width"));
	},
	//获取柱子左距离
	GetLeft: function(Index) {
		var len = 0;
		switch (Index) {
			case "first":
				len = parseInt($(".well:first").css("left"));
				break;
			case "last":
				len = parseInt($(".well:last").css("left"));
				break;
			default:
				len = 0;
		}
		return len;
	},
	//增加柱子
	Add_: function(left, width) {
		var PutDiv = '<div class="well"></div>'
		$(".well-box").append(PutDiv);
		$(".well-box div:last").css({
			"left": left + "px",
			"width": width + "px"
		});
	},
	//重置柱子
	Reset_: function(InitialWidth) {
		this.Remove_("all");
		this.Add_(0, InitialWidth);
		this.Add_(400, InitialWidth - randomNum(1, 20));
	},
	//中断棍子且倒下
	Interrupt: function() {
		this.StopAnimate();
		this.AlterRotate(0);
	},
	//删除全部柱子
	Remove_: function(Index) {
		switch (Index) {
			case "first":
				$(".well:first").remove();
				break;
			case "last":
				$(".well:last").remove();
				break;
			case "all":
				$(".well").remove();
		}
	},
	AlterLeft: function(Left) {
		$(".well").css("left", Left + "px");
	},

	//修改棍子角度
	AlterRotate: function(Angle) {
		$(".stick").css({
			"transform": "rotate(" + Angle + "deg)"
		});
	},
	//修改棍子长度
	AlterWidth: function(width) {
		$(".stick").css({
			"width": width + "px"
		});
	},
	//棍子恢复角度,棍子恢复高度
	ResetPillars: function() {
		this.AlterRotate(-90);
		this.AlterWidth(0);
	},
	//棍子开始动画
	Animate: function(Seep) {
		$(".stick").animate({
			width: '1000px'
		}, Seep);
	},
	//棍子停止动画
	StopAnimate: function() {
		$(".stick").stop();
	}
}



//人物类
var Transaction_Man = {
	//人物掉落
	Drop: function(CallBack) {
		$(".man").animate({
			"bottom": "-50%"
		}, 500, CallBack);
	},
	//播放奔跑动画
	PlayRun: function() {
        $("#imgId").attr('src', "../Content/img/stick.gif");
	},
	//暂停播放
	PlayStop: function() {
        $("#imgId").attr('src', "../Content/img/stick_stand.png");
	},
	//恢复人物
	Initialize: function() {
		$(".man").css("left", "0px");
		$(".man").css("bottom", "100%");
		this.PlayStop();
	},
	//播放动画
	Run: function(length, Delay, CallBack) {
		$(".man").animate({
			left: length
		}, Delay, CallBack); //停止动作;
	},
	//开始奔跑
	Go: function(length, Delay, CallBack) {
		this.PlayRun();
		this.Run(length, Delay, CallBack);
	}

}


//生成从minNum到maxNum的随机数
function randomNum(minNum, maxNum) {
	switch (arguments.length) {
		case 1:
			return parseInt(Math.random() * minNum + 1, 10);
			break;
		case 2:
			return parseInt(Math.random() * (maxNum - minNum + 1) + minNum, 10);
			break;
		default:
			return 0;
			break;
	}
}
