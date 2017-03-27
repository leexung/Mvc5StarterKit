$(function () {

    var observer = new MutationObserver(function (mutations) {
        mutations.forEach(function (mutation) {
            //console.log($(mutation.removedNodes)); // <<-- includes text nodes

            $("#progressLoader").hide();
            $("#progressLoaderText").hide();
            console.log("> Loader removed...")
            $(mutation.removedNodes).each(function (value, index) {
                if (this.nodeType === 1) {
                    console.log(this)
                }
            });
        });
    });

    var config = { attributes: true, childList: true, characterData: true };

    var divId = "'#izenda-root'";

    if (document.getElementById(divId))
    {
        observer.observe($(divId)[0], config);
    }
});