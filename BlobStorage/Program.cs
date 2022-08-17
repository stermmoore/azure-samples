using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("BlobConnectionString")?.Value;


var blobServiceClient = new BlobServiceClient(connectionString);

var containerClient = blobServiceClient.GetBlobContainerClient("test-blobs");

await containerClient.CreateIfNotExistsAsync();

var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString());


var blobInfo = await blobClient.UploadAsync("MyTestBlobContents.txt", 
    new BlobHttpHeaders { ContentType = "text/plain" });
