//declaring array one dimensional
let array = [1, 2, 3, 4];

//declaring array multi dimensional
let arrayMultiDimensional = ['a', 'b', 'c', [1, 2, 'e'], true];

//menghasilkan nilai array indeks ke-2 yaitu 3
console.log(array[2]);

//menghasilkan nilai e pada arrayMultiDimensional
console.log(arrayMultiDimensional[3][2]);

//menghasilkan nilai terakhir pada variabel array yaitu 4
let element = null;
for (let i = 0; i < array.length; i++) {
    element = array[i];
}
console.log(element);

//menghasilkan total nilai keseluruhan pada variabel array yaitu 10
let jmlElement = null;
for (let i = 0; i < array.length; i++) {
    jmlElement += array[i];
}
console.log(jmlElement);

//menambah value pada array yang dimasukkan di posisi terakhir
array.push('haloo');
console.log(array);

//menghapus satu nilai pada indeks terakhir
array.pop();
console.log(array);

//mennambah value pada array yang dimasukkan di posisi awal
array.unshift('ini di depan');
console.log(array);

//menghapus satu nilai pada indeks pertaama
array.shift();
console.log(array);

//bikin object mahasiswa
let mahasiswa = {
    nama: "jonathan",
    nim: 'a1234',
    umur: 24,
    hobby: ['main game', 'wibu', 'renang'],
    isActive: true
}
console.log(mahasiswa);

//mengambil hobby kedua yaitu wibu
console.log(mahasiswa.hobby[1]);

//bikin object user
const user = {};
user.nama = 'budi';
user.umur = 30;
console.log(user);

//mengambil nilai umur dari user
let key = 'umur';
console.log(user);
console.log(user[key]);

const hitung = (num1, num2) => num1 + num2;
const hitung2 = (num1, num2) => {
    const jumlah = num1 + num2;
    return jumlah;
}
console.log(hitung(5, 10));
console.log(hitung2(7, 10));

const animals = [
    { name: 'Nemo', species: 'Fish', class: { name: 'Invertebrata' } },
    { name: 'Simba', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Dory', species: 'Fish', class: { name: 'Ikan' } },
    { name: 'Panther', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Budi', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Cobra', species: 'Reptile', class: { name: 'Reptil' } },
    { name: 'Anaconda', species: 'Reptile', class: { name: 'Mamalia' } },
    { name: 'Katak', species: 'Amfibi', class: { name: 'Kodok' } },
    { name: 'Tiger', species: 'Cat', class: { name: 'Mammals' } },
    { name: 'Kakatua', species: 'Aves', class: { name: 'Burung' } },
    { name: 'Siput', species: 'Mollusca', class: { name: 'Keong-keongan' } },
    { name: 'Plankton', species: 'Protozoa', class: {name: 'Musuh Tuan Krab'}}
]

//mengambil name dari class si animal Simba
/*console.log(animals[1].class.name);*/

//Tugas 1
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'Fish') {
        animals[i].class.name = 'Non-Mamalia';
    }
    else if (animals[i].species == 'Cat') {
        animals[i].class.name = 'Mamalia';
    }
    else if (animals[i].species == 'Reptile', 'Amfibi', 'Aves') {
        animals[i].class.name = 'Vertebrata';
    }
    else {
        animals[i].class.name = 'Avertebrata';
    }
}
console.log(animals);

//Tugas 2
const meong = [];
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'Cat') {
        meong.push(animals[i]);
    }
}
console.log(meong);