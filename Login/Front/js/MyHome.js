$(document).ready(function () {
    $("#datepicker").datepicker({
        locale: 'zh-cn',
        format: 'yyyy-mm-dd',
        weekStartDay: 1
    });

    function scrollC() {
        var End = $(".Today_Wallpaper").offset().top + 50;
        var Op = parseFloat(0.3+ parseFloat($(document).scrollTop() * (0.7 / parseFloat(End))));
        console.log(Op);
        if ($(document).scrollTop() >= End) {
            $(".Content").css("opacity", "1.0");
        } else {
            $(".Content").css("opacity", Op);

        }


    }
    window.onscroll= scrollC;


    $("#Change").click(function () {
        if (this.innerHTML == "更改") {
            $(".Content").css("opacity", "1");
            $("html, body").animate({ scrollTop: $(".Today_Wallpaper").offset().top + 50 }, { duration: 1000, easing: "swing" });
        } else {
            layer.alert('你还没登录捏~快去登录！！', {
                skin: 'layui-layer-lan'
                , closeBtn: 0
                , anim: 6 //动画类型
            });
        }
    });


    $("#SendSave").click(function () {
        var formData = new FormData();
        formData.append('file', $('#inputGroupFile04')[0].files[0]);  //添加图片信息的参数
        formData.append('sizeid', 123);  //添加其他参数
        var ImgUrl = "";
        $.ajax({
            url: '../../RearStage/UpHeadPortrait.ashx',
            type: 'POST',
            cache: false, //上传文件不需要缓存
            data: formData,
            async: false,
            processData: false, // 告诉jQuery不要去处理发送的数据
            contentType: false, // 告诉jQuery不要去设置Content-Type请求头
            success: function (data) {
                var rs = eval("(" + data + ")");
                console.log(rs.ImgUrl);
                ImgUrl = rs.ImgUrl;
            },
            error: function (data) {
                console.log("上传失败");
            }
        });
        SendData = $("#senddata").serializeArray();
        SendData.push({ name: "Img_url", value: ImgUrl });
        SendData.push({ name: "UsId", value: $.cookie('UsId') });
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../RearStage/MyHome.ashx",
            async: false,
            data: SendData,
            success: function (data) {
                if (data["State"] == "1") {
                    layer.alert(data["Msg"], {
                        title: '登录消息'
                    });
                } else {
                    layer.alert(data["Msg"], {
                        title: '登录消息'
                    });
                }
            }
        });

    });
});

function YHMonblus() {
    var NickName = $('input[name="NickName"]').val();
    if (NickName == "") {
        $('#W_NickName').text("请输入用户名");
    }
    else if (NickName < 3 || NickName > 18) {
        console.log(NickName);
        $('#W_NickName').text("格式错误,长度应为3-18个字符");
    }
    else {
        $('#W_NickName').text("√");
    }
}

function DZYXonblus() {
    var email = $('input[name="Eamil"]').val();
    var re = /[a-zA-Z0-9]{1,10}@[a-zA-Z0-9]{1,5}\.[a-zA-Z0-9]{1,5}/;
    if (email == "") {
        $('#W_Eamil').text("请输入电子邮箱");
    }
    else if (!re.test(email)) {
        $('#W_Eamil').text("邮箱格式不正确");
    }
    else {
        $('#W_Eamil').text("√");
    }
}

function LXDHonblus() {
    var phone = $('input[name="Phone"]').val();
    var re = /^1\d{10}$/;
    if (phone == "") {
        $('#W_Phone').text("请输入联系电话");
    }
    else if (!re.test(phone)) {
        $('#W_Phone').text("电话格式输入错误");
    }
    else {
        $('#W_Phone').text("√");
    }
}
function CSRQonblus() {
    var BirthDate = $('input[name="Birth"]').val();
    if (BirthDate == "") {
        $('#W_Birth').text("请选择正确的出生日期");
    }
    else {
        $('#W_Birth').text("√");
    }
}

function showFilename(file) {
    $("#filename_label").html(file.name);
    var imgFile = new FileReader();
    imgFile.readAsDataURL(file);
    imgFile.onload = function () {
        var imgData = this.result; //base64数据  
        $('#ShowTouxiang').attr('src', imgData);
    }
} $(document).ready(function () {
    $("#datepicker").datepicker({
        locale: 'zh-cn',
        format: 'yyyy-mm-dd',
        weekStartDay: 1
    });





    $("#SendSave").click(function () {
        var formData = new FormData();
        formData.append('file', $('#inputGroupFile04')[0].files[0]);  //添加图片信息的参数
        formData.append('sizeid', 123);  //添加其他参数
        var ImgUrl = "";
        $.ajax({
            url: '../../RearStage/UpHeadPortrait.ashx',
            type: 'POST',
            cache: false, //上传文件不需要缓存
            data: formData,
            async: false,
            processData: false, // 告诉jQuery不要去处理发送的数据
            contentType: false, // 告诉jQuery不要去设置Content-Type请求头
            success: function (data) {
                var rs = eval("(" + data + ")");
                console.log(rs.ImgUrl);
                ImgUrl = rs.ImgUrl;
            },
            error: function (data) {
                console.log("上传失败");
            }
        });
        SendData = $("#senddata").serializeArray();
        SendData.push({ name: "Img_url", value: ImgUrl });
        SendData.push({ name: "UsId", value: $.cookie('UsId') });
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../RearStage/MyHome.ashx",
            async: false,
            data: SendData,
            success: function (data) {
                if (data["State"] == "1") {
                    layer.alert(data["Msg"], {
                        title: '登录消息'
                    });
                } else {
                    layer.alert(data["Msg"], {
                        title: '登录消息'
                    });
                }
            }
        });

    });
});

function YHMonblus() {
    var NickName = $('input[name="NickName"]').val();
    if (NickName == "") {
        $('#W_NickName').text("请输入用户名");
    }
    else if (NickName < 3 || NickName > 18) {
        console.log(NickName);
        $('#W_NickName').text("格式错误,长度应为3-18个字符");
    }
    else {
        $('#W_NickName').text("√");
    }
}

function DZYXonblus() {
    var email = $('input[name="Eamil"]').val();
    var re = /[a-zA-Z0-9]{1,10}@[a-zA-Z0-9]{1,5}\.[a-zA-Z0-9]{1,5}/;
    if (email == "") {
        $('#W_Eamil').text("请输入电子邮箱");
    }
    else if (!re.test(email)) {
        $('#W_Eamil').text("邮箱格式不正确");
    }
    else {
        $('#W_Eamil').text("√");
    }
}

function LXDHonblus() {
    var phone = $('input[name="Phone"]').val();
    var re = /^1\d{10}$/;
    if (phone == "") {
        $('#W_Phone').text("请输入联系电话");
    }
    else if (!re.test(phone)) {
        $('#W_Phone').text("电话格式输入错误");
    }
    else {
        $('#W_Phone').text("√");
    }
}
function CSRQonblus() {
    var BirthDate = $('input[name="Birth"]').val();
    if (BirthDate == "") {
        $('#W_Birth').text("请选择正确的出生日期");
    }
    else {
        $('#W_Birth').text("√");
    }
}

function showFilename(file) {
    $("#filename_label").html(file.name);
    var imgFile = new FileReader();
    imgFile.readAsDataURL(file);
    imgFile.onload = function () {
        var imgData = this.result; //base64数据  
        $('#ShowTouxiang').attr('src', imgData);
    }
}

