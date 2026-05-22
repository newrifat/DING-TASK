using Microsoft.Extensions.Configuration;
using System;

namespace DingAssignment.Config
{
    public static class ConfigReader
    {
        private static readonly IConfiguration _configuration;

        static ConfigReader()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string BaseUrl => _configuration["AppSettings:BaseUrl"] ?? throw new NullReferenceException("BaseUrl missing");
        public static string Username => _configuration["AppSettings:Username"] ?? throw new NullReferenceException("Username missing");
        public static string Password => _configuration["AppSettings:Password"] ?? throw new NullReferenceException("Password missing");
        public static string LockedOutUsername => _configuration["AppSettings:LockedOutUsername"] ?? throw new NullReferenceException("LockedOutUsername missing");

        public static string FirstName => _configuration["TestData:FirstName"] ?? throw new NullReferenceException("FirstName missing");
        public static string LastName => _configuration["TestData:LastName"] ?? throw new NullReferenceException("LastName missing");
        public static string PostalCode => _configuration["TestData:PostalCode"] ?? throw new NullReferenceException("PostalCode missing");
        public static string ProductBackpack => _configuration["TestData:ProductBackpack"] ?? throw new NullReferenceException("ProductBackpack missing");
        public static string ProductBikeLight => _configuration["TestData:ProductBikeLight"] ?? throw new NullReferenceException("ProductBikeLight missing");
        public static string ProductBoltTShirt => _configuration["TestData:ProductBoltTShirt"] ?? throw new NullReferenceException("ProductBoltTShirt missing");
    }
}