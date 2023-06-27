using PuppeteerSharp;

namespace BlazorApp1.Services;
using BlazorApp1.Shared;

using System.Collections.Immutable;


public interface IFurnitureRetrieverService
{
    Task<IEnumerable<string>> GetCategories();
}

public class FurnitureRetrieverService : IFurnitureRetrieverService
{
    public async Task<IEnumerable<string>> GetCategories()
    {
        await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

        var options = new LaunchOptions
        {
            Headless = true, Args = new[]
            {
                "--disable-gpu",
                "--disable-dev-shm-usage",
                "--disable-setuid-sandbox",
                "--no-first-run",
                "--no-sandbox",
                "--no-zygote",
                "--deterministic-fetch",
                "--disable-features=IsolateOrigins",
                "--disable-site-isolation-trials"
            }
        };
        await using var browser = await Puppeteer.LaunchAsync(options);

        var page = await browser.NewPageAsync();
        await page.GoToAsync("https://furnituretsar.com/");

        var catElems = await page.QuerySelectorAllAsync("#categories-2 > nav > ul > li");

        var categories= ImmutableArray.CreateBuilder<string?>();

        foreach (var elementHandle in catElems)
        {
            var innerTextProp =  await elementHandle.GetPropertyAsync("innerText");
                
            categories.Add(await innerTextProp.JsonValueAsync<string>());
        }
            
        return categories.ToImmutable();
    }
}