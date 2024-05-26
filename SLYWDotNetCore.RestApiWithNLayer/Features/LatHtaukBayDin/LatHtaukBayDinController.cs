﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SLYWDotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin;

[Route("api/[controller]")]
[ApiController]
public class LatHtaukBayDinController : ControllerBase
{
    private async Task<LatHtaukBayDin> GetDataAsync()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
        var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);
        return model;
    }

    //api/LatHtaukBayDin/questions
    [HttpGet("questions")]
    public async Task<IActionResult> Questions()
    {
        var model = await GetDataAsync();
        return Ok(model.questions);
    }

    [HttpGet]
    public async Task<IActionResult> NumberList()
    {
        var model = await GetDataAsync();
        return Ok(model.numberList);
    }

    [HttpGet("{QuestionNo}/{no}")]
    public async Task<IActionResult> Answers(int QuestionNo, int no)
    {
        var model = await GetDataAsync();
        return Ok(model.answers.FirstOrDefault(x => x.questionNo == QuestionNo && x.answerNo == no));
    }
}

public class LatHtaukBayDin
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}
