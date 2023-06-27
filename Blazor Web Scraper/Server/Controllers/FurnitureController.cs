using BlazorApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FurnitureController : ControllerBase
{
    private readonly ILogger<AuthorQuotesController> _logger;
    private readonly IFurnitureRetrieverService _furnitureRetrieverService;

    public FurnitureController(ILogger<AuthorQuotesController> logger, IFurnitureRetrieverService furnitureRetrieverService)
    {
        _logger = logger;
        _furnitureRetrieverService = furnitureRetrieverService;
    }

    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        try
        {
            return await _furnitureRetrieverService.GetCategories();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Error}", e.Message);
            throw;
        }
    }
}