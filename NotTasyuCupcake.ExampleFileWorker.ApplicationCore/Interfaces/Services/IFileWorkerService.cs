using System.Text;

namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;
public interface IFileWorkerService
{
    string GetFileString(string filePath);
    //bool IsFilePath(string path);
    //bool IsValidSize(string path);
    //bool IsUtf8Encoded(string filePath);
    //bool CheckLineCount(string filePath);
}