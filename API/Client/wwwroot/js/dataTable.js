// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $("#employeeTable").DataTable({
        /*filter: true,*/
        /*dom: 'Bfrtip',*/
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Employee',
                sheetName: 'Employee',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                fileName: 'Data',
                autoFilter: true,
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            },
            {
                extend: 'pdfHtml5',
                name: 'pdf',
                title: 'Employee',
                sheetName: 'Employee',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                fileName: 'Data',
                autoFilter: true,
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            }
            /*'copy', 'csv', 'excel', 'pdf', 'print'*/
        ],
        ajax: {
            "url": "/Employees/GetAll",
            "datatype": "json",
            "dataSrc": ""
        },
        columns: [
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": "nik",
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return row['firstName'] + " " + row['lastName']
                }
            },
            {
                "data": "phone",
                "orderable": false,
                "render": function (toFormat) {
                    var tPhone;
                    tPhone = toFormat.toString();
                    subsTphone = tPhone.substring(0, 1);
                    if (subsTphone == "0") {
                        tPhone = '(+62) ' + tPhone.substring(1, 4) + '-' + tPhone.substring(4, 9) + '-' + tPhone.substring(9, 14);
                        return tPhone
                    } else {
                        tPhone = '(+62) ' + tPhone.substring(0, 3) + '-' + tPhone.substring(3, 8) + '-' + tPhone.substring(8, 13);
                        return tPhone
                    }
                }
            },
            {
                "data": "salary",
                "render": function (data, type, row, meta) {
                    return "Rp " + (new Intl.NumberFormat(['ban', 'id']).format(row['salary']))
                }
            },
            {
                "data": "gender",
                "render": function (data, type, row, meta) {
                    if (row['gender'] == 0) {
                        return 'Male'
                    } else if (row['gender'] == 1) {
                        return 'Female'
                    }
                }
            },
            {
                "data": "",
                "orderable": false,
                "render": function (data, type, row, meta) {
                    return `<td scope="row"><a class="btn btn-warning btn-sm text-light" data-url="" onclick="getdatabyID('${row.nik}')" data-toggle="modal" data-target="#detailModal" title="Detail"><i class="fa fa-info-circle"></i></a></td>
                            <td scope="row"><a class="btn btn-primary btn-sm text-light" data-url="" onclick="return getbyID('${row.nik}')" data-toggle="modal" data-target="#formModal" title="Edit"><i class="fa fa-edit"></i></a></td>
                            <td scope="row"><a class="btn btn-danger btn-sm text-light" data-url="" onclick="deleteData('${row.nik}')" title="Delete"><i class="fa fa-trash-alt"></i></a></td>`
                }
            }
        ]
    });

    $(function () {
        $("form[name='registration']").validate({
            rules: {
                inputNIK: {
                    required: true
                },
                inputFirstName: {
                    required: true
                },
                inputLastName: {
                    required: true
                },
                inputPhone: {
                    required: true,
                    minlength: 10,
                    maxlength: 13
                },
                inputBirthDate: {
                    required: true
                },
                inputSalary: {
                    required: true,
                    number: true
                },
                inputEmail: {
                    required: true,
                    email: true
                },
                inputPassword: {
                    required: true,
                    minlength: 8
                    /*RegExp: `^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$`*/
                },
                inputGender: {
                    required: true
                },
                inputUniversity: {
                    required: true
                },
                inputDegree: {
                    required: true
                },
                inputGPA: {
                    required: true
                },
                inputRole: {
                    required: true
                }
            },
            messages: {
                inputNIK: {
                    required: "Please enter your NIK"
                },
                inputFirstName: {
                    required: "Please enter your first name"
                },
                inputLastName: {
                    required: "Please enter your last name"
                },
                inputPhone: {
                    required: "Please enter your phone number",
                    minlength: "Phone number should be at least 10 characters",
                    maxlength: "Phone number can't be longer than 13 characters"
                },
                inputBirthDate: {
                    required: "Please enter your birthdate"
                },
                inputSalary: {
                    required: "Please enter your salary"
                },
                inputEmail: {
                    required: "Please enter your email",
                    email: "The email should be in the format: abc@domain.tld"
                },
                inputGender: {
                    required: "Please choose your gender"
                },
                inputPassword: {
                    required: "Please enter your password",
                    minlength: "Password should be at least 8 characters"
                    /*RegExp: "Password should be contain at least 1 number, 1 lowercase character, 1 uppercase character, and 1 special (!*@#$%^&+=) character"*/
                },
                inputDegree: {
                    required: "Please choose your degree"
                },
                inputGPA: {
                    required: "Please enter your GPA"
                },
                inputUniversity: {
                    required: "Please choose your university"
                },
                inputRole: {
                    required: "Please choose your role"
                }
            }
            /*submitHandler: function () {
                form.submit();
            }*/
        });
        $('#btnAdd').click(function (e) {
            e.preventDefault();
            if ($('#formValidation').valid() == true) {
                insertData();
            }
        });
        $('#btnUpdate').click(function (e) {
            e.preventDefault();
            if ($('#formValidation').valid() == true) {
                updateData();
            }
        });
    });
});

function insertData() {
    var obj = new Object();
    obj.NIK = $('#inputNIK').val();
    obj.FirstName = $('#inputFirstName').val();
    obj.LastName = $('#inputLastName').val();
    obj.Phone = $('#inputPhone').val();
    obj.BirthDate = $('#inputBirthDate').val();
    obj.Salary = $('#inputSalary').val();
    obj.Email = $('#inputEmail').val();
    obj.Gender = $('#inputGender').val();
    /*obj.Gender = $('input[name="gender-option"]:checked').val();*/
    obj.Password = $('#inputPassword').val();
    obj.Degree = $('#inputDegree').val();
    obj.GPA = $('#inputGPA').val();
    obj.UniversityId = $('#inputUniversity').val();
    obj.RoleId = $('#inputRole').val();
    console.log(obj);

    $.ajax({
        'url': "/Employees/Register",
        /*headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },*/
        'type': 'POST',
        'data': {entity: obj}, //objek kalian
        'dataType': 'json',
    }).done((result) => {
        console.log(result);
        if (result == 200) {
            swal({
                title: "SUCCESSED",
                text: "Data berhasil ditambahkan!",
                icon: "success"
            }).then(function () {
                window.location.reload();
            });
        } else if (result == 400) {
            /*if (obj.NIK == $('#inputNIK').val(result.nik)) {
                swal({
                    title: "FAILED",
                    text: "Data gagal ditambahkan, NIK sudah terdaftar!",
                    icon: "error"
                });
            }*/
            swal({
                title: "FAILED",
                text: "Data gagal ditambahkan, periksa data yang Anda masukkan!",
                icon: "error"
            });
        } else {
            swal({
                title: "FAILED",
                text: "Data gagal ditambahkan, periksa koneksi internet Anda!",
                icon: "error"
            });
        }
        /*$("#employeeTable").DataTable().ajax.reload();*/
    /*}).fail((error) => {
        swal({
            title: "FAILED",
            text: "Data gagal ditambahkan, periksa data yang Anda masukkan!",
            icon: "error"
        });*/
        /*if (error.messages = "Data gagal dimasukkan: NIK yang Anda masukkan sudah terdaftar!") {
            swal({
                title: "FAILED",
                text: "Data gagal ditambahkan, NIK yang Anda masukkan sudah terdaftar!",
                icon: "error"
            });
        } else if (error.messages = "Data gagal dimasukkan: Phone yang Anda masukkan sudah terdaftar!") {
            swal({
                title: "FAILED",
                text: "Data gagal ditambahkan, Phone yang Anda masukkan sudah terdaftar!",
                icon: "error"
            });
        } else if (error.messages = "Data gagal dimasukkan: Email yang Anda masukkan sudah terdaftar!") {
            swal({
                title: "FAILED",
                text: "Data gagal ditambahkan, Email yang Anda masukkan sudah terdaftar!",
                icon: "error"
            });
        } else {
            swal({
                title: "FAILED",
                text: "Data gagal ditambahkan, periksa koneksi internet Anda!",
                icon: "error"
            });
        }*/
    });
}

$.ajax({
    url: "https://localhost:44391/API/Universities",
    success: function (result) {
        var optionUniv = `<option value="">---Choose University---</option>`;
        console.log(result);
        $.each(result, function (key, val) {
            optionUniv += `
                            <option value="${val.universityId}">${val.name}</option>`;
        });
        $('#inputUniversity').html(optionUniv);
    }
});

$.ajax({
    url: "https://localhost:44391/API/Roles",
    success: function (result) {
        console.log(result);
        var optionRole = `<option value="">---Choose Role---</option>`;
        $.each(result, function (key, val) {
            optionRole += `
                            <option value="${val.roleId}">${val.roleName}</option>`;
        });
        $('#inputRole').html(optionRole);
    }
});

function deleteData(nik) {
    console.log(nik)
    swal({
        title: "Are you sure?",
        text: "Data yang Anda pilih akan terhapus secara permanen!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: "/Employees/Delete/" + nik,
                type: "DELETE",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    swal({
                        title: "SUCCESSED",
                        text: "Data berhasil dihapus!",
                        icon: "success"
                    }).then(function () {
                        window.location.reload();
                    });
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        } else {
            swal("Data Anda aman tidak terhapus");
        }
    });
    /*var ans = confirm("Are you sure you want to delete this data?");
    
    if (ans) {
        $.ajax({
            url: "https://localhost:44391/API/Employees/" + nik,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                swal({
                    title: "SUCCESSED",
                    text: "Data berhasil dihapus!",
                    icon: "success"
                }).then(function () {
                    window.location.reload();
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }*/
}

//Function for getting the Data Based upon Employee ID  
function getbyID(nik) {
    $('#inputFirstName').css('border-color', 'lightgrey');
    $('#inputLastName').css('border-color', 'lightgrey');
    $('#inputPhone').css('border-color', 'lightgrey');
    $('#inputBirthDate').css('border-color', 'lightgrey');
    $('#inputBirthDate').css('border-color', 'lightgrey');
    $('#inputSalary').css('border-color', 'lightgrey');
    $('#inputEmail').css('border-color', 'lightgrey');
    $('#inputGender').css('border-color', 'lightgrey');
    $('#inputPassword').css('border-color', 'lightgrey');
    $('#inputDegree').css('border-color', 'lightgrey');
    $('#inputGPA').css('border-color', 'lightgrey');
    $('#inputUniversity').css('border-color', 'lightgrey');
    $('#inputRole').css('border-color', 'lightgrey');
    $.ajax({
        url: "https://localhost:44391/API/Employees/Profile/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result[0]);
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#inputNIK').val(result[0].nik);
            $('#inputFirstName').val(result[0].firstName);
            $('#inputLastName').val(result[0].lastName);
            $('#inputPhone').val(result[0].phone);
            $('#inputBirthDate').val(tanggal);
            $('#inputSalary').val(result[0].salary);
            $('#inputEmail').val(result[0].email);
            if (result[0].gender === "Male") {
                $('#inputGender').val(0);
            } else {
                $('#inputGender').val(1);
            }
            $('#inputPassword').val(result[0].password);
            $('#inputDegree').val(result[0].degree);
            $('#inputGPA').val(result[0].gpa);
            $('#inputUniversity').val(result[0].universityId);
            $('#inputRole').val(result[0].roleId);

            $('#formModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#univ').hide();
            $('#tingkat').hide();
            $('#ipk').hide();
            $('#role').hide();
            $('#pwd').hide();
            $('#nik').hide();
            /*$('#nik').prop('disabled', true);*/
        },
        error: function (errormessage) {
            /*alert(errormessage.responseText);*/
            swal({
                title: "FAILED",
                text: "Data tidak ditemukan!",
                icon: "error"
            }).then(function () {
                window.location = "https://localhost:44370/home/datatable";
            });
        }
    });
    return false;
}

function getdatabyID(nik) {
    console.log(nik)
    $.ajax({
        url: "https://localhost:44391/API/Employees/Profile/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result[0])
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#dataNIK').val(result[0].nik);
            $('#dataFirstName').val(result[0].firstName);
            $('#dataLastName').val(result[0].lastName);
            $('#dataPhone').val(result[0].phone);
            $('#dataBirthDate').val(tanggal);
            $('#dataSalary').val(result[0].salary);
            $('#dataEmail').val(result[0].email);
            if (result[0].gender === "Male") {
                $('#dataGender').val(0);
            } else {
                $('#dataGender').val(1);
            }
            $('#dataPassword').val(result[0].password);
            $('#dataDegree').val(result[0].degree);
            $('#dataGPA').val(result[0].gpa);
            $('#dataUniversity').val(result[0].universityId);
            $('#dataRole').val(result[0].roleId);
            $('#detailModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            swal({
                title: "FAILED",
                text: "Data tidak ditemukan!",
                icon: "error"
            }).then(function () {
                window.location.reload();
            });
        }
    });
    return false;
}

/*function getData(nik) {
    console.log(nik)
    $.ajax({
        url: "https://localhost:44391/API/Employees/profile/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result)
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#dnik').val(result[0].nik);
            $('#dfirstName').val(result[0].firstName);
            $('#dlastName').val(result[0].lastName);
            $('#dphone').val(result[0].phoneNumber);
            $('#dbirthDate').val(tanggal);
            $('#dsalary').val(result[0].salary);
            $('#demail').val(result[0].email);
            if (result[0].gender === "Male") {
                $('#dgender').val(0);
            } else {
                $('#dgender').val(1);
            };
            $('#dpassword').val(result[0].password);
            $('#ddegree').val(result[0].degree);
            $('#dgpa').val(result[0].gpa);
            $('#duniversiry_id').val(result[0].universityId);
            $('#drole_id').val(result[0].role_Id);
            $('#hid').hide();
            $('#detailModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            swal({
                title: "FAILED",
                text: "Data tidak ditemukan!",
                icon: "error"
            }).then(function () {
                window.location = "https://localhost:44370/home/datatable";
            });
        }
    });
    return false;
}*/

$.ajax({
    url: "https://localhost:44391/API/Universities",
    success: function (result) {
        var optionUniv = "";
        console.log(result);
        $.each(result, function (key, val) {
            optionUniv += `
                            <option value="${val.universityId}">${val.name}</option>`;
        });
        /*$('#inputUniversity').html(`<option value="" disabled>---Choose University---</option>`);*/
        $('#dataUniversity').html(optionUniv);
    }
});

$.ajax({
    url: "https://localhost:44391/API/Roles",
    success: function (result) {
        console.log(result);
        var optionRole = "";
        $.each(result, function (key, val) {
            optionRole += `
                            <option value="${val.roleId}">${val.roleName}</option>`;
        });
        $('#dataRole').html(optionRole);
    }
});

//function for updating employee's record  
function updateData() {
    var nik = $('#inputNIK').val();
    var obj = new Object();
    obj.NIK = $('#inputNIK').val();
    obj.FirstName = $('#inputFirstName').val();
    obj.LastName = $('#inputLastName').val();
    obj.Phone = $('#inputPhone').val();
    obj.BirthDate = $('#inputBirthDate').val();
    obj.Salary = $('#inputSalary').val();
    obj.Email = $('#inputEmail').val();
    obj.Gender = $('#inputGender').val();
    console.log(obj);

    /*var empObj = {
        NIK = $('#inputNIK').val(),
        FirstName = $('#inputFirstName').val(),
        LastName = $('#inputLastName').val(),
        Phone = $('#inputPhone').val(),
        BirthDate = $('#inputBirthDate').val(),
        Salary = $('#inputSalary').val(),
        Email = $('#inputEmail').val(),
        Gender = $('#inputGender').val(),
        Password = $('#inputPassword').val(),
        Degree = $('#inputDegree').val(),
        GPA = $('#inputGPA').val(),
        UniversityId = $('#inputUniversity').val(),
        RoleId = $('#inputRole').val()
    };*/

    $.ajax({
        url: "/Employees/Put/" + nik,
        type: "PUT",
        data: {id: nik, entity: obj},
        /*contentType: "application/json;charset=utf-8",*/
        dataType: "json",
        success: function (result) {
            /*$('#formModal').modal('hide');
            $('#inputNIK').val("");
            $('#inputFirstName').val("");
            $('#inputLastName').val("");
            $('#inputPhone').val("");
            $('#inputBirthDate').val("");
            $('#inputSalary').val("");
            $('#inputEmail').val("");
            $('#inputGender').val("");*/
            swal({
                title: "SUCCESSED",
                text: "Data berhasil diupdate!",
                icon: "success"
            }).then(function () {
                window.location.reload();
            });
            /*alert("Data berhasil diubah!")*/
        },
        error: function (errormessage) {
            swal({
                title: "FAILED",
                text: "Data gagal diupdate. Periksa data yang Anda masukkan!",
                icon: "error"
            });
            /*alert(errormessage.responseText);*/
        }
    });
}

function exportToExcel() {
    var table = $('#employeeTable').DataTable();
    table.buttons('excel:name').trigger();
}

function exportToPdf() {
    var table = $('#employeeTable').DataTable();
    table.buttons('pdf:name').trigger();
}

$.ajax({
    url: "https://localhost:44391/API/Employees/Gender",
    success: function (result) {
        var series = [];
        var label = [];
        $.each(result.result, function (key, val) {
            series.push(val.value);
            if (val.gender === 0) {
                label.push("Male");
            } else {
                label.push("Female");
            }
        });
        var options = {
            chart: {
                type: 'pie'
            },
            series: series,
            labels: label
        }
        var chart = new ApexCharts(document.querySelector("#myPieChart"), options);
        chart.render();
    }
})

$.ajax({
    url: "https://localhost:44391/API/Employees/GetRole",
    success: function (result) {
        var series = [];
        var label = [];
        $.each(result.result, function (key, val) {
            series.push(val.value);
            if (val.role === 1) {
                label.push("Manager");
            }
            else if (val.role === 2) {
                label.push("Employee");
            }
            else if (val.role === 3) {
                label.push("Director");
            }
        });
        var options = {
            chart: {
                type: 'donut'
            },
            legend: {
                position: 'top',
            },
            series: series,
            labels: label
        }
        var chart = new ApexCharts(document.querySelector("#myDonutChart"), options);
        chart.render();
    }
})

$.ajax({
    url: "https://localhost:44391/API/Employees/GetDegree",
    success: function (result) {
        var data = [];
        var degree = [];
        $.each(result.result, function (key, val) {
            degree.push(val.degree);
            data.push(val.value);
        });
        var options = {
            chart: {
                type: 'bar'
            },
            series: [{
                name: 'Total',
                data: data
            }],
            xaxis: {
                categories: degree
            }
        }
        var chart = new ApexCharts(document.querySelector("#myBarChart"), options);
        chart.render();
    }
})

/*$.ajax({
    url: "https://localhost:44391/API/Employees/GetSalary",
    success: function (result) {
        var data = [];
        var categories = [];
        $.each(result.result, function (key, val) {
            categories.push(val.salary);
            data.push(val.value);
        });
        var options = {
            chart: {
                *//*height: 280,*//*
                type: "area",
                stacked: true
            },
            *//*dataLabels: {
                enabled: false
            },*//*
            series: [
                {
                    name: "Total",
                    data: data
                }
            ],
            *//*fill: {
                type: "gradient",
                gradient: {
                    shadeIntensity: 1,
                    opacityFrom: 0.7,
                    opacityTo: 0.9,
                    stops: [0, 90, 100]
                }
            },*//*
            xaxis: {
                categories: categories
            }
        };

        var chart = new ApexCharts(document.querySelector("#myAreaChart"), options);
        chart.render();
    }
})*/

$.ajax({
    url: "https://localhost:44391/API/Employees/GetSalary2",
    success: function (result) {
        var data = [];
        var categories = [];
        $.each(result.result, function (key, val) {
            categories.push(val.label);
            data.push(val.value);
        });
        var options = {
            chart: {
                /*height: 280,*/
                type: "area",
                stacked: true
            },
            /*dataLabels: {
                enabled: false
            },*/
            series: [
                {
                    name: "Total",
                    data: data
                }
            ],
            /*fill: {
                type: "gradient",
                gradient: {
                    shadeIntensity: 1,
                    opacityFrom: 0.7,
                    opacityTo: 0.9,
                    stops: [0, 90, 100]
                }
            },*/
            xaxis: {
                categories: categories
            }
        };

        var chart = new ApexCharts(document.querySelector("#myAreaChart"), options);
        chart.render();
    }
})
/*var options = {
    chart: {
        type: 'bar'
    },
    series: [{
        name: 'sales',
        data: [30, 40, 45, 50, 49, 60, 70, 91, 125]
    }],
    xaxis: {
        categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999]
    }
}
var chart = new ApexCharts(document.querySelector("#chart"), options);
chart.render();*/