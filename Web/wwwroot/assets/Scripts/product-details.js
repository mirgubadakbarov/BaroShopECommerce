$('.slider-for').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    fade: true,
    asNavFor: '.slider-nav'
  });
  $('.slider-nav').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    asNavFor: '.slider-for',
    dots: false,
    focusOnSelect: true
  });
  
  $('a[data-slide]').click(function(e) {
    e.preventDefault();
    var slideno = $(this).data('slide');
    $('.slider-nav').slick('slickGoTo', slideno - 1);
  });

  $(document).ready(function(){
    $('.customizer-carousel').slick({
      slidesToShow: 7,
      slidesToScroll: 1,
      swipeToSlide: true
    });
  });
  
  


  transform_slider(
    document.querySelector(".related-items > ul"),
    document.querySelector("#prevrelated"),
    document.querySelector("#nextrelated")
  );
  
  function transform_slider(block, left_button, right_button) {
    const widht_li = block.querySelector("li").getBoundingClientRect().width;
    let blockleft = 0;
    let transform;
  
    right_button.onclick = function () {
      left_button.classList.add("active");
      left_button.classList.remove("no-active");
      if (block.style.transform) {
        blockleft = block.style.transform.match("\\d+")[0];
      }
  
      if (blockleft < block_widht()) {
        block.style.transform = `translate(-${Number(blockleft) + widht_li}px)`;
      }
  
      updater();
    };
  
    left_button.onclick = function () {
      right_button.classList.add("active");
      right_button.classList.remove("no-active");
      if (block.style.transform) {
        blockleft = block.style.transform.match("\\d+")[0];
      }
  
      block.style.transform = `translate(-${Number(blockleft) - widht_li}px)`;
  
      updater();
    };
  
    block.ontouchstart = function () {
      if (block.style.transform) {
        blockleft = block.style.transform.match("\\d+")[0];
      }
      let startX = Number(blockleft) + event.changedTouches[0].pageX;
      block.style.transform = `translate(-${
        startX - event.changedTouches[0].pageX
      }px)`;
  
      document.ontouchmove = function (event) {
        block.classList.add("no-transistion");
        blockleft = block.style.transform.match("\\d+")[0];
  
        if (blockleft <= block_widht()) {
          block.style.transform = `translate(-${
            startX - event.changedTouches[0].pageX
          }px)`;
        }
        updater();
        return false;
      };
  
      document.ontouchend = function () {
        block.classList.remove("no-transistion");
        let stop_li = Math.round(
          Number(block.style.transform.match("\\d+")[0]) /
            block.querySelector("li").getBoundingClientRect().width
        );
        block.style.transform = `translate(-${
          block.querySelector("li").getBoundingClientRect().width * stop_li
        }px)`;
        block.classList.remove("no-transistion");
        document.ontouchmove = false;
        updater();
      };
    };
  
    block.onmousedown = function (event) {
      if (block.style.transform) {
        blockleft = block.style.transform.match("\\d+")[0];
      }
  
      let startX = Number(blockleft) + event.pageX;
      block.style.transform = `translate(-${startX - event.pageX}px)`;
  
      document.onmousemove = function (event) {
        block.classList.add("no-transistion");
        blockleft = block.style.transform.match("\\d+")[0];
        if (blockleft <= block_widht()) {
          block.style.transform = `translate(-${startX - event.pageX}px)`;
        }
        updater();
        return false;
      };
  
      document.onmouseup = function () {
        block.classList.remove("no-transistion");
        let stop_li = Math.round(
          Number(block.style.transform.match("\\d+")[0]) /
            block.querySelector("li").getBoundingClientRect().width
        );
        block.style.transform = `translate(-${
          block.querySelector("li").getBoundingClientRect().width * stop_li
        }px)`;
        block.classList.remove("no-transistion");
        document.onmousemove = false;
        updater();
      };
    };
  
    function updater() {
      transform = Number(block.style.transform.match("\\d+")[0]);
      if (transform > block_widht()) {
        block.style.transform = `translate(-${block_widht()}px)`;
      }
      if (transform <= 3) {
        left_button.classList.add("no-active");
      } else {
        left_button.classList.remove("no-active");
      }
      if (transform >= block_widht()) {
        right_button.classList.add("no-active");
      } else {
        right_button.classList.remove("no-active");
      }
    }
  
    function block_widht() {
      return (
        block.querySelector("li").getBoundingClientRect().width *
          block.querySelectorAll("li").length -
        block.getBoundingClientRect().width
      );
    }
  }
 

$(document).ready(function(){
  $(".size").click(function(){
    $("#choosen-size").html(`${$(this).data("value")}`)
  })
})

$(document).ready(function(){
  $(".color").click(function(){
    $("#choosen-color").html($(this).data("value"))
  })
})





  


  