if (!metro) {
    var metro = {};
}
metro.slideIn = function (e) {
    e.each(function(i,v) {
        v.css("margin-left", "-100px");
        v.animate({ opacity: 1, marginLeft: '0px' }, 1000);
    });
};
