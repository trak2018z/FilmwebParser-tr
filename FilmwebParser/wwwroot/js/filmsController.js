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
                vm.errorMessage = error.data;
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
                    vm.errorMessage = error.data;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
        vm.downloadJson = function (filmTitle) {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.get("/api/films/" + filmTitle)
                .then(function (response) {
                    download(response.data, filmTitle + '.json', 'text/json');
                },
                function (error) {
                    vm.errorMessage = error.data;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();

function download(jsonData, fileName, type) {
    var readyJson = JSON.stringify(jsonData);
    var a = document.createElement("a");
    var file = new Blob([readyJson], { type: type });
    a.href = URL.createObjectURL(file);
    a.download = fileName;
    a.click();
}