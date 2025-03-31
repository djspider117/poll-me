using Microsoft.AspNetCore.Mvc;
using PollMe.Nyx.Data;
using PollMe.Nyx.Data.Access;
using PollMe.Nyx.Data.Responses;

namespace PollMe.Nyx.Controllers;

[ApiController]
[Route("[controller]")]
public class DataSetController : ControllerBase
{
    private readonly ILogger<DataSetController> _logger;
    private readonly NyxDataSetRepository _dataSetRepo;

    public DataSetController(ILogger<DataSetController> logger, NyxDataSetRepository dataSetRepository)
    {
        _logger = logger;
        _dataSetRepo = dataSetRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDataSets(CancellationToken ct = default)
    {
        _logger.LogInformation(nameof(GetDataSets));
        var data = await _dataSetRepo.GetAllAsync(ct: ct);

        var rv = data.Select(x => x.ToResponseObject());
        return Ok(rv);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDataSetById(long id, CancellationToken ct = default)
    {
        _logger.LogInformation(nameof(GetDataSetById));
        var dataSet = await _dataSetRepo.GetByIdAsync(id, ct: ct);

        if (dataSet == null)
            return NotFound();

        return Ok(dataSet.ToResponseObject());
    }

    [HttpGet("randomUnused/{dataSetId}")]
    public async Task<IActionResult> GetUnusedRandomAsync(long dataSetId, CancellationToken ct = default)
    {
        _logger.LogInformation(nameof(GetUnusedRandomAsync));
        var data = await _dataSetRepo.GetUnusedRandomAsync(dataSetId, ct: ct);

        var rv = data.Select(x => new ResponseDataSetEntry(x.Id, x.Value, x.Used));
        return Ok(rv);
    }
}
