/// <reference path="jquery.js" />

var tmpForm_UserLogin = $('#fUserLogin').detach();
var tmpForm_UserMenu = $('#fUserMenu').detach();

function clearForm(form) {
    $(':input', form).each(function () {
        var type = this.type;
        var tagName = this.tagName.toLowerCase();

        if (type == 'text' || type == 'password' || tagName == 'textarea')
            this.value = "";
    });
};

function deleteConfirm(url) {
    var result = confirm("Bạn có chắc muốn xóa bản ghi này?");

    if (result)
        window.location.href = url;    
}

function showUserLoginForm() {
    tmpForm_UserLogin.insertAfter($('#sidebar').children().get(1));
    tmpForm_UserMenu = $('#fUserMenu').detach();
}

function showUserMenuForm() {
    tmpForm_UserMenu.insertAfter($('#sidebar').children().get(1));
    tmpForm_UserLogin = $('#fUserLogin').detach();
}

function hideLink() {
    $('#navbar a').each(function () {
        var link = $(this).attr('href');
        if ((link.indexOf('login.aspx') != -1) || (link.indexOf('register.aspx') != -1)) {
            $(this).parent().addClass('hide');
        }
    });
}

function showLink() {
    $('#navbar a').each(function () {
        var link = $(this).attr('href');
        if (link.indexOf('login.aspx') != -1 || link.indexOf('register.aspx') != 1) {
            $(this).parent().addClass('show');
        }
    });
}

function isLogin() {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "/WebServices/UserProcessSv.asmx/IsUserLogin",
        success: function (data) {
            if (data.d) {
                showUserMenuForm();
                hideLink();
            }
            else {
                showUserLoginForm();
                //showLink();
            }
        },
        error: function () {
            alert("Có lỗi trong quá trình xử lí");
        }
    });
}