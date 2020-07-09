using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using PuppeteerSharp;

[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    [HttpPost("[action]")]
    public async Task<List<SearchInUrl>> google(SearchInUrl input)
    {
        var searchKey = input.Url + "&tbm=isch&tbs=isz%3Al&hl=en&biw=1349&bih=663";
        List<SearchInUrl> searchInUrl = new List<SearchInUrl>();
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = false,
            ExecutablePath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"
        });
        using (var page = await browser.NewPageAsync())
        {
            await page.GoToAsync(searchKey);
            List<string> allLink = new List<string>();
            // var jsSelectLengthString = @"(document.querySelectorAll('#islrg > div.islrc > div').length)";
            var jsSelectAllAnchors = @"Array.from(document.querySelectorAll('#islrg > div.islrc > div >a.wXeWr.islib.nfEiy.mM5pbd')).map(a => a.href)";
             //{ 
             //    if(a.href.includes(imgres?imgurl){
             //        return a.href;
             //}
             //})";
            var urls = await page.EvaluateExpressionAsync<string[]>(jsSelectAllAnchors);

            //int jsSelectLength= await page.EvaluateExpressionAsync<int>(jsSelectLengthString);
            //for (int i = 0; i < jsSelectLength; i++) {
            //    i++;
            //   var  linkString = @"document.querySelectorAll('#islrg > div.islrc > div:nth-child("+ i +") > a.wXeWr.islib.nfEiy.mM5pbd')";
            //    Console.WriteLine(linkString);
            //    var tag = await page.EvaluateExpressionAsync<string>(linkString);
            //    Console.WriteLine(tag);
            //    allLink.Add(tag);

            //   // var one = await page.QuerySelectorAllHandleAsync("#islrg > div.islrc > div:nth-child(" + i + ") > a.wXeWr.islib.nfEiy.mM5pbd")
            //   //.EvaluateFunctionAsync<string[]>("elements => elements.map(a=>a['href'])");
            //   // Console.WriteLine(one);
            //    i--;
            //}
            // var jsSelectAll = @"Array.from(document.querySelectorAll('#islrg > div.islrc > div').length)";
            // var countLink = await page.QuerySelectorAllHandleAsync("#islrg > div.islrc > div").EvaluateFunctionAsync<string[]>("" +
            //    "(elements)=>{" +
            //    "document.querySelectorAll;}" +
            //    "");
               // .EvaluateFunctionAsync<string[]>("elements => elements.map(a => a.href)");
            //for (int i = 0; i < countLink.Length; i++)
            //{
            //    var href = await page.QuerySelectorAllHandleAsync("#islrg > div.islrc > div:nth-child(" + i + 1 + ") > a.wXeWr.islib.nfEiy.mM5pbd")
            //        .EvaluateFunctionAsync<string[]>("elements => {console.log(elements);}");
            //}
            var allLinks = await page.QuerySelectorAllHandleAsync("#yDmH0d > div.T1diZc.KWE8qe > c-wiz > div.mJxzWe a")
                .EvaluateFunctionAsync<string[]>("elements => elements.map(a => a.href)");

           
            foreach (var item in allLinks)
            {
                SearchInUrl url = new SearchInUrl();
                url.Url = item.Trim();
                searchInUrl.Add(url);
                //var url = item.ExecutionContext.Frame.Url;
                //if (url.Contains("imgres?imgurl"))
                //{
                //    await page.GoToAsync(url);
                //}
            }
            // var jsSelectAllAnchors = @"Array.from(document.querySelectorAll('a')).map(a => { 
            //     if(a.href.includes(imgres?imgurl){
            //         return a.href;
            // }
            // })";
            // var urls = await page.EvaluateExpressionAsync<string[]>(jsSelectAllAnchors);
            // foreach (string url in urls)
            // {
            //     Console.WriteLine(url);
            //     await page.GoToAsync(url);
            // }
        }
        return searchInUrl;
    }
}