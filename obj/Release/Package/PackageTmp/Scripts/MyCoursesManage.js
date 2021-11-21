$(document).ready(function () {
    get_couses()
   
})



function get_couses() {
    $.get("../api/ds_khoahoc_user?userid=" + userid).done(function (data) {
        if (data.msg != "error") {
            init_couses(data.msg)
        }
    })
}

function card(data) {

    var makh = data.MaKH
    var html = '<div class="card mb-3 data-container">' +
        '<div class="row ">' +
        '<div class="col-md-5" data-mdb-ripple-color="light">' +
        ' <img style=" height:180px " src="' + data.HinhAnh + '" class="img-fluid" />' +
        '<a href="#!">' +
        ' <div class="mask" style="background-color: rgba(251, 251, 251, 0.15)"></div>' +
        '</a>' +
        '</div>' +
        '<div class="col-md-7">' +
        '<div class="header mb-4"></div>' +
        '<div class="body mb-3">' +
        '<h5 class="title"  style="margin-top:15px">' + data.TenKH + '</h5>' +
        '<a  type="button" class="btn btn-outline-dark " onclick="learn(this)" makh="' + makh + '" data-mdb-ripple-color="dark">Bắt đầu học</a>' +
        '</div>' +
        '<div class="footer">' +
        '<i class="fas fa-eye"></i>' +
        '<span>' + getRandomInt(1, 100) + data.LuotDK + ' Lượt xem </span>' +
        '<i class="fas fa-users"></i>' +
        '<span>' + data.LuotDK + ' Học viên</span>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>'
    
    
    return html
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min) + min);
}

function learn(btn) {
    $.get("../api/ds_baihoc_kh?makh=" + $(btn).attr("makh")).done(function (data) {
        if (data.msg != "error" && data.msg != []) {
            window.location = '../Manage/Learn?Courses=' + $(btn).attr("makh") + '&Chapter=' + data.msg[0].MaBaiHoc
        }
    })
}

function init_couses(arr) {
    $("#list_couses").pagination({
        dataSource: arr,
        pageSize: 3,

        callback: function (data, pagination) {
            $("#list_couses").children(".card").remove()
            data.forEach(e => {
                $.get("../api/chitietkhoahoc?makh=" + e.MaKH).done(function (res) {
                    $("#list_couses").append(card(res.msg))
                })
            })
            
            $('.paginationjs').addClass("paginationjs-big paginationjs-theme-blue")
            $('.paginationjs').appendTo($("#list_couses"))

        }
    })
}