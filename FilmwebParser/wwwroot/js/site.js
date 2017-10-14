(function () {
    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Pokaż panel");
        } else {
            $(this).text("Ukryj panel");
        }
    });
})();