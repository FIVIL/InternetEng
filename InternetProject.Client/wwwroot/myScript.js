Blazor.registerFunction('setCookie', function (cname, cvalue, exsec) {
    var d = new Date();
    d.setTime(d.getTime() + (exsec * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
});
Blazor.registerFunction('getCookie', function (cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
});
Blazor.registerFunction('Alert', function (text) {
    alert(text);
});
Blazor.registerFunction('ModalDisplay', function (id, display) {
    document.getElementById(id).style.display = display;
});
Blazor.registerFunction('Confirm', function (display) {
    return confirm(display);
});
window.onscroll = function () { myFunction() };

function myFunction() {
    var winScroll = document.body.scrollTop || document.documentElement.scrollTop;
    var height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
    var scrolled = (winScroll / height) * 100;
    document.getElementById("myBar").style.width = scrolled + "%";
}
Blazor.registerFunction('GetSliderValue', function (name) {
    var slider = document.getElementById(name);
    return slider.valueHigh * 1 + ";" + slider.valueLow * 1;
});
Blazor.registerFunction('SliderBinder', function (sliderID, bindID, scale, intercept) {
    var slider = document.getElementById(sliderID);
    var bind1 = document.getElementById(bindID);
    slider.onchange = function () {
        var Max = (this.valueHigh * scale) + intercept;
        var Min = (this.valueLow * scale) + intercept;
        bind1.innerHTML = "From: " + Min + " To: " + Max;
        return true;
    };
});
Blazor.registerFunction('MultiSliderActive', function () {
    MultiSliderActiver();
});
Blazor.registerFunction('Toast', function (Message, Type) {
    nativeToast({
        message: Message,
        position: 'top',
        type: Type
    });
    return true;
});
$(document).ready(function () {
    $(window).scroll(function () {
        var scrollPercent = 100 * $(window).scrollTop() / ($(document).height() - $(window).height());
        if (scrollPercent > 30) {
            Blazor.invokeDotNetMethod({
                type: {
                    assembly: 'InternetProject.Client',
                    name: 'InternetProject.Client.Statics'
                },
                method: {
                    name: 'Scroll'
                }
            });
        }
    });
});
Blazor.registerFunction('InitSlider', function () {
    init();
});
function init() {
    $('.slider').each(function () {
        var $this = $(this);
        var $group = $this.find('.slide_group');
        var $slides = $this.find('.slide');
        var bulletArray = [];
        var currentIndex = 0;
        var timeout;

        function move(newIndex) {
            var animateLeft, slideLeft;

            advance();

            if ($group.is(':animated') || currentIndex === newIndex) {
                return;
            }

            bulletArray[currentIndex].removeClass('active');
            bulletArray[newIndex].addClass('active');

            if (newIndex > currentIndex) {
                slideLeft = '100%';
                animateLeft = '-100%';
            } else {
                slideLeft = '-100%';
                animateLeft = '100%';
            }

            $slides.eq(newIndex).css({
                display: 'block',
                left: slideLeft
            });
            $group.animate({
                left: animateLeft
            }, function () {
                $slides.eq(currentIndex).css({
                    display: 'none'
                });
                $slides.eq(newIndex).css({
                    left: 0
                });
                $group.css({
                    left: 0
                });
                currentIndex = newIndex;
            });
        }

        function advance() {
            clearTimeout(timeout);
            timeout = setTimeout(function () {
                if (currentIndex < ($slides.length - 1)) {
                    move(currentIndex + 1);
                } else {
                    move(0);
                }
            }, 4000);
        }

        $('.next_btn').on('click', function () {
            if (currentIndex < ($slides.length - 1)) {
                move(currentIndex + 1);
            } else {
                move(0);
            }
        });

        $('.previous_btn').on('click', function () {
            if (currentIndex !== 0) {
                move(currentIndex - 1);
            } else {
                move(3);
            }
        });

        $.each($slides, function (index) {
            var $button = $('<a class="slide_btn">&bull;</a>');

            if (index === currentIndex) {
                $button.addClass('active');
            }
            $button.on('click', function () {
                move(index);
            }).appendTo('.slide_buttons');
            bulletArray.push($button);
        });

        advance();
    });
}
Blazor.registerFunction('Prompt', function () {
    return prompt("Pleace Enter Mail Address.", "hammedmohamadi@gmail.com");
});
var Filess = "";
Blazor.registerFunction('InitFile', function (inputname) {
    Filess = "";
    document.getElementById(inputname).addEventListener('change', function () {

        //var reader = new FileReader();
        //reader.onload = function () {

        //    var arrayBuffer = this.result,
        //        array = new Uint8Array(arrayBuffer),
        //        binaryString = String.fromCharCode.apply(null, array);
        //    var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(array)));
        //    Filess += (base64String + ';');
        //    //alert(Filess.length);
        //}
        //reader.readAsArrayBuffer(this.files[0]);
        //alert(this.files.length);
        for (var i = 0; i < this.files.length; i++) {
            var reader = new FileReader();
            reader.onload = function () {

                var arrayBuffer = this.result,
                    array = new Uint8Array(arrayBuffer),
                    binaryString = String.fromCharCode.apply(null, array);
                var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(array)));
                Filess += (base64String + ';');
            }
            reader.readAsArrayBuffer(this.files[i]);
        }
    }, false);
});
Blazor.registerFunction('GetFile', function () {
    return Filess;
});
Blazor.registerFunction('recaptcha', function () {
    grecaptcha.render('g-recaptcha', {
        'sitekey': '6LeI7mIUAAAAAHLU5yDd4HZAeBRV-Mj6YuxOqz-t',
        'callback': function (vale) {
            recap = vale;
        }
    });
});
var recap = '';
Blazor.registerFunction('recaptchaValue', function () {
    return recap;
});