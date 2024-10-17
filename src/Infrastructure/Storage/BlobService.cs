using Application.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Infrastructure.Storage;

public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
	private const string ContainerName = "algorithm-questions";
	public async Task DeleteAsync(Guid fileId)
	{
		BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
		BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());
		await blobClient.DeleteAsync();
	}

	public async Task<(Stream, string)> DownloadAsync(Guid fileId)
	{
		BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
		BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());
		var response = await blobClient.DownloadContentAsync();
		return (response.Value.Content.ToStream(), response.Value.Details.ContentType);
	}

	public async Task<Guid> UploadAsync(Stream stream, string contentType)
	{
		BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
		var fileId = Guid.NewGuid();
		BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());
		await blobClient.UploadAsync(
			stream,
			new BlobHttpHeaders { ContentType = contentType });
		return fileId;
	}
}
