$(document).ready(function () {
   
});

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

