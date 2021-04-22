$(document).ready(function () {
    // 进行初始化
    (function () {
        getSongLrc("324249", function () { });
        getSongInfoList("周杰伦");
    })();

    // 播放按钮点击事件
    $("#player").click(function () {
        if ($("#player").css("background-position") == "0px 0px") {
            // 开始播放	
            Play_Even();
        } else {
            Stop_Even();
        }
    });

    // 收藏按钮点击事件
    $(".ion-likeList").click(function () {
        layer.open({
            type: 2,
            area: ['500px', '700px'],
            fixed: false, //不固定
            maxmin: true,
            content: 'Comment.html',
            title: "留言"
        });
    });

    // windows按钮点击事件
    $(".ion-windows").click(function () {
        var Num = getRndInteger(1, 7);
        $("html").css("background", "url('img/bz" + Num + ".jpg') 0 / cover fixed");
        $("head style").text(".Search-song::before{background-image: url(./img/bz" + Num + ".jpg);}")
    })


    $(".ion-likeList").click(function () {
        $(".ion-likeList a").css("color", "#ff4343");
    });

    // 绑定查询按钮点击事件
    $(".Search-Botton").click(function () {
        let keyword = $(".Search-Input").val();
        getSongInfoList(keyword);
    });

    // 绑定查询回车事件
    $(".Search-Input").keydown(function (e) {
        if (e.keyCode == "13") {
            let keyword = $(".Search-Input").val();
            getSongInfoList(keyword);
        }
    });

    // 绑定歌曲点击播放事件
    $("#song-info-list").on("click", "li", function (e) {
        let musicID = $(this).attr("data-rid");
        let curIndex = $(this).index();

        // 暂停音乐
        if (!$("audio").paused) {
            Stop_Even();
        }

        // 播放音乐
        Play_Music(musicID, curIndex);
    })

    // 动态监听图片加载失败失败事件
    $("img").on("error", function (e) {
        $(this).attr("src", "./img/music_default_bg.png");
    })
});

var ip = "112.74.165.253"; // ip地址
var interval = {};  //进度条_定时器
var GcFunDong = {}; //歌词滚动_定时器
var curSongList = []; //存储歌曲列表
var curSongLrcList = [];  //存储歌词_列表
var curLine = -1;  //当前播放歌词_行

// 播放音乐
function Play_Music(musicID, index) {
    let $myaudio = $("audio");
    let songSourceUrl = "http://" + ip + "/music/songsource?musicid=" + musicID + "&bit=320";
    let loadIndex = 0;
    $.ajax({
        url: songSourceUrl,
        timeout: 10 * 1000,
        beforeSend: function () {
            // 显示 loading
            loadIndex = layer.load(2, {
                offset: ['40%', '28%'],
                shade: [0.1, '#fff']
            });
        },
        success: function (result) {
            if (result.code == 200) {
                // 清除节点
                $myaudio.remove();

                // 新建节点
                let new_audio = document.createElement("audio");
                new_audio.src = result.url;
                new_audio.autoplay = "autoplay";
                new_audio.onloadstart = function () {
                    // 修改歌词和歌曲信息
                    changeDisplayInfo(index);
                    getSongLrc(musicID, function () {
                        setTimeout(function () {
                            $("body").append(new_audio);
                            Play_Even();
                            // 关闭 loading
                            layer.close(loadIndex);
                        }, 200);
                    });
                }


            } else {
                // 关闭 loading
                layer.close(loadIndex);
                // 提示信息
                layer.msg("播放失败", {
                    offset: ['40%', '28%'],
                    time: 2000 //2秒后关闭
                })
            }
        },
        error: function (e) {
            // 关闭 loading
            layer.close(loadIndex);

            // 显示提示信息
            layer.msg("播放失败", {
                offset: ['40%', '28%'],
                time: 2000 //2秒后关闭
            })
        }
    });
}

// 播放事务
function Play_Even() {
    let $myaudio = $('audio')[0];
    $("#player").css("background-position", "-30px 0px");

    // 播放音乐
    if ($myaudio.paused) {
        $myaudio.play();
    }

    // 歌词滚动
    if (curSongLrcList.length > 1) {
        GcFunDong = setInterval(Lrc_Roll, 10);
    }

    // 进度条滚动
    interval = setInterval(GetProgress, 1000);
}

// 歌词滚动
function Lrc_Roll() {
    if (curLine >= curSongLrcList.length - 1) {
        return;
    }

    let currentTime = Math.round($("audio")[0].currentTime * 1000 / 10) / 100;
    let next_time = parseFloat(curSongLrcList[curLine + 1].time);
    // 判断是否为播放 且 播放时长大于1秒(放到服务器需要加载时间)
    if ($("#player").css("background-position") == "-30px 0px" && currentTime >= next_time) {
        curLine++;
        let heigjt = $("#Huad").children().eq(curLine).height() + 10;
        $("#Huad").css("top", $('#Huad').position().top - heigjt);
        $("#Huad").children().eq(curLine).addClass("active").siblings().removeClass("active");
    }
};

// 停止事件
function Stop_Even() {
    let $myaudio = $("audio")[0];
    $("#player").css("background-position", "0px 0px");
    $myaudio.pause();
    clearInterval(interval);//停止
    clearInterval(GcFunDong);//停止
}

// 获取当前播放进度   定时任务
function GetProgress() {
    // 获取媒体播放时间
    let $myaudio = $("audio")[0];
    let Times = Math.floor($myaudio.currentTime);
    let Duration = Math.floor($myaudio.duration);
    Duration = Duration == NaN ? 200 : Duration;
    if (Times < Duration) {
        let Percent = Times / Duration * 100;
        // 转换成百分比
        $("#Jindu").css("width", Percent + "%");
        // 秒->00:00
        $(".player_music__time").html(secondToMin(Times) + " / " + secondToMin(Duration));

    } else {
        // 播放完毕后的恢复
        $("#Jindu").css("width", "0%");
        $("#Huad").css("top", "100px");
        curLine = -1;
        $myaudio.currentTime = 0;
        $(".player_music__time").html("00:00 / " + secondToMin(Duration));
        Stop_Even()
    }
}

// 时间格式转换
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
    return Math.floor(Math.random() * (max - min)) + min;
}

// 获取歌曲列表
function getSongInfoList(keyword) {
    let songInfoApi = "http://" + ip + "/music/songlist?key=" + keyword;
    $.ajax({
        url: songInfoApi,
        timeout: 10 * 1000,
        beforeSend: function () {
            $('#song-info-list').hide();
            $("#loading").show();
        },
        success: function (result) {
            if (result.code == 200) {
                curSongList = result.data.list;

                // 清除歌曲列表
                $('#song-info-list').empty();

                // 重新渲染列表
                for (var i = 0; i < curSongList.length; i++) {
                    var obj = curSongList[i];

                    const query = $('#song-info-list');

                    query.append("<li data-rid='" + obj.rid + "'>\
					<div class='songlist__number'>" + (i + 1) + "</div>\
					<div class='songlist__name'>" + obj.name + "</div>\
					<div class='songlist__author'>" + obj.artist + "</div>\
					<div class='songlist__time'>" + obj.songTimeMinutes + "</div>\
					</li>");
                }

                // 显示列表
                $("#loading").hide();
                $('#song-info-list').show();

            } else {
                $("#loading").hide();
                $('#song-info-list').show();
                layer.msg("查询失败", {
                    offset: ['40%', '28%'],
                    time: 2000
                });
            }
        },
        error: function (e) {
            $("#loading").hide();
            $('#song-info-list').show();
            layer.msg("查询失败", {
                offset: ['40%', '28%'],
                time: 2000
            });
        }
    });
}

// 修改页面歌曲显示信息
function changeDisplayInfo(index) {
    let name = curSongList[index].name;
    let artist = curSongList[index].artist;
    let album = curSongList[index].album == undefined ? null : curSongList[index].album;
    let releaseDate = curSongList[index].releaseDate == undefined ? null : curSongList[index].releaseDate;
    let pic = curSongList[index].pic;

    $(".song_info__name a").text(name);
    $(".song_info__singer a").text(artist);
    $(".song_info__cover img").attr("src", pic);
    $(".player_music__info .music_name").text(name + ' - ' + artist);

    if ((album == null || album == '') && (releaseDate == null || releaseDate == '')) {
        $(".song_info__album a").text("无专辑  & 未知发布时间");
    } else if (album == null || album == '') {
        $(".song_info__album a").text("无专辑 & " + releaseDate);
    } else if (releaseDate == null || releaseDate == '') {
        $(".song_info__album a").text("《" + album + "》 & 未知发布时间");
    } else {
        $(".song_info__album a").text("《" + album + "》 & " + releaseDate);
    }
}

// 获取歌词
function getSongLrc(musicID, callBack) {
    // 歌词请求地址
    let songLrcUrl = "http://" + ip + "/music/songlrc?musicid=" + musicID;

    // 清空歌词列表
    curSongLrcList = [];

    // 重置当前播放歌词_行
    curLine = -1;

    let $lrcList = $("#Huad");

    // 获取歌词信息
    $.ajax({
        url: songLrcUrl,
        timeout: 10 * 1000,
        success: function (result) {

            // 清空列表
            $lrcList.empty();

            // 重置歌词块高度
            $lrcList.css("top", "100px");

            if (result.code == 200) {
                // 渲染歌词列表
                if (result.lrclist == null || result.lrclist.length <= 0) {
                    $lrcList.text("暂时无相关歌曲信息~");
                    return;
                } else {
                    curSongLrcList = result.lrclist;
                }

                for (let i = 0; i < curSongLrcList.length; i++) {
                    let new_p = document.createElement("p");
                    new_p.innerText = curSongLrcList[i].lineLyric;
                    $lrcList.append(new_p);
                }
                callBack();
            } else {
                $lrcList.text("暂时无相关歌曲信息~");
            }
        },
        error: function (e) {
            $lrcList.empty();
            $lrcList.text("暂时无相关歌曲信息~");
        }
    });
}

