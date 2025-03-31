namespace PollMe.Nyx.Data;

public class NyxDataSetEntry
{
    public long Id { get; set; }
    public string? Value { get; set; }
    public bool Used { get; set; }

    public long DataSetId { get; set; }
    public NyxDataSet? DataSet { get; set; }
}
