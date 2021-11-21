

$(document).ready(function () {


    get_chapter()
    CKEDITOR.replace('editor', {
        width: "100%",
        height: "400px",
        uiColor: '#F5F5F5',
        allowedContent: true,
        extraPlugins: 'codesnippet',
        codeSnippet_languages: {}
    });
    get_cauhoi()
    $('#select_theloai').change(function () {
        cate = $(this).val()
        console.log(cate)
    })
    $("#table_cauhoi").on("dbl-click-row.bs.table", function (field, value, row, $el) {
        flagsave = false
        selected_cauhoi = value.CauHoi.MaCauHoi
        $('#modal-cauhoi').modal('show');
    });


});

var flagsave = true
var cate = "TN"
var selected_cauhoi

function SaveOrUpdate_cauhoi() {
    if (flagsave == true) {
        add_kiemtra()
    } else
        update_cauhoi()
}

function update_cauhoi() {
    $.post("../api/chinhsuacauhoi", {
        MaCauHoi: selected_cauhoi,
        NoiDungCauHoi: $("#txt_cauhoi").val(),
        TheLoai: cate
    }).done(function (res) {
        if (res.msg != "error") {
            swal("Thông báo", "Lưu câu hỏi thành công", "success").then((value) => {
                clear_modal()
                get_cauhoi()
            });
        } else
            swal("Lỗi ", "Đã có lỗi xảy ra !" + res.msg, "error");

    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}




function dapan_view(index, row) {
    var html = []
    row.DapAns.forEach(e => {
        console.log("2")
        var cheked
        if (e.DapAnDung == true)
            cheked = 'checked'
        else cheked = ''
        html.push(
            '<div class="input-group mb-3">' +
            '<div class="input-group-text">' +
            '<input class="form-check-input mt-0"  ma_da="' + e.MaDapAn + '"onclick="change_da(this)" type="checkbox" ' + cheked + ' aria-label="Checkbox for following text input">' +
            '</div>' +
            '<input type="text" class="form-control"  ma_da="' + e.MaDapAn + '" value="' + e.NoiDungDapAn + '" onchange="change_da(this)" aria-label="Text input with checkbox">' +
            '<button class="btn btn-danger delete_da" ma_da="' + e.MaDapAn + '" onclick="del_da(this)" title="">' +
            '<i class="fas fa-trash-alt"></i>' +
            '</button>' +
            '</div>')
    })

    return html.join('')
}

function del_da(e) {
    console.log()
    $.get("../api/xoadapan?madapan=" + $(e).attr("ma_da")).done(function (res) {
        if (res.msg != "error") {
            clear_modal()
            get_cauhoi()
        }
    })
}

function get_cauhoi() {

    $.get("../api/ds_cauhoi?mabh=" + get_id()).then(function (res) {
        if (res.msg != null) {
            $("#table_cauhoi").bootstrapTable('removeAll')
            $("#table_cauhoi").bootstrapTable('append', res.msg)
        }
    })
}

function change_da(e) {
    if ($(e).attr('type') == 'text') {
        console.log($(e).parent)
        $.post("../api/chinhsuadapan", {
            MaDapAn: $(e).attr("ma_da"),
            NoiDungDapAn: $(e).val(),
        }).then(function () {
            clear_modal()
            get_cauhoi()
        })
    } else {
        var checked = false
        if ($(e).is(':checked'))
            checked = true
        $.post("../api/chinhsuadapan", {
            MaDapAn: $(e).attr("ma_da"),
            DapAnDung: checked
        }).then(function () {
            clear_modal();
            get_cauhoi()
        })
    }
}

function btn_cauhoi(value, row, index) {
    return [
        '<button class="btn btn-success add"  href="javascript:void(0)" title="">',
        '<i class="fas fa-plus-circle"></i>',
        '</button>  ',
        '<button class="btn btn-danger delete"  href="javascript:void(0)" title="">',
        '<i class="fas fa-trash-alt"></i>',
        '</button>  ',

    ].join('')
}

function custom_btn() {
    return {
        btn_themkh: {
            html: '<button type="button" name="btn_themkh" data-toggle="modal" data-target="#modal-cauhoi" class="btn btn-success"><i class="fa fas fa-plus"></i></button>',
            event: () => {
            }

        }
    }
}

function update_chapter() {
    var data = CryptoJS.AES.encrypt(CKEDITOR.instances.editor.getData(), get_id()).toString();
    $.post("../api/chinhsuabaihoc", {
        MaBaiHoc: get_id(),
        Video: $("#txt_url").val(),
        GioiThieu: $("#txt_tenbh").val(),
        LyThuyet: data,
        CodeMau: $("#codemau").val()
    }).done(function (data) {
        if (data.msg != "error")
            swal("Thông báo", "Lưu khóa học thành công", "success").then((value) => {
                window.location = "/AdminManage/ManageCourse"
            });
        else swal("Lỗi ", "Đã có lỗi xảy ra !" + res.msg, "error");
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}


function get_id() {
    var url_string = window.location.href
    var url = new URL(url_string);
    var param = url.searchParams.get("ChapID");
    return param
}

function reset() {
    $.get("../api/chitietbaihoc?mabh=" + get_id()).then(function (res) {
        if (res.msg != "error") {
            $("#my-video source").attr("src", res.msg.Video)
            $("#txt_url").val(res.msg.Video)
        }
        $('#my-video')[0].load()
        $('#my-video')[0].play()
    })
}

function check_video() {
    $("#my-video source").attr("src", $("#txt_url").val())
    $('#my-video')[0].load()
    $('#my-video')[0].play()
}

function clear_modal() {
    $('#modal-cauhoi').modal('hide');
    $("#txt_cauhoi").val("")
    $("option").removeAttr("selected")
    selected_cauhoi = null
    flagsave = true
}

function add_cauhoi(makt) {
    $.post("../api/themcauhoi", {
        NoiDungCauHoi: $("#txt_cauhoi").val(),
        TheLoai: cate,
        MaKT: makt
    }).done(function (res) {
        if (res.msg != "error") {
            swal("Thông báo", "Lưu câu hỏi thành công", "success").then((value) => {
                clear_modal()
                get_cauhoi()
            });
        } else
            swal("Lỗi ", "Đã có lỗi xảy ra !" + res.msg, "error");
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });



}
function get_chapter() {

    $.get("../api/chitietbaihoc?mabh=" + get_id()).then(function (res) {
        if (res.msg != "error") {

            $("#my-video source").attr("src", res.msg.Video)
            $("#txt_url").val(res.msg.Video)
            $("#txt_tenbh").val(res.msg.GioiThieu)
            $("#codemau").val(res.msg.CodeMau)
            var html = CryptoJS.AES.decrypt(res.msg.LyThuyet, get_id()).toString(CryptoJS.enc.Utf8)
            CKEDITOR.instances['editor'].setData(html)
        }
        $('#my-video')[0].load()

    })
}

function add_kiemtra() {
    $.get("../api/themkiemtra?mabh=" + get_id()).done(function (res) {
        if (res.msg != "error") {

            add_cauhoi(res.MaKT)
        }
        else
            wal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}

function add_da(mach) {
    $.post("../api/themdapan", {
        NoiDungDapAn: "Đáp án mới",
        DapAnDung: false,
        MaCauHoi: mach
    }).then(function () {
        get_cauhoi()
    })
}

function del_cauhoi(mach) {
    $.get("../api/xoacauhoi?mach=" + mach).done(function (res) {
        if (res.msg != "error") {
            get_cauhoi()
        } else
            wal("Lỗi ", "Đã có lỗi xảy ra !" + data.msg, "error");
    }).fail(function (data) {
        swal("Lỗi ", "Đã có lỗi xảy ra !" + data.error, "error");
    });
}



window.cauhoi_event = {

    'click .add': function (e, value, row, index) {
        add_da(row.CauHoi.MaCauHoi)
    },
    'click .delete': function (e, value, row, index) {
        del_cauhoi(row.CauHoi.MaCauHoi)
    },
}