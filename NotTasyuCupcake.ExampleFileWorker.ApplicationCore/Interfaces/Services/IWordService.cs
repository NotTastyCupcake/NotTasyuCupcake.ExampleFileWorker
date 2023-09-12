namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;
public interface IWordService
{
    Task CreateWordCounterCollection(string words);
    bool IsValidWord(string word);
    bool IsValidCount(int count);
    string[] SplatWords(string words);
}