using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SLYWDotNetCore.RestApiWithNLayer.Features.MyanmarProverbs;
using System.Linq;
using System.Threading.Tasks;

namespace SLYWDotNetCore.RestApiWithNLayer.Features.DreamDictionary;

[Route("api/[controller]")]
[ApiController]
public class DreamDictionaryController : ControllerBase
{
    private async Task<DreamDictionary> GetDreamAsync()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("dreamData.json");
        var model = JsonConvert.DeserializeObject<DreamDictionary>(jsonStr);
        return model;
    }

    [HttpGet("header")]
    public async Task<IActionResult> BlogHeader()
    {
        var model = await GetDreamAsync();
        return Ok(model.BlogHeader);
    }

    [HttpGet("header/{titleName}")]
    public async Task<IActionResult> Get(string titleName)
    {
        var modal = await GetDreamAsync();
        var item = modal.BlogHeader.FirstOrDefault(x => x.BlogTitle == titleName);
        if (item is null) return NotFound();

        var titleId = item.BlogId;
        var result = modal.BlogDetail.Where(x => x.BlogId == titleId);
        return Ok(result);
    }

    [HttpGet("detail/{blogId}/{detailId}")]
    public async Task<IActionResult> BlogDetail(int blogId, int detailId)
    {
        var model = await GetDreamAsync();
        return Ok(model.BlogDetail.FirstOrDefault(x => x.BlogId == blogId && x.BlogDetailId == detailId));
    }
}

public class DreamDictionary
{
    public Blogheader[] BlogHeader { get; set; }
    public Blogdetail[] BlogDetail { get; set; }
}

public class Blogheader
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
}

public class Blogdetail
{
    public int BlogDetailId { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; }
}
