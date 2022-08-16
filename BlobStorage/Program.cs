using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("BlobConnectionString")?.Value;


var blobServiceClient = new BlobServiceClient(connectionString);

await blobServiceClient.CreateBlobContainerAsync("test-blob");