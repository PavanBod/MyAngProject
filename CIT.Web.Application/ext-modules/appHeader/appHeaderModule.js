"use strict";

var appHeader = angular.module("appHeader", ["appNavigation", "appContent"]);

appHeader.directive("appHeader", function () {
    return {
        transclude: true,
        scope: {
            title: '@',
            subtitle: '@',
            iconFile: '@'
        },
        controller: "appHeaderController",
        templateUrl: "ext-modules/appHeader/appHeaderTemplate.html"
    };
});

appHeader.controller("appHeaderController",
    ['$scope', '$window', '$timeout', '$rootScope', '$location',
    function ($scope, $window, $timeout, $rootScope, $location) {
        $scope.isNavigationButtonVisible = true;
        $scope.isNavigationAreaVisible = true;

        $($window).on('resize.appHeader', function () {
            $scope.$apply(function () {
                checkWidth();
                broadcastNavigationState();
            });
        });

        $scope.$on("$destroy", function () {
            $($window).off("resize.appHeader");
        });

        var checkWidth = function () {
            var width = Math.max($($window).width(), $window.innerWidth);
            $scope.isNavigationAreaVisible = (width > 767);
            $scope.isNavigationButtonVisible = !$scope.isNavigationAreaVisible;
        };

        $scope.navigationButtonClicked = function () {
            $scope.isNavigationAreaVisible = !$scope.isNavigationAreaVisible;
            broadcastNavigationState();
            //$scope.$apply();
        };

        var broadcastNavigationState = function () {
            $rootScope.$broadcast('appNavigationShow', {
                show: $scope.isNavigationAreaVisible
            });
        }

        $timeout(function () {
            checkWidth();
        }, 0);

        $scope.$on('appNavigationItemSelectedEvent', function (evt, data) {
            $scope.routeString = data.route;
            $location.path(data.route);
            checkWidth();
            broadcastNavigationState();
        });
    }]);