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
    $("#datepicker").datepicker({
        locale: 'zh-cn',
        format: 'yyyy-mm-dd hh:mm:ss',
        weekStartDay: 1
    });
    $(".gj-icon").remove();
});

var UserData = new Vue({
    el: "#app",
    data: {
        NickName: "读取中...",
        Eamil: "读取中...",
        Phone: "读取中...",
        Birth: "读取中...",
        Us_RegisterTime: "读取中...",
        Us_Ip: "读取中..."
    },
    methods: {
        Modify: function (Data) {
            this.NickName = Data.Us_NickName;
            this.Eamil = Data.Us_Eamil;
            this.Phone = Data.Us_Phone;
            $("#datepicker").val(ProcessDate(Data.Us_Birthday));
            this.Us_RegisterTime = ProcessDate(Data.Us_RegisterTime);
            this.Us_Ip = Data.Us_Ip;
            //选中性别
            $("select[name=cars]").val(Data.Us_Sex);
            //改变父窗口内容
            parent.$("body div.Touxiang > p").html(Data.Us_NickName)
        }
    }
});

$("#SendSave").click(function () {
    SendData = $("#senddata").serializeArray();
    SendData.push({ name: "UserToken", value: $.cookie('UserToken') });
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/UserInfo/ModifUserInfo_",
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
//Date时间戳数据转换成时间
function ProcessDate(DateData) {
    if (DateData == null) {
        return ""
    }
    var timestamp = DateData;
    var date = new Date(parseInt(timestamp.replace("/Date(", "").replace(")/", ""), 10));
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = (date.getDate() < 10 ? '0' + (date.getDate()) : date.getDate()) + ' ';
    h = (date.getHours() < 10 ? '0' + (date.getHours()) : date.getHours()) + ':';
    m = (date.getMinutes() < 10 ? '0' + (date.getMinutes()) : date.getMinutes()) + ':';
    s = (date.getSeconds() < 10 ? '0' + (date.getSeconds()) : date.getSeconds());
    var NewDtime = Y + M + D + h + m + s;
    return NewDtime;
}


//判断用户名
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
//判断电子邮箱
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
//判断手机号
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
//判断出生日期
function CSRQonblus() {
    var BirthDate = $('input[name="Birth"]').val();
    if (BirthDate == "") {
        $('#W_Birth').text("请选择正确的出生日期");
    }
    else {
        $('#W_Birth').text("√");
    }
}