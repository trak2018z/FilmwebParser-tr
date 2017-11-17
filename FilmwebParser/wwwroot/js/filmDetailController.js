(function () {
    "use strict";
    angular.module("app-films")
        .controller("filmDetailController", filmDetailController);
    function filmDetailController() {
        var vm = this;
        vm.name = "Damian";
    }
})();