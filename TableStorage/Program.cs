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


//Query the table
var tableRecords = tableClient.QueryAsync<TableRecord>(r => r.PartitionKey == nameof(TableRecord));

await foreach(var record in tableRecords)
{
    Console.WriteLine($"{record.Timestamp} - {record.Name}");
}


//Query for records with different properties
var differentRecords = tableClient.QueryAsync<AnotherTableRecord>(r => r.PartitionKey == nameof(AnotherTableRecord));

await foreach (var record in differentRecords)
{
    Console.WriteLine($"{record.Timestamp} - {record.TypeName}");
}