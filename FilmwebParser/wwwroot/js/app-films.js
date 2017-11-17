(function () {
    "use strict";
    angular.module("app-films", ["simpleControls", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "filmsController",
                controllerAs: "vm",
                templateUrl: "/views/filmsView.html"
            });
            $routeProvider.when("/details", {
                controller: "filmDetailController",
                controllerAs: "vm",
                templateUrl: "/views/filmDetailView.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();