


$(document).ready(function () {
    load_users()

});

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
                let btn_delete = '<button type="button" class="btn btn-danger btn_delete btn-sm"  value_nd="' + e.MaND + '" >Delete</button> '
                let btn_detail = '<td> <button type="button" class="btn btn-primary btn-sm btn_detail "  data-toggle="modal" data-target="#form_user_information" value_nd="' + e.MaND + '" >Detail</button> '  + btn_delete + '</td>'
                $("#tbl_users").append("<tr>" + i + username + btn_detail + "</tr>")
                
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

function detail(mand) {
    $.ajax({
        type: "GET",
        url: "../api/lay_thong_tin_user",
        data: {
            Mand: mand
        },
        success: function (data) {
            UserId = data.nguoidung.MaND
            AccId = data.taikhoan.MaTK
            if (data.taikhoan.Quyen == "115")
                $("#select_quyen").val("115").change();
            else if (data.taikhoan.Quyen == "114")
                $("#select_quyen").val("114").change();
            else if (data.taikhoan.Quyen == "113")
                $("#select_quyen").val("113").change();
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



function save() {
    if (Kiemtradulieu() != false) {
        console.log(UserId + AccId)
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
            Quyen: $("#select_quyen").val()
        },
        success: function (data) {
            if (data.msg == "error") {
                swal("Lỗi ", "Đã có lỗi xảy ra", "error");
            } else {
                load_users()
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