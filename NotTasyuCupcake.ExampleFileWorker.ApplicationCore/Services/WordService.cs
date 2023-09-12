using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Entities;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Specifications;

namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Services;
public class WordService : IWordService
{
    private IAppLogger<WordService> _logger;
    private IRepository<WordCounter> _repository;
    public WordService(
		IAppLogger<WordService> logger, 
		IRepository<WordCounter> repository)
	{
		_logger = logger;
		_repository = repository;
	}

    public async Task CreateWordCounterCollection(string words)
    {
        List<WordCounter> items = new List<WordCounter>();

        if (string.IsNullOrEmpty(words))
        {
            return;
		}

        words = words.ToLower();

        var arrayWords = SplatWords(words);

        if(arrayWords.Any())
        {
            items.AddRange(arrayWords
                //�������� ���������� �����
                .Where(s => IsValidWord(s))
                //���������� ��� �������� ������� ����������
                .GroupBy(s => s)
                .Where(s => IsValidCount(s.Count()))
                //�������� ��������� ���� � �� �����������
                .Select(s => new WordCounter(s.Key, s.Count())));
        }
        
        if(items.Any())
        {
            foreach(var item in items)
            {
                var specification = new WordCounterSpecification(item.Word);
                var wordInDb = await _repository.FirstOrDefaultAsync(specification);
                if (wordInDb != null)
                {
                    _logger.LogInformation("���������� ����������: " + item.Count + ". � �����: " + item.Word);
                    wordInDb.Count += item.Count;
                    await _repository.UpdateAsync(wordInDb); 
                }
                else
                {
                    _logger.LogInformation("���������� �����: " + item.Word);
                    await _repository.AddAsync(item);
                }
            }

            _logger.LogInformation("���������� ����� ����������� ������� � ���� ������:" + items.Count);
            await _repository.SaveChangesAsync();
        }

        return;
    }

    public bool IsValidWord(string word)
	{
		return 
            word.Length >= Constants.MIN_LANG_WORD 
            && word.Length <= Constants.MAX_LANG_WORD;
    }

    /// <summary>
    /// ��������� ���������� ����
    /// </summary>
    /// <param name="count">����������</param>
    public bool IsValidCount(int count)
    {
        return count >= Constants.MIN_COUNT_WORDS;
    }

    /// <summary>
    /// ������ ������ ����������� �����
    /// </summary>
    /// <param name="words">����� �� �����</param>
    public string[] SplatWords(string words)
    {
        //�����������
        char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n' };

        return words.Split(delimiterChars);
    }
}