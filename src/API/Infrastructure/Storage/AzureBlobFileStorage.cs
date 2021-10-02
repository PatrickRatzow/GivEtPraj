using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Commentor.GivEtPraj.Infrastructure.Storage;

public class AzureBlobFileStorage : IFileStorage
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobFileStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Azure:Blob:ConnectionString"]);
    }

    public async Task<Stream?> FindFile(string path)
    {
        var blobClient = GetBlobClient(path);

        var fileExists = await blobClient.ExistsAsync();
        if (!fileExists) return null;

        var file = await blobClient.DownloadAsync();
        return file.Value.Content;
    }

    public async Task<bool> UploadFile(string path, Stream content, string? contentType = null)
    {
        var blobClient = GetBlobClient(path);

        var fileExists = await blobClient.ExistsAsync();
        if (fileExists) return false;

        var headers = new BlobHttpHeaders();
        if (contentType is not null)
            headers.ContentType = contentType;

        await blobClient.UploadAsync(content, headers);

        return true;
    }

    private BlobClient GetBlobClient(string path)
    {
        var split = path.Split("/");
        var containerName = split.First();
        var fileName = string.Join("/", split.Skip(1));

        return _blobServiceClient.GetBlobContainerClient(containerName)
            .GetBlobClient(fileName);
    }
}