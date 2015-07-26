var citApp = angular.module("citApp", []);

citApp.controller("loginController", function ($scope, citServices, $window, citUserProfile) {
    $scope.userId = '';
    $scope.password = '';

    $scope.authenticateUser = function () {
        var promise = citServices.authenticateUser($scope.userId, $scope.password);

        promise.then(
            function (payload) {
                citUserProfile.setUserData($scope.userId, true);
                $window.location.href = "/Posts.html";
            },
            function (errorPayLoad) {
                citUserProfile.setUserData($scope.userId, false);
                alert(errorPayLoad);
            }
        );
    }
});

citApp.controller("postController", function ($scope, citServices, citUserProfile) {
    $scope.userName = citUserProfile.userName;
});

citApp.controller("registerController", function ($scope, citServices) {
    $scope.email = '';
    $scope.password = '';
    $scope.firstName = '';
    $scope.lastName = '';
    $scope.telephone = '';

    // Validate Model before sending back to Backend.....

    $scope.registerUser = function () {
        var promise = citServices.registerUser($scope.firstName, $scope.lastName, $scope.email, $scope.password, $scope.telephone);

        promise.then(
            function (payload) {
                alert(payload.UserId + " is successfully created");
            },
            function (errorPayLoad) {
                alert(errorPayLoad);
            }
        );
    }
});

citApp.factory('citServices', function ($http, $log, $q) {
    var makeBackendCall = function (url, method, data) {
        var deferred = $q.defer();

        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
        delete $http.defaults.headers.common['X-Requested-With'];

        $http({
            url: url,
            method: method,
            data: data
        })
        .success(function (data, code) {
            deferred.resolve(data);
        })
        .error(function (msg, code) {
            if (code == 401)
                deferred.reject("Unauthorized");
            else
                deferred.reject("Application Error");
        });
        return deferred.promise;
    }
    return {
        authenticateUser: function (userId, password) {
            var url = "http://localhost:49195/api/MySiteUsers/AuthenticateUser",
                data = $.param({ "userId": userId, "password": password }),
                method = "post";
            return makeBackendCall(url, method, data);
        },
        registerUser: function (firstName, lastName, email, password, telephone) {
            var url = "http://localhost:49195/api/MySiteUsers",
                data = $.param({ "userId": email, "password": password, "firstName": firstName, "lastName": lastName, "telephone": telephone, "email": email }),
                method = "post";
            return makeBackendCall(url, method, data);
        }
    }
});

citApp.factory('citUserProfile', function () {
    var userProfile = {};
    userProfile.userName = "";
    userProfile.email = "";
    userProfile.telephone = "";
    userProfile.isAuthenticated = false;
    userProfile.setUserData = function (userName, isAuthenticated)
    {
        this.userName = userName;
        this.isAuthenticated = isAuthenticated;
    }
    return userProfile;
});