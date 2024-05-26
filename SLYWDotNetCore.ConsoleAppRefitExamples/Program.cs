using Refit;
using SLYWDotNetCore.ConsoleAppRefitExamples;

//var service = RestService.For<IBlogApi>("https://localhost:7144");
//var lst = await service.GetBlogs();
//foreach (var item in lst)
//{
//    Console.WriteLine($"ID => {item.BlogId}");
//    Console.WriteLine($"Title => {item.BlogTitle}");
//    Console.WriteLine($"Auhtor => {item.BlogAuthor}");
//    Console.WriteLine($"Content => {item.BlogContent}");
//    Console.WriteLine("-----------------------------------");
//}

//try
//{
//    RefitExample refitExample = new RefitExample();
//    await refitExample.RunAsync();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}

RefitExample refitExample = new RefitExample();
await refitExample.RunAsync();