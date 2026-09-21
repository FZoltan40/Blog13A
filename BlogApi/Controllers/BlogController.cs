using BlogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;


namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=blog13a;";

        [HttpGet]
        public List<Blogger> GetBloggers() 
        {
            List<Blogger> bloggers = new List<Blogger>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT * FROM blogger;";

            var cmd = new MySqlCommand(sql,connection);

            var data = cmd.ExecuteReader();

            while (data.Read())
            {
                var blogger = new Blogger
                {
                    Id = data.GetInt32("id"),
                    Name = data.GetString("name"),
                    Email = data.GetString("email"),
                    Age = data.GetInt32("age"),
                    Password=data.GetString("password"),
                    RegistrationTime = data.GetDateTime("registrationTime")
                };

                bloggers.Add(blogger);
            }

            connection.Close();

            return bloggers;
        }
    }
}
