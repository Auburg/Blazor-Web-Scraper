using BlazorApp1.Services;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorQuotesController : ControllerBase
{
    private readonly ILogger<AuthorQuotesController> _logger;
    private readonly IAuthorQuotesRetrieverService _authorQuotesRetrieverService;

    public AuthorQuotesController(ILogger<AuthorQuotesController> logger, IAuthorQuotesRetrieverService authorQuotesRetrieverService)
    {
        _logger = logger;
        _authorQuotesRetrieverService = authorQuotesRetrieverService;
    }

    [HttpGet]
    public async Task<IEnumerable<AuthorQuote>> Get()
    {
        try
        {
            return await _authorQuotesRetrieverService.GetAuthorQuotes();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Error}", e.Message);
            throw;
        }
    }
}