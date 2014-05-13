using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using MessageBoard.Controllers;
using MessageBoard.Data;
using MessageBoard.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MessageBoard.Test.Controllers
{
    [TestClass]
    public class TopicsControllerTests
    {
        private TopicsController _controller;

        [TestInitialize]
        public void Setup()
        {
            var repo = new FakeMessageBoardRepository();
            _controller = new TopicsController(repo);   
        }

        [TestMethod]
        public void TopicsControllerGet()
        {
            var results = _controller.Get(true);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            Assert.IsNotNull(results.First());
            Assert.IsNotNull(results.First().Title);
        }

        [TestMethod]
        public void TopicsControllerPost()
        {
            // Testing POST is harder than it should be but we need to do some work:

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/v1/topics");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "topics" } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var newTopic = new Topic()
            {
                Title = "Test title",
                Body = "Test body"
            };
            var result = _controller.Post(newTopic);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            var json = result.Content.ReadAsStringAsync().Result;
            var topic = JsonConvert.DeserializeObject<Topic>(json);

            Assert.IsNotNull(topic);
            Assert.AreEqual("Test title", topic.Title);
            Assert.AreEqual("Test body", topic.Body);
            Assert.AreNotEqual(0, topic.Id);
        }
    }
}
