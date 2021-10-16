$(document).ready(function () {
    
});
import * as CryptoJS from '~/Scripts/crypto-js.min.js';
function Dangnhap() {
    if (Kiemtradulieu() != false) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../api/dang_nhap",
            data: {
                MaTK: "",
                TenTK: $("#username").val(),
                MatKhau: $("#password").val(),
                Email: ""
            },
            success: function (data) {
                if (data.msg == "ok") {
                    sessionStorage.setItem("userID", data.tk)
                    setCookie("userID", CryptoJS.MD5(data.tk), 1);
                    window.location = "/Manage/Index"
                } else if (data.msg == "user_not_exist")
                    swal("Lỗi ", "Người dùng không tồn tại", "error");
                else
                    swal("Lỗi ", "Đã có lỗi xảy ra", "error");
            }
        })
    } else {
        swal("Lỗi ", "Dữ liệu bị thiếu", "error");
    }
}

function Kiemtradulieu() {
    if ($("#username").val() == "") {
        return false;
    }
    
    if ($("#password").val() == "") {
        return false;
    }
}

function setCookie(cName, cValue, expDays) {
    let date = new Date();
    date.setTime(date.getTime() + (expDays * 24 * 60 * 60 * 1000));
    const expires = "expires=" + date.toUTCString();
    document.cookie = cName + "=" + cValue + "; " + expires + "; path=/";
}