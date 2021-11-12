const inputs = document.querySelectorAll(".input");

function focusFunx() {
    let parent = this.parentNode.parentNode;
    parent.classList.add("focus");
}

inputs.forEach((input) => {
    input.addEventListener("focus", focusFunx);
});

/*$(document).ready($(function () {
    $("form[name='login']").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 8
                *//*RegExp: `^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$`*//*
            }
        },
        messages: {
            Email: {
                required: "Please enter your email",
                email: "The email should be in the format: abc@domain.tld"
            },
            Password: {
                required: "Please enter your password",
                minlength: "Password should be at least 8 characters"
                *//*RegExp: "Password should be contain at least 1 number, 1 lowercase character, 1 uppercase character, and 1 special (!*@#$%^&+=) character"*//*
            }
        }
        *//*submitHandler: function () {
            form.submit();
        }*//*
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
);*/