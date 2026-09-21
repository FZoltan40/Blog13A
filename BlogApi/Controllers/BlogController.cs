using BlogApi.Models;
using BlogApi.Models.DTOs;
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

        [HttpPost]
        public object AddNewBlogger([FromBody]AddNewBloggerDto addNewBloggerDto)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"INSERT INTO `blogger`(`name`, `email`, `age`, `password`, `registrationTime`) VALUES (@name,@email,@age,@password,@registrationTime)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", addNewBloggerDto.Name);
            cmd.Parameters.AddWithValue("@email", addNewBloggerDto.Email);
            cmd.Parameters.AddWithValue("@age", addNewBloggerDto.Age);
            cmd.Parameters.AddWithValue("@password", addNewBloggerDto.Password);
            cmd.Parameters.AddWithValue("@registrationTime", DateTime.Now);

            cmd.ExecuteNonQuery();

            connection.Close();

            return new {message = "Sikeres felvétel.", result = addNewBloggerDto };
        }
    }
}
