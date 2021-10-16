/*===SHOW NAVBAR===*/
const showMenu = (headerToggle, navbarId) =>{
    const toggleBtn = document.getElementById(headerToggle),
    nav = document.getElementById(navbarId)

    //Validate that variables exist
    if(headerToggle && navbarId){
        toggleBtn.addEventListener('click', ()=>{
            //We add the show-menu class to the div tag with the nav__menu class
            nav.classList.toggle('show-menu')

            //Change icon
            toggleBtn.classList.toggle('fa-times')
        })
    }
}
showMenu('header-toggle','navbar')

/*===LINK ACTIVE===*/
const linkColor = document.querySelectorAll('.nav__link')

function colorLink(){
    linkColor.forEach(L => L.classList.remove('active'))
    this.classList.add('active')
}

linkColor.forEach(L => L.addEventListener('click', colorLink))

function Dangxuat() {
    setCookie("userID", "", 1)
    window.location = "/Home/Index"
}
function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

$(document).ready(function () {
    $("#btn_dangxuat").click(function () {
        Dangxuat()
    })
});