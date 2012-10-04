if (!chocolate) {
    var chocolate = {};
}

chocolate.displayStart = function (delay) {
    var content = $('#content').stop();
    var header = $('#header').stop();

    header.css('margin-left', '20px');
    content.css('margin-left', '-40px');

    var delayTime = delay ? 450 : 0;
    
    header.delay(delayTime).animate({ opacity: 1, marginLeft: '0px' }, { duration: 'slow', easing: 'easeOutCirc' });
    content.delay(delayTime).animate({ opacity: 1, marginLeft: '0px' }, { duration: 'slow', easing: 'easeOutCirc' });
};

chocolate.hideStart = function () {
    var content = $('#content').stop();
    var header = $('#header').stop();

    header.animate({ opacity: 0, marginLeft: '20px' }, { duration: 'fast', easing: 'easeOutCirc' });
    return content.animate({ opacity: 0, marginLeft: '-40px' }, { duration: 'fast', easing: 'easeOutCirc' });
};

chocolate.displayNavbar = function () {
    var bar = $('#navBar').stop();
    bar.css("display", "block");
    bar.delay(400).animate({ left: 0 }, { duration: 400, easing: 'easeOutCirc' });
};

chocolate.displayNavFrame = function (color, callback) {
    var frame = $("#navFrame").stop();
    frame.css("-webkit-transform-origin", '90% 50%');
    if (color) {
        frame.css("background-color", color);
    } else {
        frame.css("background-color", 'transparent');
    }

    if (window) {
        window.open("about:blank", "navFrame");
    }

    // See: http://james.padolsey.com/javascript/fun-with-jquerys-animate/
    var from = { angle: 0 };
    var to = { angle: 1 };

    $(from).animate(to, {
        duration: 400,
        easing: 'easeOutExpo',
        step: function () {
            frame.css("display", "block");
            frame.css("opacity", 1);
            frame.css('-webkit-transform', "scale({0})".format(this.angle));
        },
        complete:function () {
            if (callback) {
                callback();
            }
        }
    });
};

chocolate.hideNavFrame = function() {
    var frame = $("#navFrame").stop();
    frame.css("-webkit-transform-origin", '90% 50%');

    var from = { angle: 0 };
    var to = { angle: 1 };

    $(from).animate(to, {
        duration: 400,
        easing: 'easeOutCirc',
        step: function () {
            frame.css('-webkit-transform', "scale({0})".format(1 - this.angle));
        }
    });

    $(frame).animate({ opacity: 0 }, {
        duration: 400, complete: function () {
            chocolate.displayStart(false);
            if (window) {
                window.open("about:blank", "navFrame");
            }
        }
    });
};

chocolate.goBack = function () {
    chocolate.hideNavbar();
    chocolate.hideNavFrame();
};

chocolate.hideNavbar= function () {
    var bar = $('#navBar').stop();
    bar.animate({ left: -101 }, { duration: 400, easing: 'easeOutCirc' }, function () {
        bar.css("display", "none");
    });
};


chocolate.navigate = function (url, color) {
    chocolate.hideStart();
    chocolate.displayNavFrame(color, function () {
        if (window) {
            window.open(url, "navFrame");
        }
    });
    
    chocolate.displayNavbar();
};
