using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("TableConnectionString")?.Value;

//Create a table
var tableServiceClient = new TableServiceClient(connectionString);

var tableName = "myTestTable";

await tableServiceClient.CreateTableIfNotExistsAsync(tableName);

