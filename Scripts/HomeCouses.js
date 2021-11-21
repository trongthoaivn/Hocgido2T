$(document).ready(function () {
    get_couses()
})



function get_couses() {
    $.get("../api/ds_khoahoc").done(function (data) {
        if (data.msg != "error") {
            init_couses(data.msg)
        }
    })
}

function card(data) {

    var html = '<div class="card mb-3 data-container" makh="' + data.MaKH + '" mota="' + data.MoTaKH + '">' +
        '<div class="row ">'+
        '<div class="col-md-5" data-mdb-ripple-color="light">' +
        ' <img style=" height:180px " src="' + data.HinhAnh + '" class="img-fluid" />' +
        '<a href="#!">'+
        ' <div class="mask" style="background-color: rgba(251, 251, 251, 0.15)"></div>'+
        '</a>'+
        '</div>'+
        '<div class="col-md-7">'+
        '<div class="header mb-4"></div>'+
        '<div class="body mb-3">' +
        '<h5 class="title">' + data.TenKH + '</h5>' +
        '<a href="../Users/Login" type="button" class="btn btn-outline-dark" data-mdb-ripple-color="dark">Bắt đầu học</a>'+
        '</div>'+
        '<div class="footer">'+
        '<i class="fas fa-eye"></i>' +
        '<span>' + getRandomInt(1,100) + data.LuotDK + ' Lượt xem </span>' +
        '<i class="fas fa-users"></i>' +
        '<span>'+data.LuotDK+' Học viên</span>' +
        '</div>'+
        '</div>'+
        '</div>'+
        '</div>'
    return html
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min) + min); 
}

function init_couses(arr) {
    $("#list_couses").pagination({
        dataSource: arr,
        pageSize: 1,
       
        callback: function (data, pagination) {
            $("#list_couses").children(".card").remove()
            data.forEach(e => {
                $("#list_couses").append(card(e))
            })
            $('.paginationjs').addClass("paginationjs-big paginationjs-theme-blue")
            $('.paginationjs').appendTo($("#list_couses"))
          
        }
     })
}