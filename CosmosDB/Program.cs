using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("CosmosConnectionString")?.Value;

await CreateDB("sample-db");

async Task CreateDB(string dbName)
{
    var cosmosClient = new CosmosClient(connectionString);
    await cosmosClient.CreateDatabaseIfNotExistsAsync(dbName);
}