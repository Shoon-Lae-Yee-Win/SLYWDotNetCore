using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SLYWDotNetCore.RestApiWithNLayer.Features.MyanmarProverbs;

[Route("api/[controller]")]
[ApiController]
public class MyanmarProverbsController : ControllerBase
{
    private async Task<Tbl_Mmproverbs> GetDataFromApi()
    {
        var JsonStr = await System.IO.File.ReadAllTextAsync("mmProverbsData.json");
        var modal = JsonConvert.DeserializeObject<Tbl_Mmproverbs>(JsonStr);
        return modal!;

        //HttpClient client = new HttpClient();
        //var response = await client.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
        //if (!response.IsSuccessStatusCode) return null;
        //string JsonStr = await response.Content.ReadAsStringAsync();
        //var modal = JsonConvert.DeserializeObject<Tbl_Mmproverbs>(JsonStr);
        //return modal!;

        //if (response.IsSuccessStatusCode)
        //{
        //    string JsonStr = await response.Content.ReadAsStringAsync();
        //    var modal = JsonConvert.DeserializeObject<Tbl_Mmproverbs>(JsonStr);
        //    return modal;
        //}
        //return null;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var modal = await GetDataFromApi();
        return Ok(modal.Tbl_MMProverbsTitle);
    }

    [HttpGet("{titleName}")]
    public async Task<IActionResult> Get(string titleName)
    {
        var modal = await GetDataFromApi();
        var item = modal.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
        if (item is null) return NotFound();

        var titleId = item.TitleId;
        var result = modal.Tbl_MMProverbs.Where(x => x.TitleId == titleId);
        List<Tbl_MmproverbsHead> lst = result.Select(x => new Tbl_MmproverbsHead
        {
            ProverbId = x.ProverbId,
            ProverbName = x.ProverbName,
            TitleId = x.TitleId
        }).ToList();
        return Ok(lst);
    }

    [HttpGet("{titleId}/{proverbId}")]
    public async Task<IActionResult> Get(int titleId, int proverbId)
    {
        var modal = await GetDataFromApi();
        var item = modal.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId);
        return Ok(item);
    }
}

public class Tbl_Mmproverbs
{
    public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
    public Tbl_MmproverbsDetail[] Tbl_MMProverbs { get; set; }
}

public class Tbl_Mmproverbstitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_MmproverbsDetail
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}

public class Tbl_MmproverbsHead
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
}
