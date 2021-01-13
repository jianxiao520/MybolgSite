// 网上拷贝的小火箭js
$(function () {
    var e = $("#rocket-to-top"),
        t = $(document).scrollTop(),
        n,
        r,
        i = !0;
    $(window).scroll(function () {
        var t = $(document).scrollTop();
        t == 0 ? e.css("background-position") == "0px 0px" ? e.fadeOut("slow") : i && (i = !1, $(".level-2").css(
            "opacity", 1), e.delay(100).animate({
                marginTop: "-1000px"
            },
                "normal",
                function () {
                    e.css({
                        "margin-top": "-125px",
                        display: "none"
                    }),
                        i = !0
                })) : e.fadeIn("slow")
    }),
        e.hover(function () {
            $(".level-2").stop(!0).animate({
                opacity: 1
            })
        },
            function () {
                $(".level-2").stop(!0).animate({
                    opacity: 0
                })
            }),
        $(".level-3").click(function () {
            function t() {
                var t = e.css("background-position");
                if (e.css("display") == "none" || i == 0) {
                    clearInterval(n),
                        e.css("background-position", "0px 0px");
                    return
                }
                switch (t) {
                    case "0px 0px":
                        e.css("background-position", "-298px 0px");
                        break;
                    case "-298px 0px":
                        e.css("background-position", "-447px 0px");
                        break;
                    case "-447px 0px":
                        e.css("background-position", "-596px 0px");
                        break;
                    case "-596px 0px":
                        e.css("background-position", "-745px 0px");
                        break;
                    case "-745px 0px":
                        e.css("background-position", "-298px 0px");
                }
            }
            if (!i) return;
            n = setInterval(t, 50),
                $("html,body").animate({
                    scrollTop: 0
                }, "slow");
        });

    $("#nav li").hover(function () {
        $(this).addClass("current");
    });
    $("#nav li").mouseleave(function () {
        $(this).removeClass("current");
    });


});
function OutLogin() {
    layer.confirm('您确定要退出本博客嘛？', {
        btn: ['非常坚定', '还是算了8~'] //按钮
    }, function () {
        SendData = "Type=OutLogin&Token=" + $.cookie('Token') + "&UsId=" + $.cookie('UsId');
        $.removeCookie('UsId', { path: '/' });
        $.removeCookie('Token', { path: '/' });
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../RearStage/Login.ashx",
            async: false,
            data: SendData,
            success: function (data) {
            }
        });
        location.href = "../Front/index.aspx";
        return;
    }, function () {
    });
}