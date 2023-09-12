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
                //Выбираем подходящие слова
                .Where(s => IsValidWord(s))
                //Групперуем для проверки нужного количества
                .GroupBy(s => s)
                .Where(s => IsValidCount(s.Count()))
                //Получаем коллекцию слов с их количеством
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
                    _logger.LogInformation("Добавленно количество: " + item.Count + ". В слова: " + item.Word);
                    wordInDb.Count += item.Count;
                    await _repository.UpdateAsync(wordInDb); 
                }
                else
                {
                    _logger.LogInformation("Добавленно слова: " + item.Word);
                    await _repository.AddAsync(item);
                }
            }

            _logger.LogInformation("Количество всего обновленных записей в базу данных:" + items.Count);
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
    /// Проверяет количество слов
    /// </summary>
    /// <param name="count">количество</param>
    public bool IsValidCount(int count)
    {
        return count >= Constants.MIN_COUNT_WORDS;
    }

    /// <summary>
    /// Выдает массив разделенных строк
    /// </summary>
    /// <param name="words">Текст из файла</param>
    public string[] SplatWords(string words)
    {
        //Разделители
        char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n' };

        return words.Split(delimiterChars);
    }
}