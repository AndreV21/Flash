using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FilterController> _logger;

        public FilterController(ISensitiveWordsFilterService sensitiveWordsFilterService, IConfiguration configuration, ILogger<FilterController> logger)
        {
            _sensitiveWordsFilterService = sensitiveWordsFilterService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("message")]
        public ActionResult<string> FilterMessage([FromBody] FilterMessageRequest request)
        {
            try
            {
                // Get the expected password from appsettings.json
                var expectedPassword = _configuration["Security:Password"];

                // Check if the password is provided in the headers
                if (!Request.Headers.TryGetValue("Password", out var actualPassword))
                {
                    _logger.LogError("Password not provided in the headers.");
                    return Unauthorized("Password not provided in the headers.");
                }

                // Compare the provided password with the expected password
                if (actualPassword != expectedPassword)
                {
                    _logger.LogError("Invalid password.");
                    return Unauthorized("Invalid password.");
                }

                string amendedMessage = _sensitiveWordsFilterService.FilterSensitiveWords(request.Message);
                return Ok(new FilterMessageResponse { AmendedMessage = amendedMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception:" + ex.Message);
                return BadRequest(ex.Message);
            }
 
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
