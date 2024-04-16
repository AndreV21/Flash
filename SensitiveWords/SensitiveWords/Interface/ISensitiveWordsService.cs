using SensitiveWords.Models;

namespace SensitiveWords.Interface
{
    public interface ISensitiveWordsService
    {
        List<string> GetSensitiveWords();
        void AddSensitiveWord(List<SensitiveWord> word);
        void AddSensitiveWordFromStringList(List<string> word);
        void UpdateSensitiveWord(SensitiveWord word);
        void DeleteSensitiveWord(string id);
    }
}
