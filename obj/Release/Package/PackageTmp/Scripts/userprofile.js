$(document).ready(function () {

    

});

var UserId = ""
var AccId = ""
var Permission=""

function clear_input() {
  
    $("#txt_fullname").val("")
    $("#datepicker").val("")
    $("#txt_phone").val("")
    $("#txt_address").val("")
    $("#txt_email").val("")
    $("#txt_username").val("")
    $("#txt_password").val("")

    
}

function Kiemtradulieu() {
    if ($("#txt_username").val() == "") {
        return false;
    }
    if ($("#txt_email").val() == "") {
        return false;
    }
    if ($("#txt_password").val() == "") {
        return false;
    }
    if ($("txt_fullname").val() == "") {
        return false;
    }

    if ($("#datepicker").val() == "") {
        return false;
    }

}

function detail(mand) {
    clear_input()
    $.ajax({
        type: "GET",
        url: "../api/lay_thong_tin_user",
        data: {
            Mand: mand
        },
        success: function (data) {
            UserId = data.nguoidung.MaND
            AccId = data.taikhoan.MaTK
            Permission = data.taikhoan.Quyen
            if (data.taikhoan.Quyen == "115")
                $("#115").attr("selected", "")
            else if (data.taikhoan.Quyen == "114")
                $("#114").attr("selected", "")
            else if (data.taikhoan.Quyen == "113")
                $("#113").attr("selected", "")

            $("#txt_fullname").val(data.nguoidung.HoTen)
            $("#datepicker").val((data.nguoidung.NgaySinh).substr(0, 10))
            $("#txt_phone").val(data.nguoidung.Sdt)
            $("#txt_address").val(data.nguoidung.DiaChi)
            $("#txt_email").val(data.taikhoan.Email)
            $("#txt_username").val(data.taikhoan.TenTK)
            $("#txt_password").val(data.taikhoan.MatKhau)
        },
        error: function (error) {
            swal("Lỗi ", "Đã có lỗi xảy ra !", "error");
        }
    });
}

function save() {
    if (Kiemtradulieu() != false) {
        Save_Users()

    } else {
        swal("Lỗi ", "Dữ liệu bị thiếu", "error");
    }
}

function Save_Acc() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../api/chinh_sua_tai_khoan",
        data: {
            MaTK: AccId,
            TenTK: $("#txt_username").val(),
            Email: $("#txt_email").val(),
            MatKhau: $("#txt_password").val(),
            Quyen: Permission
        },
        success: function (data) {
            if (data.msg == "error") {
                swal("Lỗi ", "Đã có lỗi xảy ra", "error");
            } else {
                clear_input()
                swal("Thông báo", "Thay đổi thông tin thành công \n Hãy đăng nhập lại! ", "success").then((value) => {
                    if (Permission == "115") window.location = "/Users/Login"
                    else window.location = "/AdminManage/Login"
                });
            }

        }
    })
}

function Save_Users(Mand) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../api/chinh_sua_thong_tin_user",
        data: {
            MaND: UserId,
            HoTen: $("#txt_fullname").val(),
            NgaySinh: $("#datepicker").val(),
            Sdt: $("#txt_phone").val(),
            DiaChi: $("#txt_address").val(),
            MaTK: ""
        },
        success: function (data) {
            if (data.msg == "ok") {
                Save_Acc()
            } else
                swal("Lỗi ", "Đã có lỗi xảy ra", "error");
        }
    })
}