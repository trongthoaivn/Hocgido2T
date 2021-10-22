


$(document).ready(function () {
    load_users()

    $('select').on('change', function (e) {
        var item = $(this).find("option:selected")
        permission = item.val()
    });

});

var permission
var UserId = ""
var AccId =""

function load_users() {
    $('#table_users tbody tr').remove();
    $.ajax({
        type: "GET",
        url: "../api/ds_nguoidung",
        success: function (data) {
            $(data.msg).each(function (index, e) {
                index++
                let i = '<td><p>' + index + '</p></td>'
                let username = '<td><p >' + e.HoTen + '</p></td>'
                let Sdt = '<td><p >' + e.Sdt + '</p></td>'
                let btn_delete = '<button type="button" class="btn btn-danger btn_delete btn-sm"  value_nd="' + e.MaND + '" >Xóa</button> '
                let btn_detail = '<td style="text-align:center"> <button type="button" class="btn btn-primary btn-sm btn_detail "  data-toggle="modal" data-target="#form_user_information" value_nd="' + e.MaND + '" >Chi tiết</button> ' + btn_delete + '</td>'
                $("#tbl_users").append('<tr >' + i + username + Sdt + btn_detail + '</tr>')
                $("#count").text("  "+index)
            })
            $(".btn_detail").on("click", function () {
                detail($(this).attr("value_nd"))
            })
            
            $(".btn_delete").on("click", function () {
                swal({
                    title: "Cảnh báo!",
                    text: "Bạn chắc chắn muốn xóa người dùng này?",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                }).then((willDelete) => {
                        if (willDelete) {
                             ecrypt($(this).attr("value_nd"))
                        } 
                    });
               
            })
        },
        error: function (error) {
            swal("Lỗi ", "Đã có lỗi xảy ra !", "error");
        }
    });
}




function delete_us(mand) {
   
    $.ajax({
        type: "GET",
        url: "../api/xoa_nguoidung",
        data: {
            Mand: mand
        },
        success: function (data) {
            if (data.msg == "ok") {
                swal("Thông báo", "Xóa người dùng thành công", "success").then((value) => {
                    load_users()
                });

            }else swal("Lỗi ", "Đã có lỗi xảy ra !", "error");
        },
        error: function (error) {
            swal("Lỗi ", "Đã có lỗi xảy ra !" + error, "error");
        }
    })


  
    load_users()
}


function ecrypt(str) {
    
    $.ajax({
        type: "GET",
        url: "../api/Encrypt",
        data: {
            msg: str
        },
        success: function (data) {
            delete_us(data.msg)
        },
        error: function (error) {
        }
    })
}

function clear_input() {
    $("option").removeAttr("selected")
    $("#txt_fullname").val("")
    $("#datepicker").val("")
    $("#txt_phone").val("")
    $("#txt_address").val("")
    $("#txt_email").val("")
    $("#txt_username").val("")
    $("#txt_password").val("")
    permission=null
    $("#txt_fullname1").val("")
    $("#datepicker1").val("")
    $("#txt_phone1").val("")
    $("#txt_address1").val("")
    $("#txt_email1").val("")
    $("#txt_username1").val("")
    $("#txt_password1").val("")
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
            permission = data.taikhoan.Quyen
            if (data.taikhoan.Quyen == "115")
                $("#115").attr("selected","")
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

function Kiemtradulieu1() {
    if ($("#txt_username1").val() == "") {
        return false;
    }
    if ($("#txt_email1").val() == "") {
        return false;
    }
    if ($("#txt_password1").val() == "") {
        return false;
    }
    if ($("txt_fullname1").val() == "") {
        return false;
    }

    if ($("#datepicker1").val() == "") {
        return false;
    }
}

function Dangky() {

    if (Kiemtradulieu1() != false) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../api/dang_ky_user",
            data: {
                MaTK: "",
                TenTK: $("#txt_username1").val(),
                Email: $("#txt_email1").val(),
                MatKhau: $("#txt_password1").val(),
                Quyen: permission
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
            HoTen: $("#txt_fullname1").val(),
            NgaySinh: $("#datepicker1").val(),
            Sdt: $("#txt_phone1").val(),
            DiaChi: $("#txt_address1").val(),
            MaTK: ""
        },
        success: function (data) {
            if (data.msg == "ok") {
                swal("Thông báo", "Đăng ký thành công!", "success").then((value) => {
                    load_users()
                    clear_input()
                });

            } else
                swal("Lỗi ", "Đã có lỗi xảy ra", "error");
        }
    })
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
            Quyen: permission
        },
        success: function (data) {
            if (data.msg == "error") {
                swal("Lỗi ", "Đã có lỗi xảy ra", "error");
            } else {
                load_users()
                clear_input()
                swal("Thông báo", "Thay đổi thông tin thành công ", "success")
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