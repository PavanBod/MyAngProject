"use strict";

var citApp = angular.module("citApp", ["ngRoute", "appHeader", "ngStorage"]);

citApp.config(function ($provide) {
    $provide.decorator("$exceptionHandler", ["$delegate", function ($delegate) {
        return function (exception, cause) {
            $delegate(exception, cause);
            alert(exception.message);
        };
    }]);
});

citApp.config(['$routeProvider', function ($routeProvider) {
    var routes = [
        {
            url: '/myPosts',
            config: {
                templateUrl: '/citApp/myPosts/myPostsTemplate.html',
                controller: 'myPostsController'
            }
        },
        {
            url: '/otherPosts',
            config: {
                templateUrl: '/citApp/otherPosts/otherPostsTemplate.html',
                controller: 'otherPostsController'
            }
        },
        {
            url: '/help',
            config: {
                templateUrl: '/citApp/help/helpTemplate.html',
                controller: 'helpController'
            }
        },
        {
            url: '/contact',
            config: {
                templateUrl: '/citApp/contact/contactTemplate.html',
                controller: 'contactController'
            }
        }
    ];

    routes.forEach(function (route) {
        $routeProvider.when(route.url, route.config);
    });

    $routeProvider.otherwise({ redirectTo: '/myPosts' });
}]);

citApp.controller('citAppController',
    ['$scope', '$sessionStorage','$rootScope',
    function ($scope, $sessionStorage, $rootScope) {
        $scope.$storage = $sessionStorage.$default({
            userProfile: {
                userName: '',
                isAuthenticated: false
            }
        });
        $scope.authorize = function () {
            $rootScope.userName = $scope.$storage.userProfile.userName;
            $scope.$storage.userProfile.isAuthenticated = true;
        };


    }]);