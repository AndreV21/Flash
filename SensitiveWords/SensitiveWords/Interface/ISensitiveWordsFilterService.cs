namespace SensitiveWords.Interface
{
    public interface ISensitiveWordsFilterService
    {
        string FilterSensitiveWords(string message);
    }
}
