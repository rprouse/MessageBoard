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

  $routeProvider.when("/message/:id", {
    controller: "singleTopicController",
    templateUrl: "/templates/singleTopicView.html"
  });

  $routeProvider.otherwise({ redirectTo: "/" });
});

module.factory("dataService", function ($http, $q) {
  var _topics = [];
  var _isInit = false;

  var _isReady = function () {
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

  var _getTopicById = function (id) {
    var deferred = $q.defer();

    if (_isReady()) {
      var topic = _findTopic(id);
      if (topic) {
        deferred.resolve(topic);
      } else {
        deferred.reject();
      }
    } else {
      _getTopics()
        .then(function () {
          // Success
          var topic = _findTopic(id);
          if (topic) {
            deferred.resolve(topic);
          } else {
            deferred.reject();
          }
        },
          function () {
            // Error
            deferred.reject();
          });
    }
    return deferred.promise;
  };

  function _findTopic(id) {
    var found = null;

    $.each(_topics, function (i, item) {
      if (item.id === id) {
        found = item;
        return false;
      }
    });

    return found;
  }

  var _saveReply = function (topic, newReply) {
    var deferred = $q.defer();

    $http.post("/api/topics/" + topic.id + "/replies", newReply)
        .then(function (result) {
          // Success
          if (topic.replies === null) topic.replies = [];
          topic.replies.push(result.data);
          deferred.resolve(result.data);
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
    addTopic: _addTopic,
    getTopicById: _getTopicById,
    saveReply: _saveReply
  };
});

function topicsController($scope, $http, dataService) {
  $scope.dataCount = 0;
  $scope.isBusy = false;
  $scope.data = dataService;

  if (dataService.isReady() === false) {
    $scope.isBusy = true;
    dataService.getTopics()
      .then(function () {
        // Success
      }, function () {
        // Error
        alert("Could not get the topics");
      })
      .then(function () {
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

function singleTopicController($scope, $window, dataService, $routeParams) {
  $scope.topics = null;
  $scope.newReply = {};

  dataService.getTopicById($routeParams.id)
    .then(function (topic) {
      // success
      $scope.topic = topic;
    },
    function () {
      // error
      $window.location = "#/";
    });

  $scope.addReply = function () {
    dataService.saveReply($scope.topic, $scope.newReply)
    .then(function (topic) {
      // success
      $scope.newReply.body = "";
    },
    function () {
      // error
      alert("Could not save the new reply");
    });
  };
}