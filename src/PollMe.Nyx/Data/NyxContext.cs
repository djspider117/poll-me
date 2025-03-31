using Microsoft.EntityFrameworkCore;

namespace PollMe.Nyx.Data;

public class NyxContext : DbContext
{
    private static readonly string[] _seedPlanets = ["Tatooine", "Mustafar", "Coruscant", "Endor", "Hoth", "Kamino", "Naboo", "Scarif", "Dathomir", "Mandalore", "Geonosis", "Yavin 4", "Jedha", "Ilum", "Felucia", "Lothal", "Ryloth", "Sullust", "Mon Cala", "Dagobah", "Malachor", "Crait"];

    public DbSet<NyxDataSet> DataSets { get; set; }
    public DbSet<NyxDataSetEntry> DataSetEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Both UseSeeding and UseAsyncSeeding are used because EF tooling uses the sync version
        // Details: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding

        base.OnConfiguring(builder);

        builder.UseSqlite("Data Source=Application.db")
               .UseSeeding((ctx, _) =>
               {
                   ctx.Set<NyxDataSet>().Add(CreateSeedDataSet());
                   ctx.SaveChanges();
               })
               .UseAsyncSeeding(async (ctx, _, ct) =>
               {
                   ctx.Set<NyxDataSet>().Add(CreateSeedDataSet());
                   await ctx.SaveChangesAsync(ct);
               });
    }

    private static NyxDataSet CreateSeedDataSet() => new()
    {
        Name = "Demo Set",
        Entries = new(_seedPlanets.Select(x => new NyxDataSetEntry { Value = x }))
    };
}