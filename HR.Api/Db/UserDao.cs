using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HR.Api.Models;
using Microsoft.Extensions.Configuration;

namespace HR.Api.Db
{
    public class UserDao
    {
        private readonly string _connectionString;

        public UserDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<int> CreateAsync(User item)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                @"INSERT INTO [dbo].[Users](
                       [FirstName]
                      ,[LastName]
                      ,[PersonalId]
                      ,[Gender]
                      ,[BirthDate]
                      ,[Email]
                      ,[Password])                     
                     VALUES(
                      @FirstName,
                      @LastName,
                      @PersonalId,
                      @Gender,
                      @BirthDate,
                      @Email,
                      @Password);
                    SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@FirstName", item.FirstName);
            command.Parameters.AddWithValue("@LastName", item.LastName);
            command.Parameters.AddWithValue("@PersonalId", item.PersonalId);
            command.Parameters.AddWithValue("@Gender", item.Gender);
            command.Parameters.AddWithValue("@BirthDate", item.BirthDate);
            command.Parameters.AddWithValue("@Email", item.Email);
            command.Parameters.AddWithValue("@Password", item.Password);
            await connection.OpenAsync();
            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }
        public async Task<User> GetByPersonalIdAsync(string id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                @"SELECT [Id]
                 ,[FirstName]
                 ,[LastName]
                 ,[Email]
                 ,[Gender]
                 ,[PersonalId]
                 ,[BirthDate]
                 ,[Password]
             FROM [HR].[dbo].[Users] WHERE PersonalId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return null;

            await reader.ReadAsync();
            return new User
            {
                Id = reader.GetInt32("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                PersonalId = reader.GetString("PersonalId"),
                Gender = reader.GetEnum<Gender>("Gender"),
                BirthDate = reader.GetDateTime("BirthDate"),
                Email = reader.GetString("Email"),
                Password = reader.GetString("Password")
            };
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                @"SELECT [Id]
                 ,[FirstName]
                 ,[LastName]
                 ,[Email]
                 ,[Gender]
                 ,[PersonalId]
                 ,[BirthDate]
                 ,[Password]
             FROM [HR].[dbo].[Users] WHERE Email = @email", connection);
            command.Parameters.AddWithValue("@email", email);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return null;

            await reader.ReadAsync();
            return new User
            {
                Id = reader.GetInt32("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                PersonalId = reader.GetString("PersonalId"),
                Gender = reader.GetEnum<Gender>("Gender"),
                BirthDate = reader.GetDateTime("BirthDate"),
                Email = reader.GetString("Email"),
                Password = reader.GetString("Password")
            };
        }
    }
}

