using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=azurestorageblobs2023;AccountKey=bLx+vSoLg/bg2K9hRNp3MawHeoRu/k1WytnP0ceXUxeA6mTb7UtWjTKqnKJow1Iq+Vqyp01+rd/5+AStCqEkEA==;EndpointSuffix=core.windows.net";
BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mycontainer");
BlobClient blobClient = containerClient.GetBlobClient("myfile.txt");

using (FileStream fs = File.OpenRead("C:\\Users\\Tcp\\source\\repos\\AaronPort\\AzureStorageBlobs\\myfile.txt"))
{
	await blobClient.UploadAsync(fs, overwrite: true);
}

BlobDownloadInfo download = await blobClient.DownloadAsync();
using (FileStream fs = File.OpenWrite("downloaded_myfile.txt"))
{
	await download.Content.CopyToAsync(fs);
	fs.Position = 0;
}