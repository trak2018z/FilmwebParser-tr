(function () {
    "use strict";
    angular.module("app-films")
        .controller("filmsController", filmsController);
    function filmsController($http) {
        var vm = this;
        vm.films = [];
        vm.newFilm = {};
        vm.errorMessage = "";
        vm.isBusy = true;
        $http.get("/api/films")
            .then(function (response) {
                angular.copy(response.data, vm.films);
            },
            function (error) {
                vm.errorMessage = "Wystąpił błąd podczas pobierania danych: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
        vm.addFilm = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/films", vm.newFilm)
                .then(function (response) {
                    vm.films.push(response.data);
                    vm.newFilm = {};
                },
                function (error) {
                    vm.errorMessage = "Wystąpił błąd podczas zapisywania danych: " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();