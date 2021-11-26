$(document).ready(function () {
    get_bhluu()
});


function card(tenkh, tenbh,ngayluu,kh,bh) {
    var html =
        '<div class="card col-lg-3 py-3 m-5">'+
        '<div class="card-header" style="background: url(https://www.gstatic.com/classroom/themes/img_learnlanguage.jpg) center center; color: #fff;">' +
        '<h5 style="font-weight: 600; text-transform: uppercase;"><a href="../Manage/Learn?Courses=' + kh +'&Chapter='+bh+'" style="color: #fff;">' + tenkh + '</a></h5>' +
        '<p class="card-text">' +
        tenbh +
        '</p>'+
        '</div>'+
        '<div class="card-body">'+
        '<p class="card-title">Ngày lưu</p>'+
        '<p class="card-text">' +
        new Date(Date.parse(ngayluu)).toLocaleDateString("vi-VN")  +
        '</p>'+
        '</div>'+
        '<div class="card-footer">'+
        '<div class="icon_right d-flex float-right">'+
        '<a href="#"><i class="far fa-trash-alt m-2"  mabh="' + bh +'" onclick="huyluu(this)" style="font-size: 20px; color: black;"></i></a>'+
        '</div>'+
        '</div>'+
        '</div >'
    return html
}


function get_bhluu() {
    $(".row").children().remove()
    $.get("../api/ds_baihocluu_user?userid=" + userid).done(function (data) {
        if (data.msg != "error") {
            data.msg.forEach(e => {
                $.get("../api/khbybh?mabh=" + e.MaBaiHoc).done(function (res) {
                    if (res.msg != "error")
                        $(".row").append(card(res.tenkh, res.tenbh, e.NgayLuu, res.makh, e.MaBaiHoc))
                })
            })
           
           
        }
    })
}


function huyluu(btn) {
    $.get("../api/huyluubaihoc?userid=" + userid + "&mabh=" + $(btn).attr("mabh")).done(function (data) {
        if(data.msg!="error")
            get_bhluu()
    })
}