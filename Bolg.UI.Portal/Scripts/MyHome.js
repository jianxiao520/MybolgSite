$(document).ready(function () {
    $(".app-Menu ul li").removeClass("active");
    $(".app-Menu ul li:nth-child(1)").addClass("active");
    $("iframe").attr("src", "/UserInfo/AlterProfilePhoto");
});


//往下滑动效果Jq
$("#Change").click(function () {
    $(".Content").css("opacity", "1");
    $("html,body").animate({ scrollTop: $('.site-footer').offset().top }, 2000);
});



//左侧菜单悬停效果
$(".app-Menu ul li").hover(function () {
    $(this).css("background-color", "#29272a21");
}, function () {
    $(this).css("background-color", "#fff");
});

//菜单选择
$(".app-Menu ul li:nth-child(1)").click(function () {
    $(".app-Menu ul li").removeClass("active");
    $(this).addClass("active");
    $("iframe").attr("src", "/UserInfo/AlterProfilePhoto");
});
$(".app-Menu ul li:nth-child(2)").click(function () {
    $(".app-Menu ul li").removeClass("active");
    $(this).addClass("active");
    $("iframe").attr("src", "/UserInfo/UserInfo");
});
$(".app-Menu ul li:nth-child(3)").click(function () {
    $(".app-Menu ul li").removeClass("active");
    $(this).addClass("active");
    $("iframe").attr("src", "/UserInfo/MyWealth");
});





