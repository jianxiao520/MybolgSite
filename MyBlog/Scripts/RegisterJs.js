var pos = 0;
var totalSlides;
var sliderWidth;
$(document).ready(function (){
	totalSlides = $('.lb-Content ul li').length;
	sliderWidth = $('.lb-Content').width();
    $("#close").click(function () {
        $("#myModal").css("display", "none");
    });
	var autoSlider = setInterval(slideRight,3000);
	$("#Login").click(function(){
		// 变换 转换 盒子内容
		$(".S1").addClass("is-hidden");
		$(".S1").removeClass("Index200");
		$(".S2").addClass("Index200");
		$(".S2").removeClass("is-hidden");
		$("#switch-cnt").addClass("Goto_left");
		$("#switch-cnt").removeClass("Goto_right__switch");
		// 变换 登录&注册 盒子内容
		$(".Loginbox").removeClass("is_hidden Goto_left");
		$(".Registerbox").removeClass("is_show Goto_left");
		$(".Loginbox").addClass("is_show Goto_right__box");
		$(".Registerbox").addClass("is_hidden Goto_right__box");
	});
	$("#Register").click(function(){
		// 变换 转换 盒子内容
		$(".S1").addClass("Index200");
		$(".S1").removeClass("is-hidden");
		$(".S2").removeClass("Index200");
		$(".S2").addClass("is-hidden");
		$("#switch-cnt").addClass("Goto_right__switch");
		$("#switch-cnt").removeClass("Goto_left");
		
		// 变换 登录&注册 盒子内容
		$(".Loginbox").removeClass("is_show Goto_right__box");
		$(".Registerbox").removeClass("is_hidden Goto_right__box");
		$(".Loginbox").addClass("is_hidden Goto_left");
		$(".Registerbox").addClass("is_show Goto_left");
	});
	// 防止双击被选择影响美观
	$('.lb-button').bind("selectstart", function () { return false; });
	
	// 上一张
	$("#left").click(function(){
		slideLeft()
	});
	// 下一张
	$("#right").click(function(){
		slideRight();
	});
	
	$('.lb-box').hover(
	  function(){ $(this).addClass('active'); clearInterval(autoSlider); }, 
	  function(){ $(this).removeClass('active'); autoSlider = setInterval(slideRight, 3000); }
	);
	
});


function slideRight() {
    pos++;
    if (pos == totalSlides) {
        pos = 0;
    }
    $('.slider').css('left', -(sliderWidth * pos));
}
function slideLeft(){
	pos--;
	if(pos==-1){ pos = totalSlides-1; }
	$('.slider').css('left', -(sliderWidth * pos));
	$('.slider').css('left', -(sliderWidth*pos)); 	
}


function ShowAlert(tittle,content) {
    $("#myModal").css("display", "block");
    $("#myModal > div > div.modal-header > h2").html(tittle);//设置标题
    $("#myModal > div > div.modal-body > h3").html(content);//设置内容
}


function SendLogin() {
    layer.msg('登录中', {icon: 16,shade: 0.01});
    if ($("#UserName").val() == "") {
        ShowAlert("警告", "请输入用户名!");
        return;
    }
    if ($("#PassWord").val() == "") {
        ShowAlert("警告", "请输入密码!");
        return;
    }
    SendData = $("#LoginFrom").serializeArray();
    SendData.push({ name: "Type", value: "Login" });
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../Index/Login",
        async: false, 
        data: SendData,

        success: function (data) {  
            var R_Data = eval("(" + data+")");
            if (R_Data["Msg"]["Token"] != undefined) {
                layer.alert("登录成功", {
                    title: '登录消息',
                    yes: function (index, layero) {
                        location.href = "/index";
                    }
                });
                
            } else {
                layer.alert(R_Data["Msg"], {
                    title: '登录消息'
                });
            }
        }
    });
}

function SendRegister() {
    if (!checkusername($("#R_UserName").val())) {
        ShowAlert("警告", "请输入正确的用户名: 用户名在3-15个字符之间.");
        return;
    }
    if ($("#R_PassWord").val() == "") {
        ShowAlert("警告", "请输入密码!");
        return;
    }
    if (!checkPhone($("#Phone").val())) {
        ShowAlert("警告", "请输入正确的手机号码!");
        return;
    }
    SendData = $("#RegisterFrom").serializeArray();
    SendData.push({ name: "Us_Ip", value: returnCitySN["cip"] });
    SendData.push({ name: "Type", value: "Register" });
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../Index/Login",
        async: false,
        data: SendData,
        success: function (data) {
          //  var R_Data = eval("(" + data + ")");
            layer.alert(data[0]["Msg"], {
                title: '注册消息'
            });
        },
        error: function (jqObj) {
            var R_Data = eval("(" + jqObj + ")");
            console.log(R_Data.status)
        }
    });
}



function checkPhone(phone) {
    if (!(/^1[3456789]\d{9}$/.test(phone))) {
        return false;
    }
    return true;
}
function checkusername(username) {
    if (username.match(/<|>|"|\(|\)|'/ig)) {
        return false;
    }
    var unlen = username.replace(/[^\x00-\xff]/g, "**").length;
    if (unlen < 3 || unlen > 15) {
        return false;
    }
    return true;
}