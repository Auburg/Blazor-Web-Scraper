using System.Collections.Immutable;
using BlazorApp1.Shared;
using PuppeteerSharp;

namespace BlazorApp1.Services;

public interface IAuthorQuotesRetrieverService
{
    Task<ImmutableArray<AuthorQuote>> GetAuthorQuotes();
}

public class AuthorQuotesRetrieverService : IAuthorQuotesRetrieverService

{
    public async Task<ImmutableArray<AuthorQuote>> GetAuthorQuotes()
    {
        await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

        var options = new LaunchOptions { Headless = true};
        await using var browser = await Puppeteer.LaunchAsync(options);

        var page = await browser.NewPageAsync();
        await page.GoToAsync("http://quotes.toscrape.com/");

        var jsCode = @"() => {
const quoteList = document.querySelectorAll("".quote"");    
    return Array.from(quoteList).map((quote) => {
      // Get the sub-elements from the previously fetched quote element
      const text = quote.querySelector("".text"").innerText;
      const author = quote.querySelector("".author"").innerText;
      return { text, author };
    });
}";
        var authorQuotes = await page.EvaluateFunctionAsync<AuthorQuote[]>(jsCode);

        return authorQuotes.ToImmutableArray();
    }
}