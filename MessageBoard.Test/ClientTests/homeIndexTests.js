/// <reference path="../scripts/jasmine.js" />
/// <reference path="../../messageboard/scripts/angular.js" />
/// <reference path="../../messageboard/scripts/angular-mocks.js" />
/// <reference path="../../messageboard/scripts/angular-route.js" />
/// <reference path="../../messageboard/js/home-index.js" />

describe("home-index Tests ->", function () {

  beforeEach(function () {
    module("homeIndex");
  });

  var $httpBackend;

  beforeEach(inject(function ($injector) {
    $httpBackend = $injector.get("$httpBackend");
    $httpBackend.when("GET", "/api/topics?includeReplies=true")
      .respond([
        {
          title: "title 1",
          body: "body",
          id: 1,
          created: "20050401"
        },
        {
          title: "title 2",
          body: "body",
          id: 2,
          created: "20050402"
        },
        {
          title: "title 3",
          body: "body",
          id: 3,
          created: "20050403"
        }]);
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  describe("dataService ->", function () {

    it("can load topics", inject(function (dataService) {
      expect(dataService.topics).toEqual([]);
      
      $httpBackend.expectGET("/api/topics?includeReplies=true")
      dataService.getTopics();
      $httpBackend.flush();
      expect(dataService.topics.length).toBeGreaterThan(0);
      expect(dataService.topics.length).toEqual(3);
    }));
  });

  describe("topicsController ->", function () {

    it("loads data", inject(function($controller, $http, dataService) {
      var theScope = {};

      $httpBackend.expectGET("/api/topics?includeReplies=true")
      var ctrl = $controller("topicsController", {
        $scope: theScope,
        $http: $http,
        dataService: dataService
      });
      $httpBackend.flush();

      expect(ctrl).not.toBeNull();
      expect(theScope.data).toBeDefined();
    }));
  });
});