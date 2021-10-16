$(document).ready(function () {
});

function Kiemtradulieu() {
    if ($("#username").val() == "") {
        return false;
    }
    if ($("#email").val() == "") {
        return false;
    }
    if ($("#password").val() == "") {
        return false;
    }
    if ($("fullname").val() == "") {
        return false;
    }

    if ($("#datepicker").val() == "") {
        return false;
    }

}

function Dangky() {
    if (Kiemtradulieu() != false) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../api/dang_ky_user",
            data: {
                MaTK: "",
                TenTK: $("#username").val(),
                Email: $("#email").val(),
                MatKhau: $("#password").val(),
                Quyen: "115"
            },
            success: function (data) {
                if (data.msg == "error") {
                    swal("Lỗi ", "Đã có lỗi xảy ra", "error");
                } else if (data.msg == "user_exist") {
                    swal("Lỗi ", "Người dùng đã tồn tại", "error");
                } else {
                    Xacnhanthongtin(data.msg)
                }
            }
        })
    } else {
        swal("Lỗi ", "Dữ liệu bị thiếu", "error");
    } 
}

function Xacnhanthongtin(Mand) {
    
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../api/chinh_sua_thong_tin_user",
            data: {
                MaND: Mand,
                HoTen: $("fullname").val(),
                NgaySinh: $("#datepicker").val(),
                Sdt: $("#phone").val(),
                DiaChi: $("#address").val(),
                MaTK: ""
            },
            success: function (data) {
                if (data.msg == "ok") {
                    swal("", "Đăng ký thành công!", "success");
                } else
                    swal("Lỗi ", "Đã có lỗi xảy ra", "error");
            }
        })
    }