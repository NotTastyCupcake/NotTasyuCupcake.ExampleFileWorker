namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Entities;
public class WordCounter : BaseEntity
{

    public string? Word { get; private set; }

    public WordCounter(string? word, int count)
    {
        Word = word;
        Count = count;
    }

    public WordCounter()
    {
        // required by EF
    }

    public int Count { get; set; }
}