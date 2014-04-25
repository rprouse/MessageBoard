// home-index.js

var module = angular.module("homeIndex", ['ngRoute']);

module.config(function ($routeProvider) {
  $routeProvider.when("/", {
    controller: "topicsController",
    templateUrl: "/templates/topicsView.html"
  });

  $routeProvider.when("/newmessage", {
    controller: "newTopicController",
    templateUrl: "/templates/newTopicView.html"
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

function newTopicController($scope, $http, $window) {
  $scope.newTopic = {};

  $scope.save = function () {
    $http.post("/api/topics", $scope.newTopic)
    .then(
      function (result) {
        // Success
        var newTopic = result.data;
        // TODO: Merge with existing list of topics
        $window.location = "#/";
      },
      function () {
        // Error
        alert("Could not save topic");
      });
  };
}