"use strict";

var appHeader = angular.module("appHeader");

appHeader.directive('appUserProfile', ['$rootScope',
    function ($rootScope) {
    return {
        templateUrl: 'ext-modules/appUserProfile/appUserProfileTemplate.html',
        scope: {
            userName: '@'
        }
    }
}]);