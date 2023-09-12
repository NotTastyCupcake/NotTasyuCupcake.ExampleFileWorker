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
        _logger.LogError("���� �� �������� ��� �������� � ���� ������");
        return "";
    }

    public bool IsValidSize(string path)
    {
        FileInfo file = new FileInfo(path);
        _logger.LogInformation("������ �����:"+ file.Length);

        if(file.Length < Constants.MAX_FILE_SIZE)
        {
            return true;
        }
        else
        {
            _logger.LogError("������ ����� ������ ����������� ��������.");
            return false;
        }
    }

    public bool IsFilePath(string path)
    {
        try
        {
            // ���������, ���������� �� ���� �� ���������� ����
            return File.Exists(path);
        }
        catch (NotSupportedException)
        {
            _logger.LogError("���� �������� ������������ �������");
            return false;
        }
        catch (ArgumentException)
        {
            _logger.LogError("���� ������ ��� �������� ������ ���������� �������");
            return false;
        }
        catch (PathTooLongException)
        {
            _logger.LogError("����� ���� ��������� ����������� ����������");
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

            // ��������� ��������� BOM (Byte Order Mark)
            if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
            {
                isUtf8 = true;
            }
            else
            {
                // ���� ����������� ��������� BOM, �� ��������� ������� �� ������������ UTF-8
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
            _logger.LogError("���� � ������������ ���������");
            return false;
        }
    }



    public bool CheckLineCount(string filePath)
    {
        // ������ ���� ����� �� �����
        string[] lines = File.ReadAllLines(filePath);

        _logger.LogInformation("���������� �����: " + lines.Length);

        // �������� ������� ����� ����� ������
        if (lines.Length > 1)
        {
            return true;
        }
        else
        {
            _logger.LogError("���������� ����� � ����� ������ �����������");
            return false;
        }
    }
}