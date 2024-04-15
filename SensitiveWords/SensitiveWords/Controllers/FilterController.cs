using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Interface;
using SensitiveWords.Services;

namespace SensitiveWords.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilterController : ControllerBase
    {
        private readonly ISensitiveWordsFilterService _sensitiveWordsFilterService;
        private readonly IConfiguration _configuration;

        public FilterController(ISensitiveWordsFilterService sensitiveWordsFilterService, IConfiguration configuration)
        {
            _sensitiveWordsFilterService = sensitiveWordsFilterService;
            _configuration = configuration;
        }

        [HttpPost("message")]
        public ActionResult<string> FilterMessage([FromBody] FilterMessageRequest request)
        {
            // Get the expected password from appsettings.json
            var expectedPassword = _configuration["Security:Password"];

            // Check if the password is provided in the headers
            if (!Request.Headers.TryGetValue("Password", out var actualPassword))
            {
                return Unauthorized("Password not provided in the headers.");
            }

            // Compare the provided password with the expected password
            if (actualPassword != expectedPassword)
            {
                return Unauthorized("Invalid password.");
            }

            string amendedMessage = _sensitiveWordsFilterService.FilterSensitiveWords(request.Message);
            return Ok(new FilterMessageResponse { AmendedMessage = amendedMessage });
        }
    }

    public class FilterMessageRequest
    {
        public string? Message { get; set; }
    }

    public class FilterMessageResponse
    {
        public string? AmendedMessage { get; set; }
    }
}
