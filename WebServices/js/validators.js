

function validateEmail(input, handler) {
    inputText = $("#" + input).val();
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (inputText.match(mailformat)) {
        $("#" + handler).html("");
        return true;
    }
    else {
        $("#" + handler).html("invalid email");
        return false;

    }
}

function validatePhone(input, handler) {
    inputText = $("#" + input).val();
    if (inputText.match("^[0-9]+$")) {
        $("#" + handler).html("");
        return true;
    }
    else {
        $("#" + handler).html("invalid phone number");
        return false;

    }
}


function validateMatchingPasswords(input1, input2, handler) {
    if ($("#" + input1).val() == $("#" + input2).val()) {
        $("#" + handler).html("");
        return true;
    }
    else {
        $("#" + handler).html("passwords doesn't match");
        return false;

    }
}