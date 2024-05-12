using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SLYWDotNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Hello, World!");

//HttpClient client = new HttpClient();
//var response = await client.GetAsync("https://localhost:7133/api/blog");
//if (response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    //Console.WriteLine(jsonStr);
//    List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
//    foreach(var blog in lst)
//    {
//        Console.WriteLine(JsonConvert.SerializeObject(blog));
//        Console.WriteLine($"Title => { blog.BlogTitle}");
//        Console.WriteLine($"Auhtor => {blog.BlogAuthor}");
//        Console.WriteLine($"Content => {blog.BlogContent}");
//    }
//}
//task.RunSynchronously();

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();