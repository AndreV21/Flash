using SensitiveWords.Interface;
using SensitiveWords.Models;
using System.Text.RegularExpressions;

namespace SensitiveWords.Services
{
    public class SensitiveWordsFilterService : ISensitiveWordsFilterService
    {
        private readonly ISensitiveWordsService _sensitiveWordsService;

        public SensitiveWordsFilterService(ISensitiveWordsService sensitiveWordsService)
        {
            _sensitiveWordsService = sensitiveWordsService;
        }

        public string FilterSensitiveWords(string message)
        {
            try
            {
                var sensitiveWords = _sensitiveWordsService.GetSensitiveWords();
                foreach (string sensitiveWord in sensitiveWords)
                {
                    // Construct a regular expression pattern to match whole words only
                    string pattern = $@"\b{Regex.Escape(sensitiveWord)}\b";

                    // Replace occurrences of the sensitive word with asterisks
                    message = Regex.Replace(message, pattern, new string('*', sensitiveWord.Length), RegexOptions.IgnoreCase);
                }
                return message;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
