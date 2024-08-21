mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
  scrollFunction();
};

function scrollFunction() {
  if (
    document.body.scrollTop > 200 ||
    document.documentElement.scrollTop > 200
  ) {
    mybutton.style.display = "block";
  } else {
    mybutton.style.display = "none";
  }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
  document.body.scrollTop = 0; // For Safari
  document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}

/*  Main Slider js
    /*----------------------------------------------------*/
    var owl = $(".owl-carousel");
    $(document).ready(function () {
      $(".owl-carousel-imgslides").owlCarousel();
    });
    //Init the carousel
initSlider();
function initSlider() {
    $(".owl-carousel-slider").owlCarousel({
      loop: true,
      dots: false,
      nav: true,
      navText: [
        "<i class='fas fa-long-arrow-alt-up'></i>",
        "<i class='fas fa-long-arrow-alt-down'></i>",
      ],
        mouseDrag: true,
        autoplay: true,
      autoplayTimeout: 10000,
      touch: true,
      animateOut: 'fadeOut',
    animateIn: 'slideInLeft',
    //smartSpeed:6000,
    // onInitialized: startProgressBar,
    // onTranslate: resetProgressBar,
    // onTranslated: startProgressBar,
      responsive: {
        0: {
          items: 1,
          dots: false,
        },
        600: {
          items: 1,
          dots: false,
        },
        1000: {
          items: 1,
        },
      },
    });
    /*-----images inside the slider-----*/
    $('.owl-carousel-imgslides').owlCarousel({
      loop: true,
      dots: false,
      nav: false,
      mouseDrag: false,
      autoplay: true,
      autoplayTimeout: 10000,
      touch: false,
      animateIn: "slideInLeft", // add this
      animateOut: "fadeOut", // and this
    //   onInitialized: startProgressBar,
    // onTranslate: resetProgressBar,
    // onTranslated: startProgressBar,
      responsive: {
        0: {
          items: 1,
          dots: false,
        },
        600: {
          items: 1,
          dots: false,
        },
        1000: {
          items: 1,
        },
      },
    })
    

// owl.on('change.owl.carousel', function (event) {
//   var el = event.target;
//   $('h1', el).addClass('slideInRight animated')
//           .one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
//               $('h1', el).removeClass('slideInRight animated');
//           });
//       });
  }
  /*-------animation slider header----*/
 //  owl.on('changed.owl.carousel', function(event) {
 //    var item = event.item.index - 2;     // Position of the current item
 //    $('h1').removeClass('wow fadeInLeft');
 //$('.owl-item').not('.cloned').eq(item).find('h1').addClass('wow fadeInLeft');
 //});
     //owl.on(".owl-stage", function (e) {
     //  if (e.deltaY > 0) {
     //    owl.trigger("next.owl");
     //  } else {
     //    owl.trigger("prev.owl");
     //  }
     //  e.preventDefault();
     //});
    /*-- linke The main slider with img slide ---*/
  var o1 = $('#c1'), o2 = $('#c2');
 //Sync o2 by o1
 o1.on('click', '.owl-next', function () {
 	o2.trigger('next.owl.carousel')
 });
 o1.on('click', '.owl-prev', function () {
 	o2.trigger('prev.owl.carousel')
 });
 

/*-----Prograss bar function ----*/

// function startProgressBar() {
//   // apply keyframe animation
//   $(".slide-progress").css({
//     width: "100%",
//     transition: "width 5000ms"
//   });
// }

// function resetProgressBar() {
//   $(".slide-progress").css({
//     width: 0,
//     transition: "width 0s"
//   });
// }



    //----owl-carousel client & projects & project-details-----//

//$(document).ready(function () {
//  $(".owl-carousel_clients").owlCarousel();
//});
//$('.owl-carousel_clients').owlCarousel({
//  rtl: true,
//  loop: true,
//  margin: 10,
//  dots:false,
//  autoplay: true,
//  responsiveClass: true,
//  responsive: {
//    0: {
//      items: 1,
//      nav: false
//      },
//      366: {
//          items: 2,
//          nav: false
//      },
//      768: {
//          items: 3,
//          nav: false
//      },
//    1000: {
//      items: 5,
//      nav: false,
//      loop: true,
//      autoplay: true
//    }
//  }
//})

$(document).ready(function () {
    $('.customer-logos').slick({
        slidesToShow:4,
        //slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 500,
        arrows: true,
        dots: true,
        pauseOnHover: true,
        responsive: [{
            breakpoint: 768,
            settings: {
                slidesToShow: 3
            }
        }, {
            breakpoint: 520,
            settings: {
                slidesToShow: 2
            }
        }]
    });
});

$(document).ready(function () {
  $(".owl-carousel_projects").owlCarousel();
});
$('.owl-carousel_projects').owlCarousel({
  loop: true,
  dots: false,
  margin: 10,
  autoplay: false,
  responsiveClass: true,
  nav: true,
  navText: [
    "<i class='fas fa-angle-double-right'></i>",
    "<i class='fas fa-angle-double-left'></i>",
      ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 2,
    },
    1000: {
      items: 3,
    }
  }
})
$(document).ready(function () {
  $(".owl-carousel_pro_details").owlCarousel();
});
$('.owl-carousel_pro_details').owlCarousel({
  loop: true,
  dots: false,
  nav: true,
  navText: [
    "<i class='fas fa-long-arrow-alt-down'></i>",
    "<i class='fas fa-long-arrow-alt-up'></i>",
  ],
  mouseDrag: true,
  autoplay: false,
  autoplayTimeout: 12000,
  touch: true,
  animateIn: "fadeIn", // add this
  animateOut: "fadeOut", // and this
  responsive: {
    0: {
      items: 1,
      dots: false,
    },
    600: {
      items: 1,
      dots: false,
    },
    1000: {
      items: 1,
    },
  },
})





/*------------open & close nav------*/
/*----------sidenav----------*/
var navArrow = $(".owl-dots");
var navArrow2 = $(".owl-nav button");
function openNav() {
  document.querySelectorAll(".sidenav").forEach((item) => {
    item.style.width = "100%";
    navArrow.css("z-index", "-1");
    navArrow2.css("z-index", "-1");
  });
}
function closeNav() {
  document.querySelectorAll(".sidenav").forEach((item) => {
    item.style.width = "0";
    navArrow.css("z-index", "0");
    navArrow2.css("z-index", "1");
  });
}

/*-----------Modal Slider Gallery----*/
let modalId = $('#image-gallery');

$(document)
  .ready(function () {

    loadGallery(true, 'a.thumbnail');

    //This function disables buttons when needed
    function disableButtons(counter_max, counter_current) {
      $('#show-previous-image, #show-next-image')
        .show();
      if (counter_max === counter_current) {
        $('#show-next-image')
          .hide();
      } else if (counter_current === 1) {
        $('#show-previous-image')
          .hide();
      }
    }

    /**
     *
     * @param setIDs        Sets IDs when DOM is loaded. If using a PHP counter, set to false.
     * @param setClickAttr  Sets the attribute for the click handler.
     */

    function loadGallery(setIDs, setClickAttr) {
      let current_image,
        selector,
        counter = 0;

      $('#show-next-image, #show-previous-image')
        .click(function () {
          if ($(this)
            .attr('id') === 'show-previous-image') {
            current_image--;
          } else {
            current_image++;
          }

          selector = $('[data-image-id="' + current_image + '"]');
          updateGallery(selector);
        });

      function updateGallery(selector) {
        let $sel = selector;
        current_image = $sel.data('image-id');
        $('#image-gallery-title')
          .text($sel.data('title'));
        $('#image-gallery-image')
          .attr('src', $sel.data('image'));
        disableButtons(counter, $sel.data('image-id'));
      }

      if (setIDs == true) {
        $('[data-image-id]')
          .each(function () {
            counter++;
            $(this)
              .attr('data-image-id', counter);
          });
      }
      $(setClickAttr)
        .on('click', function () {
          updateGallery($(this));
        });
    }
  });

// build key actions
$(document)
  .keydown(function (e) {
    switch (e.which) {
      case 37: // left
        if ((modalId.data('bs.modal') || {})._isShown && $('#show-previous-image').is(":visible")) {
          $('#show-previous-image')
            .click();
        }
        break;

      case 39: // right
        if ((modalId.data('bs.modal') || {})._isShown && $('#show-next-image').is(":visible")) {
          $('#show-next-image')
            .click();
        }
        break;

      default:
        return; // exit this handler for other keys
    }
    e.preventDefault(); // prevent the default action (scroll / move caret)
  });
