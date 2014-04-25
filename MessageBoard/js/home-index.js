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

module.factory("dataService", function ($http, $q) {
  var _topics = [];
  var _isInit = false;

  var _isReady = function() {
    return _isInit;
  };

  var _getTopics = function () {
    var deferred = $q.defer();

    $http.get("/api/topics?includeReplies=true")
      .then(
        function (result) {
          // Success
          angular.copy(result.data, _topics);
          _isInit = true;
          deferred.resolve();
        },
        function () {
          // Error
          deferred.reject();
        });

    return deferred.promise;
  };

  var _addTopic = function (newTopic) {
    var deferred = $q.defer();

    $http.post("/api/topics", newTopic)
    .then(
      function (result) {
        // Success
        var createdTopic = result.data;
        // Merge with existing list of topics
        _topics.splice(0, 0, createdTopic);
        deferred.resolve(createdTopic);
      },
      function () {
        // Error
        deferred.reject();
      });

    return deferred.promise;
  };

  return {
    topics: _topics,
    isReady: _isReady,
    getTopics: _getTopics,
    addTopic: _addTopic
  };
});

function topicsController($scope, $http, dataService) {
  $scope.dataCount = 0;
  $scope.isBusy = false;
  $scope.data = dataService;

  if (dataService.isReady() == false) {
    $scope.isBusy = true;
    dataService.getTopics()
      .then(function() {
        // Success
      }, function() {
        // Error
        alert("Could not get the topics");
      })
      .then(function() {
        $scope.isBusy = false;
      });
  }
}

function newTopicController($scope, $http, $window, dataService) {
  $scope.newTopic = {};

  $scope.save = function () {
    dataService.addTopic($scope.newTopic)
    .then(
      function () {
        $window.location = "#/";
      },
      function () {
        alert("Could not save topic");
      });
  };
}