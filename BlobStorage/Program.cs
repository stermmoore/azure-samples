using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("BlobConnectionString")?.Value;


var blobServiceClient = new BlobServiceClient(connectionString);

//Create container if it doesnt exist
var containerClient = blobServiceClient.GetBlobContainerClient("test-blobs");

await containerClient.CreateIfNotExistsAsync();

//Upload a blob to the container
var blobId = Guid.NewGuid().ToString();

var blobClient = containerClient.GetBlobClient(blobId);

var blobInfo = await blobClient.UploadAsync("MyTestBlobContents.txt",
    new BlobHttpHeaders { ContentType = "text/plain" });


//List all blobs in a container
var blobListPage = containerClient.GetBlobsAsync();

await foreach (var page in blobListPage)
{
    Console.WriteLine(page.Name);
}

await OutputBlobContents(containerClient, blobId);


static async Task OutputBlobContents(BlobContainerClient containerClient, string blobId)
{
    var blobClient = containerClient.GetBlobClient(blobId);

    var downloadResult = await blobClient.DownloadContentAsync();

    var blobContents = downloadResult.Value.Content.ToString();

    Console.WriteLine(blobContents);
}


