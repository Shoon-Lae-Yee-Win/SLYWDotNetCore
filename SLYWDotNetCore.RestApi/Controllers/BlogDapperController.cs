﻿using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLYWDotNetCore.Restapi;
using SLYWDotNetCore.Restapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SLYWDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogDapperController : ControllerBase
{
    //Read
    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "Select * From Tbl_Blog";
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult EditBlog(int id)
    {
        //string query = "Select * From Tbl_Blog Where Blogid = @BlogId";
        //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("no data found");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        int result = db.Execute(query, blog);
        string message = result > 0 ? "Saving Successful." : "Saving Failed.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("no data found");
        }

        blog.BlogId = id;
        string query = @"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
                    WHERE BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        int result = db.Execute(query, blog);
        string message = result > 0 ? "Updating Successful." : "Updating Failed.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blog)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("no data found");
        }

        string conditions = string.Empty;
        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            conditions += "[BlogTitle] = @BlogTitle,";
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            conditions += "[BlogAuthor] = @BlogAuthor,";
        }
        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            conditions += "[BlogContent] = @BlogContent,";
        }

        if (conditions.Length == 0)
        {
            return NotFound("no data to update");
        }
        conditions = conditions.Substring(0, conditions.Length - 1);

        blog.BlogId = id;
        string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {conditions}
            WHERE BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        int result = db.Execute(query, blog);
        string message = result > 0 ? "Updating Patch Successful." : "Updating Patch Failed.";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("no data found");
        }
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        int result = db.Execute(query, new BlogModel { BlogId = id });
        string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
        return Ok(message);
    }

    private BlogModel? FindById(int id)
    {
        string query = "Select * From Tbl_Blog Where Blogid = @BlogId";
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
        return item;
    }
}
