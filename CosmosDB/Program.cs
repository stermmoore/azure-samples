using CosmosDB;
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

var reading = new Reading { Location = "Garden" };

await AddItem("sample-db", "sample-container", reading);
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

async Task AddItem(string databaseName, string containerName, Reading item)
{
    var cosmosClient = new CosmosClient(connectionString);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    await container.CreateItemAsync<Reading>(item);
}