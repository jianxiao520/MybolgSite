var Ws_Link;
var Is_Hover = false;
$(function () {
    Ws_Link = new WebSocket("ws://" + window.location.hostname + ":" + window.location.port + "/api/ChatRoom?Method=Connect");
    Ws_Link.onopen = function () {
        Add_System('已经成功连接至聊天服务器');
    };
    //接收到服务器消息处理
    Ws_Link.onmessage = function (evt) {
        var status = evt.data.split("|")[0];
        var Text = evt.data.split("|")[1];
        //数据类型分类
        switch (status) {
            case "0":
                $.cookie('ChatRoomToken', Text, { expires: 7, path: '/' });
               // $.cookie("ChatRoomToken", Text);
                if ($.cookie('UserToken') == null) {
                    //未登录
                    $(".Send-Msg").addClass("disable");
                    $(".Send-Msg").css("opacity", "0.1");
                    $(".NotLogin").css("display", "block");
                    $(".Send-Msg-Tourists").css("display", "block");
                }
                RefreshUser(Text);
                break;
            case "1":
                Add_System(Text);
                RefreshUser($.cookie('ChatRoomToken'));
                break;
            case "2":
                Add_Msg_Other(Text, evt.data.split("|")[2]);
                break;
            default:
                Add_System("收到未知类型数据");
        }
    };
});

//绑定数据
var UsersInfo = new Vue({
    el: '#app',
    data: {
        NickName: "读取中...",
        Experience: "读取中...",
        ImagePath: "../UserHeadPortrait/Connection_Fails.jpg",
        items: [{
            // UserId: "0",
            NickName: "读取中...",
            Experience: "读取中...",
            ImagePath: "../UserHeadPortrait/Connection_Fails.jpg"
        }, {
            // UserId: "1",
            NickName: "读取中...",
            Experience: "读取中...",
                ImagePath: "../UserHeadPortrait/Connection_Fails.jpg"
        },
        ]
    },
    methods: {
        add(Data) {
            this.items.push({
                // UserId: Data.UserId,
                NickName: Data.NickName,
                Experience: Data.Experience,
                ImagePath: Data.ImagePath
            });
        }
    }
});

//刷新用户数据
function RefreshUser(UserId) {
    //Get请求修改
    $.get("../api/ChatRoom?Method=GetUserInfo", function (result) {
        if (result.State != 0) {
            UsersInfo.NickName = result.Msg.MyInfo[0].NickName == null ? "游客" : result.Msg.MyInfo[0].NickName;
            UsersInfo.Experience = result.Msg.MyInfo[0].Experience;
            UsersInfo.ImagePath = "../UserHeadPortrait/" + result.Msg.MyInfo[0].Head_Portrait + ".jpg";
            UsersInfo.items = [];
            $.each(result.Msg.UsersInfo, function (i, field) {
                UsersInfo.add(
                    {
                        NickName: field.NickName,
                        Experience: field.Experience,
                        ImagePath: "../UserHeadPortrait/" + field.Head_Portrait + ".jpg"
                    });
            });
        }
    });
}


//修改用户数据
function ModifyUserInfo(UserId, NewNickName, CallBack) {
    $.get("ModifyUserInfo.ashx?Type=NickName&UserId=" + UserId + "&NewNickName=" + NewNickName, function (result) {
        switch (result) {
            case "1":
                layer.msg('修改成功');
                break;
            case "0":
                layer.msg('提交参数错误');
                break;
            case "-1":
                layer.msg('用户UserId不存在');
                break;
            default:
                layer.msg('未知错误');
        }

        CallBack(UserId);
    });
}



//自身发送消息出去
function Add_Msg_My(HeadPortrait) {
    Content = $("[name='Send-T']").val();
    Element = $(".Message-Box");
    Information.Message_Add_Self(Content, UsersInfo.ImagePath, Element);
    $("[name='Send-T']").val("");
    if (Ws_Link.readyState == WebSocket.OPEN) {
        Ws_Link.send("2|" + Content);
    } else {
        Add_System('连接已经关闭');
    }
    
}

//系统发送消息
function Add_System(Content) {
    Element = $(".Message-Box");
    Information.Message_Add_System(Content, Element);
}

//接收到消息
function Add_Msg_Other(UserName, Content) {
    Element = $(".Message-Box");

    $.each(UsersInfo.items, function (i, item) {
        // console.log(item.NickName)
        if (item.NickName == UserName) {
            HeadPortrait = item.ImagePath;
            return false
        }
    });
    Information.Message_Add_From(UserName, Content, HeadPortrait, Element);
}

//消息插入到div
var Information = {
    Message_Add_Self: function (Content, HeadPortrait, Element) {
        if (isEmpty(Content)) return;
        var Addstyle =
            '<div class="Box-Receiver MessageB"><img src="{0}" class="Portrait"/><div class="UserName Right-box"><a href="">我</a></div><div class="Message-Content Right-box" style="background-color:#9EEA6A;"><div class="Left-arrow"></div><span class="Content">{1}</span></div></div>'
                .format(HeadPortrait, Content);
        $(Element).append(Addstyle);
        $(Element).scrollTop($(Element)[0].scrollHeight);
    },
    Message_Add_From: function (User, Content, HeadPortrait, Element) {
        var Addstyle =
            '<div class="Box-Sender MessageB"><img src="{0}" class="Portrait"/><div class="UserName Left-box"><a href="">{1}</a></div><div class="Message-Content Left-box"><div class="Right-arrow"></div><span class="Content">{2}</span></div></div>'
                .format(HeadPortrait, User, Content);
        $(Element).append(Addstyle);
        $(Element).scrollTop($(Element)[0].scrollHeight);
    },
    Message_Add_System: function (Content, Element) {
        var Addstyle =
            '<div style="text-align: center;margin: 20px 0 20px 0;"><span class="Message-System">{0}</span></div>'
                .format(Content);
        $(Element).append(Addstyle);
        $(Element).scrollTop($(Element)[0].scrollHeight);
    },
    Message_Add_UserName: function (UserName, Element) {
        $(Element).html(UserName);
    }
}

//是否为空(包括空字符串、空格、null,{})
function isEmpty(strings){
        if ((strings + '').replace(/(^\s*)|(\s*$)/g, '').length === 0) { //已修正bug[当strings为数字时，会报strings.replace is not a function]
            return true;
        }
    // 不为空返回false
    return false;
}

//格式化文本拓展方法
String.prototype.format = function () {
    var regexp = /\{(\d+)\}/g;
    var args = arguments;
    var result = this.replace(regexp, function (m, i, o, n) {
        return args[i];
    });
    return result.replaceAll('%', String.EscapeChar);
}

//离开修改框，执行修改
$(".MyslefUserName").blur(function () {
    ModifyUserInfo($.cookie('UserId'), this.innerHTML, RefreshUses);
});





//消息块悬停效果
$(".Message-Content").hover(function () {
    $(this).css("background-color", "#F6F6F6");
    $(this).children(".Left-arrow").css("color", "#F6F6F6");
    $(this).children(".Right-arrow").css("color", "#F6F6F6");
}, function () {
    $(this).css("background-color", "#ffffff");
    $(this).children(".Left-arrow").css("color", "#ffffff");
    $(this).children(".Right-arrow").css("color", "#ffffff");
});

// 绑定回车事件
$("[name='Send-T']").bind("keypress",
    function (event) {
        if (event.keyCode == "13") {
            Add_Msg_My();
            event.preventDefault();
        }
    }
);

// 绑定点击发送事件
$(".Button_Send").bind("click", function () {
    Add_Msg_My();
});


//绑定修改名字样式
$(".MyslefUserName").bind(
    "keypress",
    function (event) {
        if (event.keyCode == "13") {
            event.preventDefault();
        }
    }
);
//悬停离开弹出菜单1JQ
$('.Pop-Box-Memu li:nth-child(1)').mousemove(function () {
    $(".OnLineUsers").css({
        "opacity": "1",
        "display": "block",

    });
    $(".Pop-Box").css({
        "left": $(".L_Memu").width()
    });
});
$('.Pop-Box-Memu li:nth-child(1)').mouseleave(function () {
    $(".OnLineUsers").css({
        "opacity": "0",
        "display": "none"
    });
});

//悬停离开弹出菜单2JQ
$('.Pop-Box-Memu li:nth-child(2)').mousemove(function () {
    $(".UserInfo").css({
        "opacity": "1",
        "display": "block"
    });
    $(".Pop-Box").css({
        "left": $(".L_Memu").width()
    });
});
$('.Pop-Box-Memu li:nth-child(2)').mouseleave(function () {
    $(".UserInfo").css({
        "opacity": "0",
        "display": "none"
    });
});

//弹出框悬停JQ
$(".Pop-Box").hover(function () {
    $(this).css({
        "opacity": "1",
        "display": "block"
    })
}, function () {
    $(this).css({
        "opacity": "0",
        "display": "none"
    })
});
