


$(document).ready(function () {
    get_chapter()
    get_courses()
    get_comment()
    window.setInterval(function () {
        get_comment()
        var elem = document.getElementById('binhluan');
        elem.scrollTop = elem.scrollHeight;
    }, 5000);
})

var url_string = window.location.href
var url = new URL(url_string);
var mabh = url.searchParams.get("Chapter");
var makh = url.searchParams.get("Courses")

function get_chapter() {
    $.get("../api/chitietbaihoc?mabh=" + mabh).done(function (data) {
        if (data.msg != "error") {
            init_chapter(data.msg)
        }
    })
}

function get_courses() {
    $.get("../api/ds_baihoc_kh?makh=" + makh).done(function (data) {
        if (data.msg != "error") {
            var index = 1
            data.msg.forEach(e => {
                $("#danhsachbh").append('<a href="../Manage/Learn?Courses=' + makh + '&Chapter=' + e.MaBaiHoc+'" type="button" class="list-group-item list-group-item-action">Bài ' + index + ' ' + e.GioiThieu + '</a>')
                index++
            })
           
        }
    })
}

function comment(nguoidung, noidung, ngayBL) {

    var html =
        '<br />' +
        '<div class="media">'+
            '<div class="media-left">'+
                '<img src="/images/owl.png" alt="" class="header__img" />'+
            '</div>'+
            '<div style="margin-left:15px" class="comment-replacement">'+
        '<div class="comment-heading text-muted">' +
        '<strong class="comment-author-name">' + nguoidung + '</strong>' +
        '<span> đã bình luận </span>' +
        '<strong class="format-time">' + new Date(Date.parse(ngayBL)).toLocaleDateString("vi-VN") +' - '+ new Date(Date.parse(ngayBL)).toLocaleTimeString("vi-VN") + '</strong>' +
                '</div>'+
        '<div class="comment-body">' +
        '<strong>' + noidung + '</strong>' +
                '</div>'+
            '</div>'+
        '</div>'
        
    return html
}

function get_comment() {
    $.get("../api/ds_binhluan_bh?mabh=" + mabh).done(function (data) {
        $("#binhluan").children().remove()
        if (data.msg != "error") {
            data.msg.forEach(e => {
                $("#binhluan").append(comment(e.NguoiDung, e.NoiDung, e.NgayBL))
            })
           
        }
    })
}

function comment_post() {
    $.post("../api/thembinhluan", {
        NoiDung: $("#comment_text").val(),
        MaND: userid,
        MaBaiHoc: mabh,
    }).done(function (data) {
        if (data.msg != "error") {
            get_comment()
            
            $("#comment_text").val("")
        }
    })
}

function init_chapter(data) {
    $(".tenkh").text(data.GioiThieu)
    var html = CryptoJS.AES.decrypt(data.LyThuyet, mabh).toString(CryptoJS.enc.Utf8)
    $("#lythuyet").append(html)
    $("#codemau").append(data.CodeMau)
    hljs.highlightAll();
}