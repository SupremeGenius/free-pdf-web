/// <reference path="jquery.js" />

function tabToggle() {
    $('.mostview_tab').click(function () {
        $('.mostview_tab').addClass('tab_active');
        $('.mostrate_tab').removeClass('tab_active');
        $('.mostrate_content').hide();
        $('.mostview_content').show();
    });

    $('.mostrate_tab').click(function () {
        $('.mostrate_tab').addClass('tab_active');
        $('.mostview_tab').removeClass('tab_active');
        $('.mostview_content').hide();
        $('.mostrate_content').show();
    });

    $('.random_tab').click(function () {
        $('.random_tab').addClass('tab_active');
        $('.mostdownload_tab').removeClass('tab_active');
        $('.mostdownload_content').hide();
        $('.random_content').show();
    });

    $('.mostdownload_tab').click(function () {
        $('.mostdownload_tab').addClass('tab_active');
        $('.random_tab').removeClass('tab_active');
        $('.random_content').hide();
        $('.mostdownload_content').show();
    });
}

function LoginEvents() {    
    var username = $('#txtUsername').val();
    var password = $('#txtPassword').val();
    var flag = false;    

    if (username.length <= 5) {
        $('#fUserLogin ul.errormsg').show().html("<li>Tài khoản tối thiểu 5 kí tự</li>");
        flag = true;
    }

    if (password.length <= 8) {
        $('#fUserLogin ul.errormsg').show().html("<li>Mật khẩu tối thiểu 8 kí tự</li>");
        flag = true;
    }

    if (username.length <= 5 && password.length <= 8) {
        $('#fUserLogin ul.errormsg').show().html("<li>Tài khoản tối thiểu 5 kí tự</li><li>Mật khẩu tối thiểu 8 kí tự</li>");
        flag = true;
    }

    if (flag)
        return;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{ 'Username' : '" + username + "', 'Password' : '" + password + "' }",
        contentType: "application/json; charset=utf-8",
        url: "/WebServices/UserProcessSv.asmx/IsUserAvaliable",
        success: function (data) {
            if (data.d) {
                hideLink();
                showUserMenuForm();
            }
            else {
                alert("Tài khoản hoặc mật khẩu không đúng");
            }
        },
        error: function () {
            alert("Có lỗi trong quá trình xử lí");
        }
    });
}

function LogoutEvents() {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "/WebServices/UserProcessSv.asmx/UserLogout",
        success: function (data) {
            if (data.d) {
                //showLink();
                //showUserLoginForm();
                $('#fUserLogin ul.errormsg').html('');
                window.location.href = "default.aspx";
            }
        },
        error: function () {
            alert("Có lỗi trong quá trình xử lí");
        }
    });    
}