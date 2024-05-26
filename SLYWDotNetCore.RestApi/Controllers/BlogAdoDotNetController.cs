using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLYWDotNetCore.Restapi;
using SLYWDotNetCore.Restapi.Models;

namespace SLYWDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNetController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBlogs()
    {
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = "SELECT * FROM Tbl_Blog";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlDataAdapter.Fill(dt);

        connection.Close();

        //List<BlogModel> lst = new List<BlogModel>();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    BlogModel blog = new BlogModel();
        //    blog.BlogId = Convert.ToInt32(dr["BlogID"]);
        //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
        //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
        //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);

        //    BlogModel blog = new BlogModel
        //    {
        //        BlogId = Convert.ToInt32(dr["BlogID"]),
        //        BlogTitle = Convert.ToString(dr["BlogTitle"]),
        //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
        //        BlogContent = Convert.ToString(dr["BlogContent"])
        //    };

        //    lst.Add(blog);
        //}

        List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogID"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        }).ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult EditBlogs(int id)
    {
        string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlDataAdapter.Fill(dt);

        connection.Close();

        if (dt.Rows.Count == 0)
        {
            return NotFound("no data found");
        }
        DataRow dr = dt.Rows[0];
        var item = new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogID"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        };
        return Ok(dt);
    }

    [HttpPost]
    public IActionResult CreateBlogs(BlogModel blog)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Saving Successful." : "Saving Failed.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlogs(int id, BlogModel blog)
    {
        string query = @"UPDATE [dbo].[Tbl_Blog]
                SET [BlogTitle] = @BlogTitle
                    ,[BlogAuthor] = @BlogAuthor
                    ,[BlogContent] = @BlogContent
                WHERE BlogId = @BlogId";

        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();
        if (result == 0)
        {
            return NotFound("No data found");
        }
        connection.Close();

        string message = result > 0 ? "Updating Successful." : "Updating Failed.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlogs(int id, BlogModel blog)
    {
        string conditions = string.Empty;

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            conditions += $"[BlogTitle] = '{blog.BlogTitle}',";
        }

        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            conditions += $"[BlogAuthor] = '{blog.BlogAuthor}',";
        }

        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            conditions += $"[BlogContent] = '{blog.BlogContent}',";
        }

        if (string.IsNullOrEmpty(conditions))
        {
            return NotFound("No data to update");
        }

        conditions = conditions.Substring(0, conditions.Length - 1);

        string query = $@"UPDATE [dbo].[Tbl_Blog]
                      SET {conditions}
                      WHERE BlogId = {id}";

        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();
        string message = result > 0 ? "Updating Patch Successful." : "Updating Patch Failed.";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlogs(int id)
    {
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();
        if (result == 0)
        {
            return NotFound("No data found");
        }
        connection.Close();

        string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
        return Ok(message);
    }
}
