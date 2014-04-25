// home-index.js

var module = angular.module("homeIndex", ['ngRoute']);

module.config(function ($routeProvider) {
  $routeProvider.when("/", {
    controller: "topicsController",
    templateUrl: "/templates/topicsView.html"
  });

  $routeProvider.otherwise({ redirectTo: "/" });
});

function topicsController($scope, $http) {
  $scope.dataCount = 0;
  $scope.isBusy = true;
  $scope.data = [];

  $http.get("/api/topics?includeReplies=true")
    .then(
      function (result) {
        // Success
        angular.copy(result.data, $scope.data);
      },
      function () {
        // Error
        alert("Could not load topics");
      })
     .then(function () {
       $scope.isBusy = false;
     });
}