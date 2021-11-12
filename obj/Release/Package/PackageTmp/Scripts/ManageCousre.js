

function checknull() {
    if ($("#txt_tenkh").val() != "" && $("#txt_motakh").val() != "")
        return true
    else {
        swal("Lỗi ", "Đã có lỗi xảy ra !", "error");
        return false
    }
}

function chitietbaihoc(value, row, index) {
    return [
        '<button class="btn btn-info detail"  href="javascript:void(0)" title="">',
        '<i class="fas fa-list-alt"></i>',
        '</button>  ',
        '<button class="btn btn-danger delete"  href="javascript:void(0)" title="">',
        '<i class="fas fa-trash-alt"></i>',
        '</button>  '
    ].join('')
}

function del_course(makh) {
    $.get("../api/xoakhoahoc?makh=" + makh).done(function (res) {
        if (res.msg != "error")
            swal("Thông báo", "Xóa khóa học thành công", "success").then((value) => {
                $("#table_course").bootstrapTable('refresh')
            });
        else
            swal("Lỗi ", "Đã có lỗi xảy ra !" + res.msg, "error");
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}

window.get_baihoc = {
    'click .detail': function (e, value, row, index) {
        $("#chaptermodal").modal("show")
    },
    'click .delete': function (e, value, row, index) {
        del_course(row.MaKH);
        console.log(row.MaKH)
    }
}

function clear_modal() {
    $("#txt_tenkh").val("")
    $("#txt_motakh").val("")
    $("#txt_hinhanh").val("")
    $("#hinhanh").attr("src", "")
}


function set_course(makh) {
    maKH = makh
    $.get("../api/chitietkhoahoc?makh=" + makh).then(function (res) {
        if (res.msg != "error") {
            $("#coursemodal").modal("show")
            $("#txt_tenkh").val(res.msg.TenKH)
            $("#txt_motakh").val(res.msg.MoTaKH)
            $("#txt_hinhanh").val(res.msg.HinhAnh)
            $("#hinhanh").attr("src", $("#txt_hinhanh").val())
        }
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}

function save() {
    if (flagsave) add_course()
    else update_cousrse()
}

function update_cousrse() {
    if (checknull()) {
        $.post("../api/suakhoahoc", {
            MaKH: maKH,
            TenKH: $("#txt_tenkh").val(),
            MoTaKH: $("#txt_motakh").val(),
            HinhAnh: $("#txt_hinhanh").val(),
        }).done(function (data) {
            if (data.msg == "ok")
                swal("Thông báo", "Lưu khóa học thành công", "success").then((value) => {
                    clear_modal()
                    $("#coursemodal").modal("hide")
                    $("#table_course").bootstrapTable('refresh')
                });
            else swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
        })
            .fail(function (data) {
                swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
            });
    }
    $("#table_course").bootstrapTable('refresh')
}

function add_course() {
    if (checknull()) {
        $.post("../api/themkhoahoc", {
            TenKH: $("#txt_tenkh").val(),
            MoTaKH: $("#txt_motakh").val(),
            HinhAnh: $("#txt_hinhanh").val()
        })
            .done(function (data) {
                if (data.msg == "ok")
                    swal("Thông báo", "Thêm khóa học thành công", "success").then((value) => {

                        clear_modal()
                        $("#table_course").bootstrapTable('refresh')
                    });
                else swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
            })
            .fail(function (data) {
                swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
            });
    }
    $("#table_course").bootstrapTable('refresh')
}

function get_course(params) {
    $.get("../api/ds_khoahoc").then(function (res) {
        params.success(res.msg)
    })
}

function custom_btn() {
    return {
        btn_themkh: {
            html: '<button type="button" name="btn_themkh" data-toggle="modal" data-target="#coursemodal" class="btn btn-success"><i class="fa fas fa-plus"></i></button>',
            event: () => {
            }
        }
    }
}


function get_chapter(id) {
    $.get("../api/ds_baihoc_kh?makh=" + id).then(function (res) {
        $("#table_chapter").bootstrapTable('removeAll')
        $("#table_chapter").bootstrapTable('append', res.msg)
        $("#chaptermodal").attr("value", id)
    })
}

function get_chapterinfo(mabh) {

    $.get("../api/chitietbaihoc?mabh="+mabh).then(function (res) {
        if (res.msg != "error") {

            $("#my-video source").attr("src", res.msg.Video)
            $("#titleChaptermodal").text(res.msg.GioiThieu)

          
            $("#LyThuyet").append(CryptoJS.AES.decrypt(res.msg.LyThuyet, mabh).toString(CryptoJS.enc.Utf8))

            $("#chaptermodal").modal("hide")
            
           
        }
        $("#Chapterinfo").modal("show")


        $('#my-video')[0].load()
        $('#my-video')[0].play()
    })
}


function btn_add_baihoc() {
    return {
        btn_add_baihoc: {
            html: '<button type="button" name="btn_add_baihoc"  class="btn btn-success"><i class="fa fas fa-plus"></i></button>',
            event: () => {
                $.post("../api/thembaihoc", {
                    GioiThieu: "Bài học mới",
                    MaKH: $("#chaptermodal").attr("value")

                }).done(function (res) {
                    if (res.msg != "error")
                        get_chapter($("#chaptermodal").attr("value"))
                    else swal("Lỗi ", "Đã có lỗi xảy ra !" + res.msg, "error");
                }).fail(function (res) {
                    swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
                })
            }

        }
    }
}

function xoabaihoc() {
    return [
        '<button class="btn btn-danger delete_chap"  href="javascript:void(0)" title="">',
        '<i class="fas fa-trash-alt"></i>',
        '</button>  '
    ].join('')
}

function del_chap(id) {
    $.get("../api/xoabaihoc?mabh=" + id).done(function (res) {
        if (res.msg != "error")
            swal("Thông báo", "Xóa bài học thành công", "success").then((value) => {
                get_chapter($("#chaptermodal").attr("value"))
            });
        else
            swal("Lỗi ", "Đã có lỗi xảy ra !" + res.msg, "error");
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}

window.chap_event = {
    'click .delete_chap': function (e, value, row, index) {
        del_chap(row.MaBaiHoc)
    },
}

var maKH = ""
var flagsave = true

$(document).ready(function () {

    $("#table_course").on("click-row.bs.table", function (field, value, row, $el) {
        flagsave = false
        get_chapter(value.MaKH)
    });

    $("#table_course").on("dbl-click-row.bs.table", function (field, value, row, $el) {
        flagsave = false
        set_course(value.MaKH)
    });

    $("#txt_hinhanh").change(function () {

        $("#hinhanh").attr("src", $("#txt_hinhanh").val())
    });


    $("#chaptermodal").on("dbl-click-row.bs.table", function (field, value, row, $el) {
        window.location = "/AdminManage/Chapter?ChapID=" + value.MaBaiHoc
    });
});
