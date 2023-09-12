using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Entities;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore;

namespace NotTasyuCupcake.ExampleFileWorker.Infrastructure.Data.Config;
class WordCounterConfigutation : IEntityTypeConfiguration<WordCounter>
{
    public void Configure(EntityTypeBuilder<WordCounter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(WordCounter));

        builder.Property(b => b.Word)
            .IsRequired(true)
            .HasMaxLength(ApplicationCore.Constants.MAX_LANG_WORD);

        builder.Property(b => b.Count)
            .IsRequired(true);
    }
}