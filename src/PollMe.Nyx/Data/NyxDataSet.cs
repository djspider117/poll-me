namespace PollMe.Nyx.Data;

public class NyxDataSet
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public HashSet<NyxDataSetEntry>? Entries { get; set; }
}
