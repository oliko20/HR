using System;
using HR.Api.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HR.Api.Db
{
    public class EmployeeDao
    {
        private readonly string _connectionString;

        public EmployeeDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<List<Employee>> ListAsync(string firstName, string lastName)
        {
            var list = new List<Employee>();
            await using var connection = new SqlConnection(_connectionString);

            var command = new SqlCommand(
                @$"SELECT [Id]
                     ,[FirstName]
                     ,[LastName]
                     ,[PersonalId]
                     ,[Gender]
                     ,[BirthDate]
                     ,[JobPosition]
                     ,[Status]
                     ,[FireDate]
                     ,[IsDeleted]
                 FROM [dbo].[Employees]", connection);

            var queryParams = new List<string> { "IsDeleted = 0" };
            if (!string.IsNullOrEmpty(firstName))
            {
                queryParams.Add($"FirstName LIKE N'{firstName}%'");
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                queryParams.Add($"LastName LIKE N'{lastName}%'");
            }

            var joinedQuery = string.Join(" AND ", queryParams);
            command.CommandText += $"WHERE {joinedQuery}";

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Employee
                {
                    Id = reader.GetInt32("Id"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    PersonalId = reader.GetString("PersonalId"),
                    Gender = reader.GetEnum<Gender>("Gender"),
                    BirthDate = reader.GetDateTime("BirthDate"),
                    JobPosition = reader.GetString("JobPosition"),
                    Status = reader.GetEnum<Status>("Status"),
                    FireDate = reader.GetDateTime("FireDate"),
                    IsDeleted = reader.GetBool("IsDeleted")
                });
            }
            return list;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                 @"SELECT [Id]
                     ,[FirstName]
                     ,[LastName]
                     ,[PersonalId]
                     ,[Gender]
                     ,[BirthDate]
                     ,[JobPosition]
                     ,[Status]
                     ,[FireDate]
                     ,[IsDeleted]
                 FROM [dbo].[Employees] WHERE @Id = Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return null;

            await reader.ReadAsync();
            return new Employee
            {
                Id = reader.GetInt32("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                PersonalId = reader.GetString("PersonalId"),
                Gender = reader.GetEnum<Gender>("Gender"),
                BirthDate = reader.GetDateTime("BirthDate"),
                JobPosition = reader.GetString("JobPosition"),
                Status = reader.GetEnum<Status>("Status"),
                FireDate = reader.GetDateTime("FireDate"),
                IsDeleted = reader.GetBool("IsDeleted")
            };
        }
        public async Task<Employee> GetByPersonalIdAsync(string id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                @"SELECT [Id]
                     ,[FirstName]
                     ,[LastName]
                     ,[PersonalId]
                     ,[Gender]
                     ,[BirthDate]
                     ,[JobPosition]
                     ,[Status]
                     ,[FireDate]
                     ,[IsDeleted]
                 FROM [dbo].[Employees] WHERE @Id = PersonalId AND IsDeleted = 0", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return null;

            await reader.ReadAsync();
            return new Employee
            {
                Id = reader.GetInt32("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                PersonalId = reader.GetString("PersonalId"),
                Gender = reader.GetEnum<Gender>("Gender"),
                BirthDate = reader.GetDateTime("BirthDate"),
                JobPosition = reader.GetString("JobPosition"),
                Status = reader.GetEnum<Status>("Status"),
                FireDate = reader.GetDateTime("FireDate"),
                IsDeleted = reader.GetBool("IsDeleted")
            };
        }

        public async Task<int> CreateAsync([Bind]Employee item)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                @"INSERT INTO [dbo].[Employees](
                       [FirstName]
                      ,[LastName]
                      ,[PersonalId]
                      ,[Gender]
                      ,[BirthDate]
                      ,[JobPosition]
                      ,[Status]
                      ,[FireDate]
                      ,[IsDeleted])                     
                     VALUES(
                      @FirstName,
                      @LastName,
                      @PersonalId,
                      @Gender,
                      @BirthDate,
                      @JobPosition,
                      @Status,
                      @FireDate,
                      @IsDeleted);
                    SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@FirstName", item.FirstName);
            command.Parameters.AddWithValue("@LastName", item.LastName);
            command.Parameters.AddWithValue("@PersonalId", item.PersonalId);
            command.Parameters.AddWithValue("@Gender", item.Gender);
            command.Parameters.AddWithValue("@BirthDate", item.BirthDate);
            command.Parameters.AddWithValue("@JobPosition", item.JobPosition);
            command.Parameters.AddWithValue("@Status", item.Status);
            command.Parameters.AddWithValue("@FireDate", item.FireDate.HasValue ? (object)item.FireDate.Value : DBNull.Value);
            command.Parameters.AddWithValue("@IsDeleted", item.IsDeleted);
            await connection.OpenAsync();
            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }
        public async Task UpdateAsync([Bind]Employee item)
        {
            await using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                @"UPDATE [dbo].[Employees] SET  
                    [FirstName] = @FirstName, 
                    [LastName] = @LastName, 
                    [PersonalId] = @PersonalId,
                    [Gender] = @Gender, 
                    [BirthDate] = @BirthDate,
                    [JobPosition] = @JobPosition,
                    [Status] = @Status,
                    [FireDate] = @FireDate,
                    [IsDeleted] = @IsDeleted
                WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", item.Id);
            command.Parameters.AddWithValue("@FirstName", item.FirstName);
            command.Parameters.AddWithValue("@LastName", item.LastName);
            command.Parameters.AddWithValue("@PersonalId", item.PersonalId);
            command.Parameters.AddWithValue("@Gender", item.Gender);
            command.Parameters.AddWithValue("@BirthDate", item.BirthDate);
            command.Parameters.AddWithValue("@JobPosition", item.JobPosition);
            command.Parameters.AddWithValue("@Status", item.Status);
            command.Parameters.AddWithValue("@FireDate", item.FireDate.HasValue ? (object)item.FireDate.Value : DBNull.Value);
            command.Parameters.AddWithValue("@IsDeleted", item.IsDeleted);
            await connection.OpenAsync();
            await command.ExecuteScalarAsync();
        }
    }
}
