using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;

[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    [HttpPost("[action]")]
    public async Task<SearchInUrl> google(SearchInUrl input)
    {
        var searchKey = input.Url + "&tbm=isch&tbs=isz%3Al&hl=en&biw=1349&bih=663";
        var searchInUrl = new SearchInUrl();
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = false,
            ExecutablePath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"
        });
        using (var page = await browser.NewPageAsync())
        {
            await page.GoToAsync(searchKey);
            var jsSelectAllAnchors = @"Array.from(document.querySelectorAll('a')).map(a => { 
                if(a.href.includes(imgres?imgurl){
                    return a.href;
            }
            })";
            var urls = await page.EvaluateExpressionAsync<string[]>(jsSelectAllAnchors);
            foreach (string url in urls)
            {
                Console.WriteLine(url);
                await page.GoToAsync(url);
            }
        }
        return searchInUrl;
    }
}