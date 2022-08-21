using CosmosDB;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("CosmosConnectionString")?.Value;

var dbName = "sample-db";
var containerName = "sample-container";

await CreateDB(dbName);

await CreateContainer(dbName, containerName, "/location");


//Write Item
var newReading = new Reading { Location = "Garden" };

await AddItem(dbName, containerName, newReading);

//Read Items
var readings = await GetAllReadings(dbName, containerName);

foreach(var reading in readings)
{
    Console.WriteLine($"Time {reading.ReadingTime} - Location {reading.Location}");
}

//Change Item
var existingReading = readings.First();

existingReading.IsVerified = true;

await Update(dbName, containerName, existingReading);



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

    var result = await container.CreateItemAsync<Reading>(item);
    Console.WriteLine($"Add Item Charge: {result.RequestCharge}");
}

async Task Update(string databaseName, string containerName, Reading item)
{
    var cosmosClient = new CosmosClient(connectionString);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    await container.UpsertItemAsync(item);
}

async Task<IEnumerable<Reading>> GetAllReadings(string databaseName, string containerName)
{
    var cosmosClient = new CosmosClient(connectionString);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var queryDefinition = new QueryDefinition("SELECT * FROM c");

    var feedIterator = container.GetItemQueryIterator<Reading>(queryDefinition);
    
    var result = new List<Reading>();

    while(feedIterator.HasMoreResults)
    {
        var response = await feedIterator.ReadNextAsync();
        Console.WriteLine($"Feed Iterator Read Charege: {response.RequestCharge}");

        foreach(var reading in response)
        {
            result.Add(reading);
        }
    }

    return result;
}