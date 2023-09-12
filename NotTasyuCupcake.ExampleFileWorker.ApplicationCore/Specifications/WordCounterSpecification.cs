using Ardalis.Specification;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Entities;

namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Specifications;
public sealed class WordCounterSpecification : Specification<WordCounter> 
{
	public WordCounterSpecification(string word)
	{
		Query
			.Where(x => x.Word == word);
	}
}