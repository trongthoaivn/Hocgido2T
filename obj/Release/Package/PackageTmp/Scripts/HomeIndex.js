
$(document).ready(function () {
    $("#homeli ").addClass("active")
    init()
});
    

function baner_item(tenkh,hinhanh) {
    var item = 
        '<div style="text-align:center" class="col-lg-3 col-md-2 py-3 wow fadeInUp">'+
        '<div class="card-blog">'+
        '<div style="text-align:center" class="header">'+
        '<div class="post-thumb">' +
        '<img style="width:100%" src="' + hinhanh + '" alt="">' +
        '</div>'+
        '</div>'+
        '<div style="background-color:white" class="body">' +
        '<h5 class="post-title"><a href="#">' + tenkh + '</a></h5>' +
        '</div>'+
        '</div>'+
        '</div>'
    return item
}

function course_item() {
    var item =''
    return item
}

function init() {
    $("#baner4").append(baner_item("Linux", "https://media.discordapp.net/attachments/905409110122561537/905409596515045396/ubuntu-logo.png"))
    $("#baner4").append(baner_item(".Net", "https://media.discordapp.net/attachments/905409110122561537/905411762730446858/Microsoft-dotnet.png"))
    $("#baner4").append(baner_item("Mobile App", "https://media.discordapp.net/attachments/905409110122561537/905413299477315614/react-native.png"))
    $("#baner4").append(baner_item("Web Development", "https://cdn.discordapp.com/attachments/905409110122561537/905414975458271232/5-Things-to-know-to-become-a-Web-Developer-in-2021.png"))
    $("#baner4").append(baner_item("Database", "https://cdn.discordapp.com/attachments/905409110122561537/905416151046172682/blob-15934489781641x.png"))
    $("#baner4").append(baner_item("Blockchain", "https://media.discordapp.net/attachments/905409110122561537/905416603687084052/dac-diem-noi-troi-cua-block-chain-1-1.png"))
    $("#baner4").append(baner_item("Tool and Utilities", "https://cdn.discordapp.com/attachments/905409110122561537/905417099650936872/utilities-icon-4.png"))
    $("#baner4").append(baner_item("Security", "https://media.discordapp.net/attachments/905409110122561537/905417515591688232/img-header-security.png"))
}


function get_course() {
    $.ajax({
        type: "GET",
        url: "api/ds_khoahoc",
        success: function (data) {
            if (data.msg != null) {
                console.log(data.msg)
            }
        },
        error: function (error) {

        }
    })
}
