
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
        '<button class="btn btn-info detail" href="javascript:void(0)" title="">',
        '<i class="fas fa-list-alt"></i>',
        '</button>  ',
        '<button class="btn btn-danger delete" href="javascript:void(0)" title="">',
        '<i class="fas fa-trash-alt"></i>',
        '</button>  '
    ].join('')
}

window.get_baihoc = {
    'click .detail': function (e, value, row, index) {
        $("#chaptermodal").modal("show")
    },

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
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
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
                });
            else swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
        })
            .fail(function (data) {
                swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
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
                    });
                else swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
            })
            .fail(function (data) {
                swal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
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
