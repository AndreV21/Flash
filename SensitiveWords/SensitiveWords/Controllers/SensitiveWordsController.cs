using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Interface;
using SensitiveWords.Models;
using SensitiveWords.Services;

namespace SensitiveWords.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensitivewordsController : ControllerBase
    {
        private readonly ISensitiveWordsService _sensitiveWordsService;
        private readonly ILogger<SensitivewordsController> _logger;

        public SensitivewordsController(ISensitiveWordsService sensitiveWordsService, ILogger<SensitivewordsController> logger)
        {
            _sensitiveWordsService = sensitiveWordsService;
            _logger = logger;   
        }

        [HttpGet("GetSensitiveWords")]
        public ActionResult<IEnumerable<string>> GetSensitiveWords()
        {
            try
            {
                var sensitiveWords = _sensitiveWordsService.GetSensitiveWords();
                return Ok(sensitiveWords);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while retrieving words: " + ex.Message);
                return BadRequest("Error Occured while retrieving words: " + ex.Message);
            }

        }

        [HttpPost("AddSensitiveWord")]
        public ActionResult AddSensitiveWord([FromBody] List<SensitiveWord> sensitiveWords)
        {
            try
            {
                if (sensitiveWords == null || sensitiveWords.Count == 0)
                {
                    _logger.LogError("No sensitive words provided.");
                    return BadRequest("No sensitive words provided.");
                }
                _sensitiveWordsService.AddSensitiveWord(sensitiveWords);
                return Ok("Sensitive words added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while adding word: " + ex.Message);
                return BadRequest("Error Occured while adding word: " + ex.Message);
            }

        }

        [HttpPost("AddSensitiveWordFromStringList")]
        public ActionResult AddSensitiveWordFromStringList([FromBody] List<string> sensitiveWords)
        {
            try
            {
                if (sensitiveWords == null || sensitiveWords.Count == 0)
                {
                    _logger.LogError("No sensitive words provided.");
                    return BadRequest("No sensitive words provided.");
                }
                _sensitiveWordsService.AddSensitiveWordFromStringList(sensitiveWords);
                return Ok("Sensitive words added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while adding words from string list: " + ex.Message);
                return BadRequest("Error Occured while adding words from string list: " + ex.Message);
            }

        }

        [HttpPut("UpdateSensitiveWord")]
        public ActionResult UpdateSensitiveWord([FromBody] SensitiveWord word)
        {
            try
            {
                _sensitiveWordsService.UpdateSensitiveWord(word);
                return Ok("Sensitive Word has been updated");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while updating word: " + ex.Message);
                return BadRequest("Error Occured while updating word: " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSensitiveWord(string id)
        {
            try
            {
                _sensitiveWordsService.DeleteSensitiveWord(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while deleting word: " + ex.Message);
                return BadRequest("Error Occured while deleting word: " + ex.Message);
            }

        }
    }

}
