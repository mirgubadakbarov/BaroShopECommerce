
(function () {
    var $$ = function (selector, context) {
        var context = context || document;
        var elements = context.querySelectorAll(selector);
        return [].slice.call(elements);
    };

    function _fncSliderInit($slider, options) {
        var prefix = ".fnc-";

        var $slider = $slider;
        var $slidesCont = $slider.querySelector(prefix + "slider__slides");
        var $slides = $$(prefix + "slide", $slider);
        var $controls = $$(prefix + "nav__control", $slider);
        var $controlsBgs = $$(prefix + "nav__bg", $slider);
        var $progressAS = $$(prefix + "nav__control-progress", $slider);

        var numOfSlides = $slides.length;
        var curSlide = 1;
        var sliding = false;
        var slidingAT =
            +parseFloat(getComputedStyle($slidesCont)["transition-duration"]) * 1000;
        var slidingDelay =
            +parseFloat(getComputedStyle($slidesCont)["transition-delay"]) * 1000;

        var autoSlidingActive = false;
        var autoSlidingTO;
        var autoSlidingDelay = 5000; // default autosliding delay value
        var autoSlidingBlocked = false;

        var $activeSlide;
        var $activeControlsBg;
        var $prevControl;

        function setIDs() {
            $slides.forEach(function ($slide, index) {
                $slide.classList.add("fnc-slide-" + (index + 1));
            });

            $controls.forEach(function ($control, index) {
                $control.setAttribute("data-slide", index + 1);
                $control.classList.add("fnc-nav__control-" + (index + 1));
            });

            $controlsBgs.forEach(function ($bg, index) {
                $bg.classList.add("fnc-nav__bg-" + (index + 1));
            });
        }

        setIDs();

        function afterSlidingHandler() {
            $slider
                .querySelector(".m--previous-slide")
                .classList.remove("m--active-slide", "m--previous-slide");
            $slider
                .querySelector(".m--previous-nav-bg")
                .classList.remove("m--active-nav-bg", "m--previous-nav-bg");

            $activeSlide.classList.remove("m--before-sliding");
            $activeControlsBg.classList.remove("m--nav-bg-before");
            $prevControl.classList.remove("m--prev-control");
            $prevControl.classList.add("m--reset-progress");
            var triggerLayout = $prevControl.offsetTop;
            $prevControl.classList.remove("m--reset-progress");

            sliding = false;
            var layoutTrigger = $slider.offsetTop;

            if (autoSlidingActive && !autoSlidingBlocked) {
                setAutoslidingTO();
            }
        }

        function performSliding(slideID) {
            if (sliding) return;
            sliding = true;
            window.clearTimeout(autoSlidingTO);
            curSlide = slideID;

            $prevControl = $slider.querySelector(".m--active-control");
            $prevControl.classList.remove("m--active-control");
            $prevControl.classList.add("m--prev-control");
            $slider
                .querySelector(prefix + "nav__control-" + slideID)
                .classList.add("m--active-control");

            $activeSlide = $slider.querySelector(prefix + "slide-" + slideID);
            $activeControlsBg = $slider.querySelector(prefix + "nav__bg-" + slideID);

            $slider
                .querySelector(".m--active-slide")
                .classList.add("m--previous-slide");
            $slider
                .querySelector(".m--active-nav-bg")
                .classList.add("m--previous-nav-bg");

            $activeSlide.classList.add("m--before-sliding");
            $activeControlsBg.classList.add("m--nav-bg-before");

            var layoutTrigger = $activeSlide.offsetTop;

            $activeSlide.classList.add("m--active-slide");
            $activeControlsBg.classList.add("m--active-nav-bg");

            setTimeout(afterSlidingHandler, slidingAT + slidingDelay);
        }

        function controlClickHandler() {
            if (sliding) return;
            if (this.classList.contains("m--active-control")) return;
            if (options.blockASafterClick) {
                autoSlidingBlocked = true;
                $slider.classList.add("m--autosliding-blocked");
            }

            var slideID = +this.getAttribute("data-slide");

            performSliding(slideID);
        }

        $controls.forEach(function ($control) {
            $control.addEventListener("click", controlClickHandler);
        });

        function setAutoslidingTO() {
            window.clearTimeout(autoSlidingTO);
            var delay = +options.autoSlidingDelay || autoSlidingDelay;
            curSlide++;
            if (curSlide > numOfSlides) curSlide = 1;

            autoSlidingTO = setTimeout(function () {
                performSliding(curSlide);
            }, delay);
        }

        if (options.autoSliding || +options.autoSlidingDelay > 0) {
            if (options.autoSliding === false) return;

            autoSlidingActive = true;
            setAutoslidingTO();

            $slider.classList.add("m--with-autosliding");
            var triggerLayout = $slider.offsetTop;

            var delay = +options.autoSlidingDelay || autoSlidingDelay;
            delay += slidingDelay + slidingAT;

            $progressAS.forEach(function ($progress) {
                $progress.style.transition = "transform " + delay / 1000 + "s";
            });
        }

        $slider
            .querySelector(".fnc-nav__control:first-child")
            .classList.add("m--active-control");
    }

    var fncSlider = function (sliderSelector, options) {
        var $sliders = $$(sliderSelector);

        $sliders.forEach(function ($slider) {
            _fncSliderInit($slider, options);
        });
    };

    window.fncSlider = fncSlider;
})();

/* not part of the slider scripts */

/* Slider initialization
  options:
  autoSliding - boolean
  autoSlidingDelay - delay in ms. If audoSliding is on and no value provided, default value is 5000
  blockASafterClick - boolean. If user clicked any sliding control, autosliding won't start again
  */
fncSlider(".example-slider", { autoSlidingDelay: 4000 });

transform_slider(
    document.querySelector(".sliders_block > ul"),
    document.querySelector("#left"),
    document.querySelector("#right")
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
        block.style.transform = `translate(-${startX - event.changedTouches[0].pageX
            }px)`;

        document.ontouchmove = function (event) {
            block.classList.add("no-transistion");
            blockleft = block.style.transform.match("\\d+")[0];

            if (blockleft <= block_widht()) {
                block.style.transform = `translate(-${startX - event.changedTouches[0].pageX
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
            block.style.transform = `translate(-${block.querySelector("li").getBoundingClientRect().width * stop_li
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
            block.style.transform = `translate(-${block.querySelector("li").getBoundingClientRect().width * stop_li
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

//testimonial slider
$(".testimonials").slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    speed: 300,
    dots: true,
    centerMode: true,
    centerPadding: "0px",
    autoplay: true,
    autoplaySpeed: 2000,
    responsive: [
        {
            breakpoint: 768,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                dots: true,
                arrows: false,
            },
        },
    ],
});

// Params
var sliderSelector = ".swiper-container",
    options = {
        init: false,
        loop: true,
        speed: 800,
        slidesPerView: 2, // or 'auto'
        // spaceBetween: 10,
        centeredSlides: true,
        effect: "coverflow", // 'cube', 'fade', 'coverflow',
        coverflowEffect: {
            rotate: 50, // Slide rotate in degrees
            stretch: 0, // Stretch space between slides (in px)
            depth: 100, // Depth offset in px (slides translate in Z axis)
            modifier: 1, // Effect multipler
            slideShadows: true, // Enables slides shadows
        },
        grabCursor: true,
        parallax: true,
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        breakpoints: {
            1023: {
                slidesPerView: 1,
                spaceBetween: 0,
            },
        },
        // Events
        on: {
            imagesReady: function () {
                this.el.classList.remove("loading");
            },
        },
    };
var mySwiper = new Swiper(sliderSelector, options);

// Initialize slider
mySwiper.init();



class Countdown {
    constructor(el) {
        this.el = el;
        this.targetDate = new Date(el.getAttribute("date-time"));
        this.createCountDownParts();
        this.countdownFunction();
        this.countdownLoopId = setInterval(this.countdownFunction.bind(this), 1000);
    }
    createCountDownParts() {
        ["days", "hours", "minutes", "seconds"].forEach((part) => {
            const partEl = document.createElement("div");
            partEl.classList.add("part", part);
            const textEl = document.createElement("div");
            textEl.classList.add("text");
            textEl.innerText = part;
            const numberEl = document.createElement("div");
            numberEl.classList.add("number");
            numberEl.innerText = 0;
            partEl.append(numberEl, textEl);
            this.el.append(partEl);
            this[part] = numberEl;
        });
    }

    countdownFunction() {
        const currentDate = new Date();
        if (currentDate > this.targetDate) return clearInterval(this.intervalId);
        const remaining = this.getRemaining(this.targetDate, currentDate);
        Object.entries(remaining).forEach(([part, value]) => {
            this[part].style.setProperty("--value", value);
            this[part].innerText = value;
        });
    }

    getRemaining(target, now) {
        let seconds = Math.floor((target - now) / 1000);
        let minutes = Math.floor(seconds / 60);
        let hours = Math.floor(minutes / 60);
        let days = Math.floor(hours / 24);
        hours = hours - days * 24;
        minutes = minutes - days * 24 * 60 - hours * 60;
        seconds = seconds - days * 24 * 60 * 60 - hours * 60 * 60 - minutes * 60;
        return { days, hours, minutes, seconds };
    }
}

const countdownEls = document.querySelectorAll(".countdown") || [];
countdownEls.forEach((countdownEl) => new Countdown(countdownEl));

$(function () {
    $(".carousel-item").eq(0).addClass("active");
    var total = $(".carousel-item").length;
    var current = 0;
    $("#moveRight").on("click", function () {
        var next = current;
        current = current + 1;
        setSlide(next, current);
    });
    $("#moveLeft").on("click", function () {
        var prev = current;
        current = current - 1;
        setSlide(prev, current);
    });
    function setSlide(prev, next) {
        var slide = current;
        if (next > total - 1) {
            slide = 0;
            current = 0;
        }
        if (next < 0) {
            slide = total - 1;
            current = total - 1;
        }
        $(".carousel-item").eq(prev).removeClass("active");
        $(".carousel-item").eq(slide).addClass("active");
        setTimeout(function () { }, 800);

        console.log("current " + current);
        console.log("prev " + prev);
    }
});

const ele = ".promotion-carousel";
const $window = $(window);
const viewportHeight = $window.height();

let ui = {
    promo: ele + " .promotion",
    promoText: ele + " .promo-text",
    navigationItems: ".navigation a",
};

function isOnScreen(el) {
    const viewport = {
        top: $window.scrollTop(),
    };

    viewport.bottom = viewport.top + viewportHeight;

    const bounds = el.offset();
    bounds.bottom = el.offset().top + el.outerHeight();

    return !(viewport.bottom < bounds.top || viewport.top > bounds.bottom);
}

class ScrollEvents {
    run() {
        const $promo = $(".promotion");
        const $promoText = $(".promo-text");

        function smoothScroll(target) {
            $("body, html").animate({ scrollTop: target.offset().top }, 600);
        }

        $(ui.navigationItems).on("click", (e) => {
            e.preventDefault();
            smoothScroll($(e.currentTarget.hash));
        });

        $window.on("scroll", () => {
            $(ui.promo)
                .toArray()
                .forEach((el) => {
                    const $el = $(el);
                    if (isOnScreen($el)) {
                        this.scrolly($el);
                    }
                });
            this.updateNavigation();
            this.fadeAtTop($(ui.promoText));
        });

        this.updateNavigation();
    }

    scrolly(el) {
        const topOffset = el.offset().top;
        const height = el.height();
        let scrollTop = $window.scrollTop();
        const maxPixels = height / 4;
        const percentageScrolled = (scrollTop - topOffset) / height;
        let backgroundOffset = maxPixels * percentageScrolled;

        backgroundOffset = Math.round(
            Math.min(maxPixels, Math.max(0, backgroundOffset))
        );

        el.css(`background-position`, `right 0px top ${backgroundOffset}px`);
    }

    fadeAtTop(el) {
        const startPos = 0.25;

        el.toArray().forEach((el) => {
            const $el = $(el);
            let position =
                $el.offset().top - $window.scrollTop() + viewportHeight / 6;
            let opacity =
                position < viewportHeight * startPos
                    ? (position / (viewportHeight * startPos)) * 1
                    : 1;

            $el.css("opacity", opacity);
        });
    }

    updateNavigation() {
        $(ui.promo)
            .toArray()
            .forEach((el) => {
                let $el = $(el);
                let activeSection =
                    $(`.navigation a[href="#${$el.attr("id")}"]`).data("number") - 1;

                if (
                    $el.offset().top - $window.height() / 2 < $window.scrollTop() &&
                    $el.offset().top + $el.height() - $window.height() / 2 >
                    $window.scrollTop()
                ) {
                    $(ui.navigationItems).eq(activeSection).addClass("active");
                } else {
                    $(ui.navigationItems).eq(activeSection).removeClass("active");
                }
            });
    }
}

const scrollEvents = new ScrollEvents();

scrollEvents.run();


var skipRow = 1

$(document).on("click",'#more-btn', function () {
    $.ajax({
        method: "GET",
        url: "/product/LoadMore",
        data: {
            skipRow: skipRow
        },
        success: function (result) {
            $('#best-selling-products').append(result);
            skipRow++;

        }
    })
})


// skipRowSale = 1
//$(document).on("click", '.insale-more-btn', function () {
//    $.ajax({
//        method: "GET",
//        url: "/product/LoadMoreInSale",
//        data: {
//            skipRowSale: skipRowSale
//        },
//        success: function (result) {
//            $('#insale-products').append(result);
//            skipRowSale++;

//        }
//    })
//})




