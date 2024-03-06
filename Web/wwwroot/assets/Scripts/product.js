$(document).ready(function () {
    var check = true;
    $(".category-title").click(function () {
        var id = $(this).data("id");
        $(`.sub-categories[id=${id}]`).toggle(300);
        if (check) {
            rotate(".arrow", id, 180)

            check = false;
        } else if (!check) {
            rotate(".arrow", id, 0)
            check = true;
        }
    });
});


$(".sub-categories").css("display", "none")

function rotate(element, id, degree) {
    $(`${element}[id=${id}]`).animate({
        transform: degree
    }, {

        step: function (now, fx) {
            $(this).css({
                '-webkit-transform': 'rotate(' + now + 'deg)',
                '-moz-transform': 'rotate(' + now + 'deg)',
                'transform': 'rotate(' + now + 'deg)',
                'color': 'orangered'
            });
        }
    });
}


$(".head-right").css("display", "none");

$(document).on("mouseover", ".product-item", function () {
    var id = $(this).data("id");
    $(`.head-right[id=${id}]`).fadeIn(300);
});

$(document).on("mouseleave", ".product-item", function () {
    var id = $(this).data("id");
    $(`.head-right[id=${id}]`).fadeOut(300);
});


//let checkboxes = document.querySelectorAll(".gender");

//for (let i = 0; i < checkboxes.length; i++) {
//    checkboxes[i].addEventListener("change", function () {
//        if (this.checked) {
//            var gender = $(this).data('gender')
//            console.log(gender)
//        }
//    });
//}


$("input[name='genderCheck']").change(function () {
    // Get the current state of the checkboxes
    var filter = $("input[name='genderCheck']:checked").map(function () {
        return this.value;
    }).get();
    var Gender = 0
    var filtered = filter.map(function (Genders) {
        Gender = Genders
    })
    var models = {
        Gender:Gender
        }
    $.ajax({
        url: "/product/filterproducts",
        data: JSON.stringify(models),
        success: function (result) {
            $("#products").html(result);
            $("#MinPrice").val('');
            $("#MaxPrice").val('');

        }
    });

});

//$("input[name='modelCheck']").change(function () {
//    // Get the current state of the checkboxes
//    var filter = $("input[name='modelCheck']:checked").map(function () {
//        return this.value;
//    }).get();
//    var models = 0
//    var filtered = filter.map(function (model) {
//        models = model
//    })
//    $.ajax({
//        url: "/product/filterproducts",
//        data: { models: models },
//        success: function (result) {
//            $("#products").html(result);
//        }
//    });

//});






$(document).on('click', '.changepage', function (e) {
    var pageNumber = $(this).data('page')
    $(".loader-2").show()
    $.ajax({
        type: "GET",
        url: "/product/GetProductsPaginate",
        data: {
            pageNumber: pageNumber,

        },
        success: function (data) {
            $(".loader-2").hide(200);
            $("#products").html(data);
        }
    });
})

$(".card-img img").hide()

setTimeout(() => {
    $(".card-img img").show()
}, 1000)



//$("#filterResetbtn").click(function () {
//    document.getElementById("filter").reset();
//})

 