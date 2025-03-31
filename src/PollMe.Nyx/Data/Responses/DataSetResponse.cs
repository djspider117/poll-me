namespace PollMe.Nyx.Data.Responses;

public readonly record struct ResponseDataSet(long Id, string Name, IReadOnlyList<ResponseDataSetEntry>? Entries);
public readonly record struct ResponseDataSetEntry(long Id, string? Value, bool WasUsed);
