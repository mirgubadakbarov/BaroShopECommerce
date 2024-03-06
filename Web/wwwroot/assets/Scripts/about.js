$(document).ready(function () {
    $(document).on('click', ".lines-button", function () {
        $('.burger-menu-items').show(200);
        $('.lines-button').addClass('close');
    });
    $(document).on('click', ".lines-button.close", function () {
        $('.burger-menu-items').hide(200);
        $('.lines-button').removeClass('close');
    });



    $(".menu-item").click(function () {
        $(".menu-item").removeClass("choosen")
        $(this).addClass("choosen")
    })







   






});




