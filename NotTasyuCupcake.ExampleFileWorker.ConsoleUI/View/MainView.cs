using Microsoft.Extensions.Hosting;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;

namespace NotTasyuCupcake.ExampleFileWorker.ConsoleUI.View;
class MainView : IHostedService
{
	private readonly IWordService _wordservice;
	private readonly IFileWorkerService _fileWorkerService;
	public MainView(IWordService wordservice, IFileWorkerService fileWorkerService)
	{
        _wordservice = wordservice;
		_fileWorkerService= fileWorkerService;
	}

    public void Inisialize()
    {
        Console.WriteLine("¬ведите путь к файлу дл€ чтени€ и нажмите Enter:");

        string? path = Console.ReadLine();

		if(!string.IsNullOrWhiteSpace(path))
		{
			var res = _fileWorkerService.GetFileString(path);
			_wordservice.CreateWordCounterCollection(res);
		}
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Inisialize();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}