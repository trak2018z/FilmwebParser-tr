function download(n,i,e){var t=JSON.stringify(n),s=document.createElement("a"),o=new Blob([t],{type:e});s.href=URL.createObjectURL(o),s.download=i,s.click()}!function(){"use strict";function n(n){var i=this;i.films=[],i.newFilm={},i.errorMessage="",i.isBusy=!0,n.get("/api/films").then(function(n){angular.copy(n.data,i.films)},function(n){i.errorMessage=n.data}).finally(function(){i.isBusy=!1}),i.addFilm=function(){i.isBusy=!0,i.errorMessage="",n.post("/api/films",i.newFilm).then(function(n){i.films.push(n.data),i.newFilm={}},function(n){i.errorMessage=n.data}).finally(function(){i.isBusy=!1})},i.downloadJson=function(e){i.isBusy=!0,i.errorMessage="",n.get("/api/films/"+e).then(function(n){download(n.data,e+".json","text/json")},function(n){i.errorMessage=n.data}).finally(function(){i.isBusy=!1})}}n.$inject=["$http"],angular.module("app-films").controller("filmsController",n)}();