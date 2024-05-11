using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace SLYWDotNetCore.RestApiWithNLayer.Features.DreamDictionary
{
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

        [HttpGet("{BlogId}")]
        public async Task<IActionResult> BlogTitle(int BlogId)
        {
            var model = await GetDreamAsync();
            return Ok(model.BlogHeader.FirstOrDefault(x => x.BlogId == BlogId));
        }

        [HttpGet("detail/{BlogId}")]
        public async Task<IActionResult> BlogDetail(int BlogId)
        {
            var model = await GetDreamAsync();
            return Ok(model.BlogDetail.FirstOrDefault(x => x.BlogId == BlogId));
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
}
