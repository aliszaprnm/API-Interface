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



/*var cont1 = document.getElementById('con1');
var cont2 = document.getElementById('con2');
var cont3 = document.getElementById('con3');

var tombol1 = document.querySelector('.btn1');
tombol1.addEventListener("click", function () {
    alert("Tombol 1 diklik");
    cont1.style.backgroundColor = 'palevioletred';
    cont2.style.backgroundColor = 'yellow';
    cont2.click(function () {
        *//*cont2.innerHTML("<video width='320' height='240'/><source src='/cocomelon.mp4' type='video/mp4'></video>".play());
        });
        cont3.style.backgroundColor = 'aqua';*//*
    });
});

$(".btn2").click(function () {
    alert("Tombol 2 diklik");
    $("body").css('background-color', 'aquamarine');
    $("#con2").mouseenter(function () {
        $(this).hide();
    });
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

    *//*cont3.style.textAlign = "center";
    cont3.ondblclick = function () {
        cont3.innerHTML("BERUBAHHHH");
    };*//*
}*/


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    success: function (result) {
        console.log(result.results);
        var listpokemon = "";
        $.each(result.results, function (key, val) {
            listpokemon += `<tr>
                            <td><center>${key + 1}</center></td>
                            <td style="text-transform:capitalize">${val.name}</td>
                            <td><center><button type="button" class="btn btn-primary" onclick="launchModal('${val.url}');" data-url="${val.url}" data-toggle="modal" data-target="#modalP">Detail</button></center></td>
                        </tr>`;
        });
        $('#tablePoke').html(listpokemon);
    }
})

function alertPoke(url) {
    alert(url);
}

function launchModal(url) {
    console.log(url);
    listP = "";
    let type = "";
    let stat = "";
    let ability = "";
    let move = "";
    $.ajax({
        url: url,
        success: function (result) {
            for (let i = 0; i < result.types.length; i++) {
                if (result.types[i].type.name == 'grass') {
                    type += ` <span class="badge badge-success">Grass</span>`;
                }
                else if (result.types[i].type.name == 'poison') {
                    type += ` <span class="badge badge-dark">Poison</span>`;
                }
                else if (result.types[i].type.name == 'fire') {
                    type += ` <span class="badge badge-danger">Fire</span>`;
                }
                else if (result.types[i].type.name == 'water') {
                    type += ` <span class="badge badge-info">Water</span>`;
                }
                else if (result.types[i].type.name == 'bug') {
                    type += ` <span class="badge badge-warning">Bug</span>`;
                }
                else if (result.types[i].type.name == 'normal') {
                    type += ` <span class="badge badge-secondary">Normal</span>`;
                }
                else if (result.types[i].type.name == 'flying') {
                    type += ` <span class="badge badge-primary">Flying</span>`;
                }
            }

            for (let j = 0; j < result.stats.length; j++) {
                if (j == 0) {
                    stat += `${result.stats[0].stat.name}: ${result.stats[0].base_stat}, <br />`;
                }
                else if (j == result.stats.length - 1) {
                    stat += `${result.stats[j].stat.name}: ${result.stats[j].base_stat}`;
                } else {
                    stat += `${result.stats[j].stat.name}: ${result.stats[j].base_stat}, <br />`
                }
            }

            for (let k = 0; k < result.abilities.length; k++) {
                if (k == 0) {
                    ability += `${result.abilities[0].ability.name}, `;
                }
                else if (k == result.abilities.length - 1) {
                    ability += `${result.abilities[k].ability.name}`;
                } else {
                    ability += `${result.abilities[k].ability.name}, `
                }
            }

            //ini dicomment
            /*for (result.moves = 0; result.moves <= 4; l++) {
                if (result.moves == 0) {
                    move += `${result.moves[0].move.name}, `;
                }
                else if ( == 4) {
                    move += `${result.moves[l].move.name}`;
                } else {
                    move += `${result.moves[l].move.name}, `
                }
            }*/

            var a = result.moves;
            for (a = 0; a <= 4; a++) {
                if (a == 0) {
                    move += `${result.moves[0].move.name}, `;
                }
                else if (a == 4) {
                    move += `${result.moves[a].move.name}`;
                }
                else {
                    move += `${result.moves[a].move.name}, `;
                }
            }

            listP += `<div>
                        <p><center><img src="${result.sprites.other.dream_world.front_default}" width="250" height="300"></center></p>
                        <h3 style="text-transform: capitalize"><center>${result.name}</center></h3>
                    </div>
                    <table class="table table-borderless">
                        <tr>
                            <th>Weight</th>
                            <td>${result.weight}</td>
                        </tr>
                        <tr>
                            <th>Height</th>
                            <td>${result.height}</td>
                        </tr>
                        <tr>
                            <th>Types</th>
                            <td>${type}</td>
                        </tr>
                        <tr>
                            <th>Base Experience</th>
                            <td>${result.base_experience}</td>
                        </tr>
                        <tr>
                            <th>Abilities</th>
                            <td>${ability}</td>
                        </tr>
                        <tr>
                            <th>Moves</th>
                            <td>${move}</td>
                        </tr>
                        <tr>
                            <th>Stats</th>
                            <td>${stat}</td>
                        </tr>
                    </table>`;
            $('.modal-body').html(listP);
        }
    })
}


/*$.ajax({
    url: "https://swapi.dev/api/people/",
    success: function (result) {
        console.log(result.results);
        var listpoke = "";
        $.each(result.results, function (key, val) {
            listpoke += `<tr>
                                <td>${key + 1}</td>
                                <td>${val.name}</td>
                                <td>${val.height}</td>
                                <td>${val.hair_color}</td>
                                <td>${val.skin_color}</td>
                                <td><button type="button" class="btn btn-primary" onclick="launchModal('${val.url}');" data-url="${val.url}" data-toggle="modal" data-target="#modalSW">Detail</button></td>
                            </tr>`;
        });
        $('#tablePeople').html(listpoke);
    }
})*/

/*function alertPoke(url) {
    alert(url);
}*/

/*function launchModal(url) {
    console.log(url);
    listSW = "";
    $.ajax({
        url: url,
        success: function (result) {
                listSW += `<tr>
                                <td>${result.name}</td>
                                </button></td>
                            </tr>`;
            $('.modal-body').html(listSW);
        }
    })
}*/


//using datatable
/*$(document).ready(function () {
    $("#tableSW").DataTable({
        ajax: {
            'url': "https://swapi.dev/api/people/",
            'dataSrc': 'results'
        },
        columns: [
            {
                "data": "name",
            },
            {
                "data": "height",
                "render": function (data, type, row, meta) {
                    return row['height'] + " cm"
                }
            },
            {
                "data": "gender",
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" onclick="launchModal('${row['url']}');" data-url="${row['url']}" data-toggle="modal" data-target="#modalSW">Detail</button>`
                }
            }
        ]
    });
})*/

/*function alertPoke(url) {
    alert(url);
}*/

/*function launchModal(url) {
    console.log(url);
    listSW = "";
    $.ajax({
        url: url,
        success: function (result) {
            listSW += `<tr>
                                <td>${result.name}</td>
                                </button></td>
                            </tr>`;
            $('.modal-body').html(listSW);
        }
    })
}*/