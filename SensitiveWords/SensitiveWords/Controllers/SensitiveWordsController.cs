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

        public SensitivewordsController(ISensitiveWordsService sensitiveWordsService)
        {
            _sensitiveWordsService = sensitiveWordsService;
        }

        [HttpGet("GetSensitiveWords")]
        public ActionResult<IEnumerable<string>> GetSensitiveWords()
        {
            var sensitiveWords = _sensitiveWordsService.GetSensitiveWords();
            return Ok(sensitiveWords);
        }

        [HttpPost("AddSensitiveWord")]
        public ActionResult AddSensitiveWord([FromBody] List<SensitiveWord> sensitiveWords)
        {
            if (sensitiveWords == null || sensitiveWords.Count == 0)
            {
                return BadRequest("No sensitive words provided.");
            }
            _sensitiveWordsService.AddSensitiveWord(sensitiveWords);
            return Ok("Sensitive words added successfully.");
        }

        [HttpPost("AddSensitiveWordFromStringList")]
        public ActionResult AddSensitiveWordFromStringList([FromBody] List<string> sensitiveWords)
        {
            if (sensitiveWords == null || sensitiveWords.Count == 0)
            {
                return BadRequest("No sensitive words provided.");
            }
            _sensitiveWordsService.AddSensitiveWordFromStringList(sensitiveWords);
            return Ok("Sensitive words added successfully.");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSensitiveWord(SensitiveWord word)
        {
            _sensitiveWordsService.UpdateSensitiveWord(word);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSensitiveWord(int id)
        {
            _sensitiveWordsService.DeleteSensitiveWord(id);
            return Ok();
        }
    }

}
