


$(document).ready(function () {
    $("#page2").hide();
    get_chapter()
    get_courses()
    get_comment()
    get_cauhoi()

    window.setInterval(function () {
        get_comment()
        var elem = document.getElementById('binhluan');
        elem.scrollTop = elem.scrollHeight;
       
    }, 5000);

    $("#cauhoi").on('shown', function () {
        alert("I want this to appear after the modal has opened!");
    });
    
    video = videojs('my-video', options, function onPlayerReady() {
        this.on('ended', function () {
            swal("Bạn đã hoàn thành bài học!", "Trả lời câu hỏi bên dưới để sang bài học tiếp theo.", "success").then((value) => {
                done = true
            });;
        });
    });
})
var options = {};
var video 
var url_string = window.location.href
var url = new URL(url_string);
var mabh = url.searchParams.get("Chapter");
var makh = url.searchParams.get("Courses")
var done = false
var done_ch = false
var cauhoi = []
var cauhoiid=0
var index = 0;
var time = 0

var diem = 0;

function chuyencau(btn) {
    if (index == cauhoi.length - 1) {
        swal({
            title: "Good job!",
            text: "Bạn đã hoàn thành bài tập với số điểm là : " + diem.toFixed(),
            icon: "success",
            button: "Ok!",
        });
    }
    if (index >= 0 && index < cauhoi.length) {
        if ($(btn).attr("hihi") == "truoc") {
            index--
        } else index++
        
        set_cauhoi(index)
    }

}

function set_cauhoi(i) {
    time = 0
    $("#options").children().remove()
    $("#text_ch").text((i + 1) + "/" + cauhoi.length + " : " + cauhoi[i].CauHoi.NoiDungCauHoi)
    cauhoi[i].DapAns.forEach(e => {
        $("#options").append('<label class="options" hihi="' + e.DapAnDung+'" >' + e.NoiDungDapAn+'<input type="radio" name="radio"> <span class="checkmark"></span> </label>')
    })

    var diemmoicau = 100 / cauhoi.length
    $(".options").click(function () {
       
        if ($(this).attr("hihi") == 'true') {
            if (time == 0) {
                $(this).append('<i class="fas fa-check"></i>')
                time++
                diem += diemmoicau
            }
                
        }else {
            if (time == 0) {
                $(this).append('<i class="fas fa-times"></i>')
                time++
            }
        }
        
    })

}

function chuyen(btn) {
    if ($(btn).attr("chon") == "lt") {
        $("#page2").hide();
        $("#page1").show();
    }
    else {
        $("#page1").hide();
        $("#page2").show();
        if (done == true) {
            $("#options").show()
            $("#prev").show()
            $("#next").show()
            
            set_cauhoi(0)
        } else {
            $("#text_ch").text("Bạn chưa xem hết video kìa, xem lại đi!")
            $("#options").hide()
            $("#next").hide()
            $("#prev").hide()
        }
    } 
}


function get_cauhoi() {
    $.get("../api/ds_cauhoi?mabh=" + mabh).done(function (data) {
        if (data.msg != "error") {
            cauhoi = data.msg
        }
    })
}

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
    if (data.Video != null) {
        video.src({ type: 'video/webm', src: data.Video });
        video.src({ type: 'video/mp4', src: data.Video });
    }
    hljs.highlightAll();
}