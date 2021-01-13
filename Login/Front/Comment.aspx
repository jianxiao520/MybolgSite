<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="Login.Front.Comment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<script src="js/jquery-3.3.1.js"></script>
<link href="layui/css/layui.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="css/Comment.css" />
<script src="js/layer/layer.js" type="text/javascript" charset="utf-8"></script>
<body>
    <div class="Comment">
        <li>
            <div class="D_Comment">
                <div class="TouXiang">
                    <img src="img/touxiang.jpg">
                </div>
                <h1 class="NickName">Jianxiao</h1>
                <p class="Content">哈哈哈哈哈哈，这里的留言真有意思~</p>
                <div class="ion-likeList"><a href="#"></a></div>
                <p class="Add">2212赞</p>
            </div>
        </li>
        <li>
            <div class="D_Comment">
                <div class="TouXiang">
                    <img src="img/touxiang1.jpg">
                </div>
                <h1 class="NickName">WoaiNi</h1>
                <p class="Content">楼上的真搞笑</p>
                <div class="ion-likeList"><a href="#"></a></div>
                <p class="Add">332赞</p>
            </div>
        </li>
        <li>
            <div class="D_Comment">
                <div class="TouXiang">
                    <img src="img/touxiang2.jpg">
                </div>
                <h1 class="NickName">Mr.P</h1>
                <p class="Content">鸡你太美啦哈哈哈哈哈</p>
                <div class="ion-likeList"><a href="#"></a></div>
                <p class="Add">23赞</p>
            </div>
        </li>
        <li>
            <div class="D_Comment">
                <div class="TouXiang">
                    <img src="img/touxiang3.jpg">
                </div>
                <h1 class="NickName">Mr.s</h1>
                <p class="Content">我觉得可以</p>
                <div class="ion-likeList"><a href="#"></a></div>
                <p class="Add">13赞</p>
            </div>
        </li>
        <li>
            <div class="D_Comment">
                <div class="TouXiang">
                    <img src="img/touxiang4.jpg">
                </div>
                <h1 class="NickName">Mr.s</h1>
                <p class="Content">璞，哈哈哈哈哈哈哈</p>
                <div class="ion-likeList"><a href="#"></a></div>
                <p class="Add">3赞</p>
            </div>
        </li>
    </div>
    <form id="form1" runat="server">
        <div class="Send">
            <div class="layui-input-block">
                <textarea name="desc" placeholder="请输入内容" class="layui-textarea Input"></textarea>
                <button class="layui-btn Tij" lay-submit lay-filter="formDemo">立即提交</button>
            </div>
        </div>

    </form>
    <footer class="site-footer">
        Copyright © 2020 by Edwin Today's share . All rights reserved. | Design By Huangzexu.<br>
        津ICP备XXXXXXXX号-1
    </footer>
</body>
</html>
