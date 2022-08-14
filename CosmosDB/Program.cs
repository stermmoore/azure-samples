using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("CosmosConnectionString")?.Value;

await CreateDB("sample-db");

await CreateContainer("sample-db", "sample-container", "/location");


//Write Item

//Read Item

//Read Items

//Change Item

async Task CreateDB(string dbName)
{
    var cosmosClient = new CosmosClient(connectionString);
    await cosmosClient.CreateDatabaseIfNotExistsAsync(dbName);
}

async Task CreateContainer(string databaseName, string containerName, string partitionKey)
{
    var cosmosClient = new CosmosClient(connectionString);

    var database = cosmosClient.GetDatabase(databaseName);

    await database.CreateContainerIfNotExistsAsync(containerName, partitionKey);
}