//See: http://james.padolsey.com/javascript/fun-with-jquerys-animate/

if (!chocolate) {
    var chocolate = {};
}

chocolate.displayStart = function (t) {
    var content = $('#content').stop();
    var header = $('#header').stop();

    header.css('margin-left', '20px');
    content.css('margin-left', '-40px');

    header.delay(450).animate({ opacity: 1, marginLeft: '0px' }, { duration: 'slow', easing: 'easeOutCirc' });
    content.delay(500).animate({ opacity: 1, marginLeft: '0px' }, { duration: 'slow', easing: 'easeOutCirc' });
};

chocolate.hideStart = function () {
    var content = $('#content').stop();
    var header = $('#header').stop();

    header.animate({ opacity: 0, marginLeft: '20px' }, { duration: 'fast', easing: 'easeOutCirc' });
    return content.animate({ opacity: 0, marginLeft: '-40px' }, { duration: 'fast', easing: 'easeOutCirc' });
};

chocolate.displayNavFrame = function () {
    var frame = $("#navFrame").stop();
    frame.css("-webkit-transform-origin", '50% 50%');

    var from = { angle: 1 };
    var to = { angle: 0 };

    $(from).animate(to, {
        duration: 2300,
        easing: 'easeOutCirc',
        step: function () {
            frame.css("display", "block");
            frame.css("opacity", 1);
            frame.css('-webkit-transform', "rotate3d(1,1,0,{0}deg)".format(this.angle * 33));
        }
    });
};

chocolate.hideNavFrame = function() {
    var frame = $("#navFrame").stop();
    frame.css("-webkit-transform-origin", '90% 90%');
    
    var from = { angle: 0 };
    var to = { angle: 1 };

    $(from).animate(to, {
        duration: 400,
        easing: 'easeOutCirc',
        step: function () {
            frame.css('-webkit-transform', "rotate3d(1,1,0,{0}deg)".format(this.angle * 33));
        }
    });

    $(frame).animate({ opacity: 0 }, {
        duration: 400, complete: function () {
            frame.css('display', 'none');
            frame.css('opacity', 0);
            chocolate.displayStart();
        }
    });
};

chocolate.goBack = function () {
    chocolate.hideNavbar();
    chocolate.hideNavFrame();
};

chocolate.hideNavbar= function () {
    var bar = $('#navBar').stop();
    bar.animate({ left: -101 }, { duration: 400, easing: 'easeOutExpo' }, function () {
        bar.css("display", "none");
    });
};

chocolate.displayNavbar = function () {
    var bar = $('#navBar').stop();
    bar.css("display", "block");
    bar.delay(400).animate({ left: 0 }, { duration: 2000, easing: 'easeOutQuint' });
};

chocolate.navigate = function (url) {
    chocolate.hideStart();
    chocolate.displayNavFrame();
    chocolate.displayNavbar();
};
