$(document).ready(function () {

    //for password
    $('#show_password1').hover(function show() {
        //Change the attribute to text
        $('#AccountPassword').attr('type', 'text');
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    },
        function () {
            //Change the attribute back to password
            $('#AccountPassword').attr('type', 'password');
            $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        });

    //for confirm password
    $('#show_password2').hover(function show() {
        //Change the attribute to text
        $('#ConfirmPassword').attr('type', 'text');
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    },
        function () {
            //Change the attribute back to password
            $('#ConfirmPassword').attr('type', 'password');
            $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        });
});

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}