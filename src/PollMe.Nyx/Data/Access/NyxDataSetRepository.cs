using Microsoft.EntityFrameworkCore;

namespace PollMe.Nyx.Data.Access;

public class NyxDataSetRepository(NyxContext ctx)
{
    private readonly NyxContext _ctx = ctx;

    public async Task<NyxDataSet?> GetByIdAsync(long id, bool includeEntries = true, bool noTracking = true, CancellationToken ct = default)
    {
        IQueryable<NyxDataSet> source = _ctx.DataSets;

        if (noTracking)
            source = source.AsNoTracking();


        if (includeEntries)
            source = source.Include(x => x.Entries);

        return await source.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IReadOnlyCollection<NyxDataSet>> GetAllAsync(bool includeEntries = true, bool noTracking = true, CancellationToken ct = default)
    {
        IQueryable<NyxDataSet> source = _ctx.DataSets;

        if (noTracking)
            source = source.AsNoTracking();

        if (includeEntries)
            source = source.Include(x => x.Entries);

        return await source.ToListAsync(ct);
    }

    public async Task ImportFromCSVStringAsync(string name, string csvString, CancellationToken ct = default)
    {
        var split = csvString.Split(',');
        var set = new NyxDataSet { Name = name, Entries = [] };
        for (int i = 0; i < split.Length; i++)
        {
            set.Entries.Add(new NyxDataSetEntry { Value = split[i] });
        }

        await _ctx.DataSets.AddAsync(set, ct);
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<NyxDataSetEntry>> GetUnusedRandomAsync(long dataSetId, int top = 5, CancellationToken ct = default)
    {
        return await _ctx.DataSetEntries
            .Where(x => x.DataSetId == dataSetId && !x.Used)
            .OrderBy(x => EF.Functions.Random())
            .Take(top)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task MarkEntryAsUsedAsync(long entryId, CancellationToken ct = default)
    {
        var entity = await _ctx.DataSetEntries.FirstOrDefaultAsync(x => x.Id == entryId, ct);
        if (entity == null)
            return;

        entity.Used = true;
        await _ctx.SaveChangesAsync(ct);
    }
}