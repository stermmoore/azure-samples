using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
using System.Text;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetSection("BlobConnectionString")?.Value;
var containerName = "test-blobs";


var blobServiceClient = new BlobServiceClient(connectionString);

//Create container if it doesnt exist
var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

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

await AppendToABlob(connectionString, containerName);

await OutputBlobContents(containerClient, "appendBlob");


static async Task OutputBlobContents(BlobContainerClient containerClient, string blobId)
{
    var blobClient = containerClient.GetBlobClient(blobId);

    var downloadResult = await blobClient.DownloadContentAsync();

    var blobContents = downloadResult.Value.Content.ToString();

    Console.WriteLine(blobContents);
}

static async Task AppendToABlob(string? connectionString, string containerName)
{
    var appendBlobClient = new AppendBlobClient(connectionString, containerName, "appendBlob");
    await appendBlobClient.CreateIfNotExistsAsync();

    for (var i = 0; i < 100; i++)
    {
        var content = $"Test {i} {Environment.NewLine}";

        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        {
            await appendBlobClient.AppendBlockAsync(ms);
        }
    }
}