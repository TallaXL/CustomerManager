(function () {
    'use strict';
    angular
        .module('customersApp', ['ngRoute', 'ngMaterial', 'ngMessages', 'ui.bootstrap'])
        .config(['$routeProvider', function ($routeProvider) {
            var viewBase = '/app/customerApp/views/';

            $routeProvider
                .when('/search', {
                    controller: 'customerController',
                    templateUrl: viewBase + 'search.html',
                    controllerAs: 'ctrl'
                })
                .when('/edit', {
                    controller: 'customerEditController',
                    templateUrl: viewBase + 'edit.html',
                    controllerAs: 'ctrl'
                })
                .when('/about', {
                    controller: 'aboutController',
                    templateUrl: viewBase + 'about.html',
                    controllerAs: 'ctrl'
                })
                .otherwise({ redirectTo: '/search' });
        }])
})();