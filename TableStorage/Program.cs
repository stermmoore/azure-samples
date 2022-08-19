using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using TableStorage;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("TableConnectionString")?.Value;

//Create a table
var tableServiceClient = new TableServiceClient(connectionString);

var tableName = "myTestTable";

await tableServiceClient.CreateTableIfNotExistsAsync(tableName);


//Add an entry to a table

var tableClient = tableServiceClient.GetTableClient(tableName);

await tableClient.AddEntityAsync<TableRecord>(new TableRecord { Name = "Dave" });