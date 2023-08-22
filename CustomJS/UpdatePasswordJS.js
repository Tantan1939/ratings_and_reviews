$(document).ready(function () {

    //for current password
    $('#show_password1').hover(function show() {
        //Change the attribute to text
        $('#CurrentPassword').attr('type', 'text');
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    },
        function () {
            //Change the attribute back to password
            $('#CurrentPassword').attr('type', 'password');
            $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        });

    //for new password
    $('#show_password2').hover(function show() {
        //Change the attribute to text
        $('#NewPassword').attr('type', 'text');
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    },
        function () {
            //Change the attribute back to password
            $('#NewPassword').attr('type', 'password');
            $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        });

    //for confirm password
    $('#show_password3').hover(function show() {
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