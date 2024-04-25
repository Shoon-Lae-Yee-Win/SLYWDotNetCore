// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using SLYWDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(12, "test title", "test author", "test content");
//adoDotNetExample.Delete(13);
//adoDotNetExample.Edit(13);
//adoDotNetExample.Edit(1);

DapperExample dapperExample = new DapperExample();
dapperExample.Run();

Console.ReadKey();
