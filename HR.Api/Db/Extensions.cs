using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HR.Api.Db
{
    public static class Extensions
    {
        public static int GetInt32(this SqlDataReader sqlDataReader, string key) => Convert.ToInt32(sqlDataReader[key]);

        public static DateTime? GetDateTime(this SqlDataReader sqlDataReader, string key) => sqlDataReader[key] is DBNull ? (DateTime?)null : Convert.ToDateTime(sqlDataReader[key]);

        public static string GetString(this SqlDataReader sqlDataReader, string key) => sqlDataReader?[key] != null ? sqlDataReader[key].ToString() : null;

        public static bool GetBool(this SqlDataReader sqlDataReader, string key) => Convert.ToBoolean(sqlDataReader[key]);

        public static T GetEnum<T>(this SqlDataReader sqlDataReader, string key) where T : struct => Enum.Parse<T>(sqlDataReader.GetString(key));

        public static string GetConnectionString(this IConfiguration configuration) => configuration["ConnectionString"];
        
}
}
