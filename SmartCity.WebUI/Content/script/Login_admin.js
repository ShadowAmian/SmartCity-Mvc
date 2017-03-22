//单击重新改变验证码<a href="WebHelper/Vcode.ashx">WebHelper/Vcode.ashx</a>
function ClickRemoveChangeCode() {
    var img = document.getElementById("imgCode");
    img.src = "../../Vcode.ashx" + '?' + Math.random();
}

var dig = {
    Success: function (result) {
        if (result.Status == "y") {
            ClickRemoveChangeCode();
            window.location.href = result.ReUrl;
        }
        else {
            ClickRemoveChangeCode();
            layer.msg('登录失败，请稍后重试！', { icon: 2, time: 2000 });
        }
    },
    Failure: function () {
        ClickRemoveChangeCode();
        layer.msg('登录失败，请稍后重试！', { icon: 2, time: 2000 });
    },
    ErrorMsg: function (msg) {
        ClickRemoveChangeCode();
        layer.msg('登录异常，请稍后重试！', { icon: 2, time: 2000 });
    }
}

