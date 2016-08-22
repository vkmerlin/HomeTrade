using HRPortal.Business;
using HRPortal.Model;
using HRPortal.Web.Controllers;
using HRPortal.Web.NUnitTests.MockDInjections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRPortal.Web.NUnitTests.Controllers
{
    public class NewsApiControllerTest
    {
        ServiceFactory factory;

        [SetUp]
        public void HomeTestInitialize()
        {
            factory = new ServiceFactory();
        }

        [Test]
        public void NewsGetNews()
        {
            // Arrange
            NewsApiController controller = factory.NewsApiController;

            // Act
            var result = controller.GetNews().ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void NewsGetPieceIfNews()
        {
            // Arrange
            NewsApiController controller = factory.NewsApiController;

            // Act
            var result = controller.GetNews(1).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void NewsCreateNews()
        {
            NewsApiController controller = factory.NewsApiController;
            int testId = 12;
            News testData = new News { Id = testId, CreateDate = DateTime.Now, Title = "Title", Message = "Message" };
            // Act
            var result = controller.CreateNews();
            var IfNewsAdded = factory.NewsService.GetById(testData.Id);
            Assert.IsNotNull(IfNewsAdded);
            Assert.IsInstanceOf<News>(IfNewsAdded);
            Assert.AreEqual(testData.Id, IfNewsAdded.Id);

            //Remove test piece of News
            factory.NewsService.RemoveNews(testData);
            var IsTestDataRemoved = factory.NewsService.GetById(testId);
            Assert.IsNull(IsTestDataRemoved);

            Assert.IsNotNull(result);
        }

        [Test]
        public void NewsEditNews()
        {
            NewsApiController controller = factory.NewsApiController;
            int testId = 1;
            string title = "Updated title";
            News testData = new News { Id = testId, CreateDate = DateTime.Now, Title = title, Message = "Message" };
            // Act
            var result = controller.EditNews();
            var IfNewsUpdated = factory.NewsService.GetById(testData.Id);
            Assert.IsNotNull(IfNewsUpdated);
            Assert.IsInstanceOf<News>(IfNewsUpdated);
            Assert.AreEqual(testData.Title, IfNewsUpdated.Title);

            Assert.IsNotNull(result);
        }

        [Test]
        public void NewsRemoveNews()
        {
            NewsApiController controller = factory.NewsApiController;
            int testId = 1;
            News testData = new News { Id = testId, CreateDate = DateTime.Now, Title = "Title", Message = "Message" };
            // Act
            var result = controller.RemoveNews(testData);
            var IfNewsRemoved = factory.NewsService.GetById(testData.Id);
            Assert.IsNull(IfNewsRemoved);

            Assert.IsNotNull(result);
        }
    }
}



