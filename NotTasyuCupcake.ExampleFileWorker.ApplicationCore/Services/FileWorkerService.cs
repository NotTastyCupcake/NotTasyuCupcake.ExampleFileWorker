using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;
using System.IO;
using System.Text;

namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Services;
public class FileWorkerService : IFileWorkerService
{
    private readonly IAppLogger<FileWorkerService> _logger;
    public FileWorkerService(IAppLogger<FileWorkerService> logger)
    {
        _logger = logger;
    }

    public string GetFileString(string filePath)
    {
        if(!string.IsNullOrEmpty(filePath) 
            && IsFilePath(filePath) 
            && IsValidSize(filePath) 
            && IsUtf8Encoded(filePath)
            && CheckLineCount(filePath))
        {

            using (StreamReader stream = new StreamReader(filePath, Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }

        }
        _logger.LogError("Файл не подходит для отправки в базу данных");
        return "";
    }

    public bool IsValidSize(string path)
    {
        FileInfo file = new FileInfo(path);
        _logger.LogInformation("Размер файла:"+ file.Length);

        if(file.Length < Constants.MAX_FILE_SIZE)
        {
            return true;
        }
        else
        {
            _logger.LogError("Размер файла больше допустимого значения.");
            return false;
        }
    }

    public bool IsFilePath(string path)
    {
        try
        {
            // Проверяем, существует ли файл по указанному пути
            return File.Exists(path);
        }
        catch (NotSupportedException)
        {
            _logger.LogError("Путь содержит недопустимые символы");
            return false;
        }
        catch (ArgumentException)
        {
            _logger.LogError("Путь пустой или содержит только пробельные символы");
            return false;
        }
        catch (PathTooLongException)
        {
            _logger.LogError("Длина пути превышает максимально допустимую");
            return false;
        }
    }

    public bool IsUtf8Encoded(string filePath)
    {
        bool isUtf8 = false;
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer = new byte[4];
            fileStream.Read(buffer, 0, 4);

            // Проверяем сигнатуру BOM (Byte Order Mark)
            if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
            {
                isUtf8 = true;
            }
            else
            {
                // Если отсутствует сигнатура BOM, то проверяем символы на соответствие UTF-8
                byte[] buffer2 = new byte[4096];
                int bytesRead = fileStream.Read(buffer2, 0, buffer2.Length);
                string content = Encoding.UTF8.GetString(buffer2, 0, bytesRead);
                byte[] encodedBytes = Encoding.UTF8.GetBytes(content);

                if (encodedBytes.Length == bytesRead)
                {
                    isUtf8 = true;
                }
            }
        }

        if(isUtf8)
        {
            return true;
        }
        else
        {
            _logger.LogError("Файл в неправильной кодировке");
            return false;
        }
    }



    public bool CheckLineCount(string filePath)
    {
        // Чтение всех строк из файла
        string[] lines = File.ReadAllLines(filePath);

        _logger.LogInformation("Количество строк: " + lines.Length);

        // Проверка наличия более одной строки
        if (lines.Length > 1)
        {
            return true;
        }
        else
        {
            _logger.LogError("Количество строк в файле меньше допустимого");
            return false;
        }
    }
}