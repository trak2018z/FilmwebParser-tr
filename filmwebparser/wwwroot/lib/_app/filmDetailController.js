!function(){"use strict";function t(t,i){var e=this;e.title=t.title,e.details=[],e.errorMessage="",e.isBusy=!0,i.get("/api/films/"+e.title).then(function(t){angular.copy(t.data,e.details)},function(t){e.errorMessage=t.data}).finally(function(){e.isBusy=!1})}t.$inject=["$routeParams","$http"],angular.module("app-films").controller("filmDetailController",t)}();