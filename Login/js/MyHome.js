$(document).ready(function (){
	$("#Change").click(function(){
		$(".Content").css("opacity","1");

        $("html,body").animate({scrollTop:$('.site-footer').offset().top}, 2000);
	});


    $("#SendSave").click(function () {
        var formData = new FormData();
        formData.append('file', $('#inputGroupFile04')[0].files[0]);  //添加图片信息的参数
        formData.append('sizeid', 123);  //添加其他参数

        $.ajax({
            url: 'UpHeadPortrait.ashx',
            type: 'POST',
            cache: false, //上传文件不需要缓存
            data: formData,
            processData: false, // 告诉jQuery不要去处理发送的数据
            contentType: false, // 告诉jQuery不要去设置Content-Type请求头
            success: function (data) {
                var rs = eval("(" + data + ")");
                console.log(rs.ImgUrl);
            },
            error: function (data) {
                console.log("上传失败");
            }
        });
	});





});

function YHMonblus(){
	var NickName=$('input[name="NickName"]').val();
	if(NickName==""){
		$('#W_NickName').text("请输入用户名");
	}
	else if(NickName < 3 ||NickName > 18){
		console.log(NickName);
		$('#W_NickName').text("格式错误,长度应为3-18个字符");
	}
	else {
		$('#W_NickName').text("√");
	}
}

function DZYXonblus(){
	var email=$('input[name="Eamil"]').val();
	var re= /[a-zA-Z0-9]{1,10}@[a-zA-Z0-9]{1,5}\.[a-zA-Z0-9]{1,5}/;
	if(email==""){
		$('#W_Eamil').text("请输入电子邮箱");
	}
	else if(!re.test(email)){
		$('#W_Eamil').text("邮箱格式不正确");
	}
	else {
		$('#W_Eamil').text("√");
	}
}

function LXDHonblus(){
	var phone=$('input[name="Phone"]').val();
	var re = /^1\d{10}$/;
	if(phone==""){
		$('#W_Phone').text("请输入联系电话");
	}
	else if(!re.test(phone)){
		$('#W_Phone').text("电话格式输入错误");
	}
	else {
		$('#W_Phone').text("√");
	}
}
function CSRQonblus(){
	var BirthDate=$('input[name="Birth"]').val();
	if(BirthDate==""){
		$('#W_Birth').text("请选择正确的出生日期");
	}
	else {
		$('#W_Birth').text("√");
	}
}