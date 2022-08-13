using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("CosmosConnectionString")?.Value;

var cosmosClient = new CosmosClient(connectionString);