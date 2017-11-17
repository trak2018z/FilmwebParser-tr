(function () {
    "use strict";
    angular.module("app-films")
        .controller("filmDetailController", filmDetailController);
    function filmDetailController($routeParams, $http) {
        var vm = this;
        vm.title = $routeParams.title;
        vm.details = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        $http.get("/api/films/" + vm.title)
            .then(function (response) {
                angular.copy(response.data, vm.details);
            },
            function (error) {
                vm.errorMessage = "Wystąpił błąd podczas pobierania danych: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
    }
})();