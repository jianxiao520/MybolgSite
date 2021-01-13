$(document).ready(function () {
    $.post("GetUserInfo_", "", function (Data) {
        if (Data != "您未登录!") {
            UserData.Modify(Data);
        } else {
            parent.layer.alert("请您先登录！", {
                title: '响应消息'
            });
            console.log("请您先登录！");
        }
    });
});
var UserData = new Vue({
    el: "#app",
    data: {
        Us_ProfilePhoto: "../Content/img/MyImg2.jpg"
    },
    methods: {
        Modify: function (Data) {
            this.Us_ProfilePhoto = "../UserHeadPortrait/" + Data.Us_ProfilePhoto + ".jpg";
            parent.$("body div.Touxiang > p").html(Data.Us_NickName)
        }
    }
});

function showFilename(file) {
    $("#filename_label").html(file.name);
    var imgFile = new FileReader();
    imgFile.readAsDataURL(file);
    imgFile.onload = function () {
        var imgData = this.result; //base64数据  
        $('#ShowTouxiang').attr('src', imgData);
    }
}

$("#SendSave").click(function () {
    var formData = new FormData();
    formData.append('file', $('#inputGroupFile04')[0].files[0]);  //添加图片信息的参数
    formData.append('sizeid', 123);  //添加其他参数
    var ImgUrl = "";
    $.ajax({
        url: 'UpHeadPortrait_',
        type: 'POST',
        cache: false, //上传文件不需要缓存
        data: formData,
        async: false,
        processData: false, // 告诉jQuery不要去处理发送的数据
        contentType: false, // 告诉jQuery不要去设置Content-Type请求头
        success: function (data) {
            //parent.layer.alert("上传头像成功", {
            //    title: '响应消息'
            //});
            console.log(data.ImgUrl);
            ImgUrl = data.ImgUrl;
        },
        error: function (data) {
            console.log("上传失败");
        }
    });
    SendData = $("#senddata").serializeArray();
    SendData.push({ name: "Img_url", value: ImgUrl });
    SendData.push({ name: "UserToken", value: $.cookie('UserToken') });
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "ModifUserInfo_",
        async: false,
        data: SendData,
        success: function (data) {
            if (data["State"] == "1") {
                parent.layer.alert(data["Msg"], {
                    title: '响应消息'
                });
            } else {
                parent.layer.alert(data["Msg"], {
                    title: '响应消息'
                });
            }
        }
    });
});