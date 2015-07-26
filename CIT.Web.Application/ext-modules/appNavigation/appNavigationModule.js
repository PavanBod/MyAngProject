"use strict";

var appNavigation = angular.module("appNavigation", ["ngAnimate"]);

appNavigation.directive('appNavigation', ['$timeout', function ($timeout) {
    return {
        transclude: true,
        scope: {

        },
        templateUrl: 'ext-modules/appNavigation/appNavigationTemplate.html',
        controller: 'appNavigationController',
        link: function (scope, el, attr) {
            var item = el.find('.appNavigationSelectableItem:first');
            $timeout(function () {
                item.trigger('click');
            });
        }
    }
}]);

appNavigation.directive('appNavigationItem', function () {
    return {
        require: '^appNavigation',
        scope: {
            label: '@',
            icon: '@',
            route: '@'
        },
        templateUrl: 'ext-modules/appNavigation/appNavigationItemTemplate.html',
        controller: 'appNavigationController',
        link: function (scope, el, attr, ctrl) {
            scope.isActive = function () {
                return el === ctrl.getActiveElement();
            };

            scope.isVertical = function () {
                return ctrl.isVertical() || el.parents('.appNavigationSubItems').length > 0;
            };

            el.on('click', function (evt) {
                evt.stopPropagation();
                evt.preventDefault();
                scope.$apply(function () {
                    ctrl.setActiveElement(el);
                    ctrl.setRoute(scope.route);
                });
            });
        }
    };
});

appNavigation.directive('appNavigationGroup', function () {
    return {
        require: '^appNavigation',
        transclude: true,
        scope: {
            label: '@',
            icon: '@'
        },
        templateUrl: 'ext-modules/appNavigation/appNavigationGroupTemplate.html',
        link: function (scope, el, attr, ctrl) {
            scope.isOpen = false;
            scope.closeNavigationGroup = function () {
                scope.isOpen = false;
            };
            scope.clicked = function () {
                scope.isOpen = !scope.isOpen;
            };
        }
    };
});

appNavigation.controller('appNavigationController', ['$scope', '$rootScope', '$timeout',
    function ($scope, $rootScope, $timeout) {
        $scope.showNavigation = true;

        this.setActiveElement = function (el) {
            $scope.activeElement = el;
        };

        this.getActiveElement = function () {
            return $scope.activeElement;
        }

        this.setRoute = function (route) {
            $rootScope.$broadcast('appNavigationItemSelectedEvent', { route: route });
        };

        $scope.$on('appNavigationShow', function (evt, data) {
            $scope.showNavigation = data.show;
        });
    }]);