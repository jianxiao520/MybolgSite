var index;
$(document).ready(function () {
    index = layer.load();
    RefreshUses();
});

//绑定数据 
var CommentInfo = new Vue({
    el: '#app',
    data: {
        items: [{
            Content: "加载中...",
            NickName: "读取中...",
            Like_Count: "读取中...",
            ImagePath: "img/MyImg1.jpg",
            SendDate: "1970-01-01",
            Comment_Id:"1"
        }
        ]
    },
    methods: {
        add(Data) {
            this.items.push({
                Content: Data.Content,
                NickName: Data.NickName,
                Like_Count: Data.Like_Count,
                ImagePath: Data.ImagePath,
                SendDate: ProcessDate(Data.SendDate),
                Comment_Id: Data.Comment_Id
            })
            layer.close(index);
        }
    }
});

//评论
$("#formDemo").click(function () {
    $.post("Comment_", {
        Method: "AddComment",
        UserToken: $.cookie("UserToken"),
        Content: $("[name=desc]").val()
    }, function (Result) {
        if (Result.State == 1) { location.reload(); }
        else { layer.msg(Result.Msg); }
        
    });
});

//刷新评论
function RefreshUses() {
    $.post("Comment_", {
        Method :"QueryComment"
    }, function (Result) {
        CommentInfo.items = [];
        $.each(Result, function (i, field) {
            CommentInfo.add(
                {
                    Content: field.Comment.Comment_Content,
                    NickName: field.Author.Us_NickName,
                    ImagePath: "../UserHeadPortrait/" + field.Author.Us_ProfilePhoto+".jpg",
                    Like_Count: field.Comment.Comment_Like_Count,
                    SendDate: field.Comment.Comment_Date,
                    Comment_Id: field.Comment.Comment_Id
                });
        });
    });
}

//转换时间戳
function ProcessDate(DateData) {
    var timestamp = DateData;
    var date = new Date(parseInt(timestamp.replace("/Date(", "").replace(")/", ""), 10));
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = (date.getDate() < 10 ? '0' + (date.getDate()) : date.getDate()) + ' ';
    var NewDtime = Y + M + D;
    return NewDtime;
}


//点赞
function AddNum(obj) {
    var $P = $(obj).next("p");
    $.post("Comment_", {
        Method: "UpVote",
        UserToken: $.cookie("UserToken"),
        Comment_Id: $P.attr("value")
    }, function (Result) {
        if (Result.State == 1) {
            //前端添加数量
            $P.html((parseInt($P.html()) + 1) + "赞");
            layer.msg('点赞成功^_^!~');
        } else {
            layer.msg('点赞失败o(╥﹏╥)o');
        }
    });
}