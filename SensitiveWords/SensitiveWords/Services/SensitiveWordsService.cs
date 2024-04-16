using SensitiveWords.Interface;
using SensitiveWords.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SensitiveWords.Services
{
    public class SensitiveWordsService : ISensitiveWordsService
    {
        private readonly SensitiveWordContext _context;

        public SensitiveWordsService(SensitiveWordContext context)
        {
            _context = context;
        }

        public void AddSensitiveWord(List<SensitiveWord> sensitiveWords)
        {
            foreach (SensitiveWord sensitiveWord in sensitiveWords)
            {
                _context.SensitiveWord.Add(sensitiveWord);
            }
            
            _context.SaveChanges();
        }

        public void AddSensitiveWordFromStringList(List<string> sensitiveWords)
        {
            foreach (string word in sensitiveWords) {
                SensitiveWord sensitiveWord = new SensitiveWord();
                sensitiveWord.Word = word;
                _context.SensitiveWord.Add(sensitiveWord);
            }
            _context.SaveChanges();
        }

        public void DeleteSensitiveWord(string id)
        {
            var word = _context.SensitiveWord.Find(id);
            if (word != null)
            {
                _context.SensitiveWord.Remove(word);
                _context.SaveChanges();
            }
        }

        public List<string> GetSensitiveWords()
        {
            var list = new List<string>();
            foreach (SensitiveWord sensitiveWord in _context.SensitiveWord)
            {
                list.Add(sensitiveWord.Word);
            }
            return list;
        }

        public void UpdateSensitiveWord(SensitiveWord word)
        {
            _context.SensitiveWord.Update(word);
            _context.SaveChanges();
        }
    }
}
