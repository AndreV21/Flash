using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SensitiveWords.Controllers;
using SensitiveWords.Interface;

namespace UnitTests
{
    [TestFixture]
    public class FilterControllerTests
    {
        [Test]
        public void FilterMessage_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ISensitiveWordsFilterService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["Security:Password"]).Returns("YourSecretPassword123"); // Set up the expected password
            var controller = new FilterController(mockService.Object, mockConfiguration.Object);
            FilterMessageRequest request = new FilterMessageRequest
            {
                Message = "This is a sensitive message."
            };
            var amendedMessage = "This is a ****** message.";
            mockService.Setup(s => s.FilterSensitiveWords(request.Message)).Returns(amendedMessage);

            // Create a new HttpContext with request headers
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Password"] = "YourSecretPassword123"; // Set the password header
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = controller.FilterMessage(request) as ActionResult<string>;

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var response = okResult.Value as FilterMessageResponse;
            Assert.That(response.AmendedMessage, Is.EqualTo(amendedMessage));
        }
    }
}
