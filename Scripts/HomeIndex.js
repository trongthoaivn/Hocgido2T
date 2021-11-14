
$(document).ready(function () {
    $("#homeli ").addClass("active")
    init()
});
    


function baner_item(tenkh,hinhanh) {
    var item = 
        '<div style="text-align:center;" class="col-lg-3 py-3 wow fadeInUp">'+
        '<div  class="card-blog">'+
        '<div  class="header">'+
        '<div style="height: 145px" class="post-thumb">' +
        '<img style="width:100%;height: auto;" src="' + hinhanh + '" alt="">' +
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
    $("#baner4").append(baner_item("Linux", "https://media.discordapp.net/attachments/909009794084532256/909009813248294912/00-Linux.png"))
    $("#baner4").append(baner_item(".Net", "https://media.discordapp.net/attachments/905409110122561537/905411762730446858/Microsoft-dotnet.png"))
    $("#baner4").append(baner_item("Mobile App", "https://media.discordapp.net/attachments/909009794084532256/909361235676512296/MOBILE-APP-DEVELOPMENT.png"))
    $("#baner4").append(baner_item("Web Development", "https://media.discordapp.net/attachments/909009794084532256/909362029515653140/cssimg.png"))
    $("#baner4").append(baner_item("Database", "https://cdn.discordapp.com/attachments/905409110122561537/905416151046172682/blob-15934489781641x.png"))
    $("#baner4").append(baner_item("Blockchain", "https://media.discordapp.net/attachments/909009794084532256/909362562632654859/Ways-in-which-trade-finance-can-be-reshaped-by-blockchain.png?width=1193&height=671"))
    $("#baner4").append(baner_item("Tool and Utilities", "https://media.discordapp.net/attachments/909009794084532256/909366173764812830/top-free-seo-tools-5f110b597b71e.png?width=1278&height=671"))
    $("#baner4").append(baner_item("Security", "https://media.discordapp.net/attachments/909009794084532256/909364637336731699/Machine-Learning-for-Healthcare-Security.png"))
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