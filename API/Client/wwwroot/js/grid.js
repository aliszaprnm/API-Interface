// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*var judul = document.getElementById("judul");
judul.style.backgroundColor = 'lightgreen';

//untuk ngubah teks html judul tanpa mengubah syntax
judul.innerHTML = 'Diubah melalui JavaScript';

var p = document.getElementsByTagName("p");

//untuk ngubah warna background Paragraf 1
p[0].style.backgroundColor = 'yellow';

//untuk ngubah warna background seluruh elemen paragraf
for (var i = 0; i < p.length; i++) {
    p[i].style.backgroundColor = 'yellow';
}

//untuk ngubah warna background Halo
p['a'].style.backgroundColor = 'yellow';

//untuk ngubah warna background Paragraf 3
p[2].style.backgroundColor = 'yellow';

//menampilkan section id='a'
var q = document.querySelector('#a');

//menampilkan semua elemen yang memiliki id a
var x = document.querySelectorAll('#a');

//kalo cuma mau nampilin si Halo aja
var y = document.querySelectorAll('#a')[1];

//Paragraf 4
var psemua = document.querySelector('section#b p.b');
psemua.addEventListener("click", function () {
    psemua.innerHTML = 'CONGRATSSS!!'
});

psemua.addEventListener("mouseleave", function () {
    psemua.innerHTML = 'Mouse keluar!!'
});

var btn = document.getElementsByTagName("button")[0];
btn.addEventListener("click", function () {
    alert("button telah diclick");
});

//function saat submit diklik lalu muncul alert lalu warna background paragraf 4 berubah
function panggilAlert() {
    psemua.style.backgroundColor = 'pink';
    alert("Tombol diklik menggunakan onclick")
}

//pake jquery ngubah teks html  Halo
$("div#b p#a").html("Diubah melalui JQuery");

$("div#b p#a").click(function () {
    $("div#b p#a").css('background-color', 'green');
});*/

var cont1 = document.getElementById('con1');
var cont2 = document.getElementById('con2');
var cont3 = document.getElementById('con3');

var tombol1 = document.querySelector('.btn1');
tombol1.addEventListener("click", function () {
    alert("Tombol 1 diklik");
    cont1.style.backgroundColor = 'palevioletred';
    cont2.style.backgroundColor = 'yellow';
    /*cont2.click(function () {
        cont2.innerHTML("<video width='320' height='240'/><source src='/cocomelon.mp4' type='video/mp4'></video>".play());
        });
        cont3.style.backgroundColor = 'aqua';
    });*/
});

$(".btn2").click(function () {
    alert("Tombol 2 diklik");
    $("body").css('background-color', 'aquamarine');
    /*$("#con2").mouseenter(function () {
        $(this).hide();
    });*/
    $("#con1").mouseenter(function () {
        $(this).html("BERUBAHHHH");
    });
    $("#con1").mouseleave(function () {
        $(this).hide();
    });
});

function klikTombol3() {
    alert("Tombol 3 diklik");
    cont3.ondblclick = function () {
        cont3.innerHTML = "<img src='https://image.freepik.com/free-vector/opened-surprise-gift-box_3446-340.jpg' />";
    };

    /*cont3.style.textAlign = "center";
    cont3.ondblclick = function () {
        cont3.innerHTML("BERUBAHHHH");
    };*/
}