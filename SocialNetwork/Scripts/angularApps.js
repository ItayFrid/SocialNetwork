
var MainApp = angular.module('MainApp', []);
MainApp.controller('teacherSearch', function ($scope, $http) {
    $http.get('/Student/GetTeachersJson')
        .then(function (result) {
            $scope.teachers = result.data;
        })
        .catch(function (data) {
            console.log(data);
        });
});
MainApp.controller('courseSearch', function ($scope, $http) {
    $http.get('/Student/getCoursesJson')
        .then(function (result) {
            $scope.courses = result.data;
        })
        .catch(function (data) {
            console.log(data);
        });
});
MainApp.filter('searchFor', function () {
    return function (arr, searchString) {
        if (!searchString) {
            return arr;
        }
        var result = [];
        searchString = searchString.toLowerCase();
                angular.forEach(arr, function (item) {
                if (item.name.toLowerCase().indexOf(searchString) !== -1) {
                    result.push(item);
                }
            });
        return result;
    };
});

MainApp.controller('Messages', function ($scope, $http) {
    $http.get('/Message/getMessagesJson')
        .then(function (result) {
            $scope.messages = result.data;
        })
        .catch(function (data) {
            console.log(data);
        });
});