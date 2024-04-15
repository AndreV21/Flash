using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SensitiveWords.Controllers;
using SensitiveWords.Interface;
using SensitiveWords.Models;
using SensitiveWords.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class SensitiveWordsControllerTests
    {
        [Test]
        public void AddSensitiveWords_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ISensitiveWordsService>();
            var controller = new SensitivewordsController(mockService.Object);
            var sensitiveWords = new List<string> { "ACTION", "ADD", "ALL" }; // Example sensitive words

            // Act
            var result = controller.AddSensitiveWordFromStringList(sensitiveWords) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AddSensitiveWord_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ISensitiveWordsService>();
            var controller = new SensitivewordsController(mockService.Object);
            var sensitiveWords = new List<SensitiveWord> { new SensitiveWord { Id = "asd", Word = "ACTION" } };

            // Act
            var result = controller.AddSensitiveWord(sensitiveWords) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            mockService.Verify(s => s.AddSensitiveWord(sensitiveWords), Times.Once);
        }

        [Test]
        public void AddSensitiveWords_ReturnsBadRequest_WhenSensitiveWordsNull()
        {
            // Arrange
            var mockService = new Mock<ISensitiveWordsService>();
            var controller = new SensitivewordsController(mockService.Object);

            // Act
            var result = controller.AddSensitiveWord(null) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }
    }
}
