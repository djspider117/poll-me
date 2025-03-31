using PollMe.Nyx.Data.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PollMe.Nyx.Data;

public static class Map
{
    public static ResponseDataSet ToResponseObject(this NyxDataSet dataSet)
    {
        return new(dataSet.Id, 
                   dataSet.Name, 
                   dataSet.Entries?.Select(q => new ResponseDataSetEntry(q.Id, q.Value, q.Used))?.ToList());
    }
}
