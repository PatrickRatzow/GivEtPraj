using System;
using System.IO;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Infrastructure.Compression;

namespace Commentor.GivEtPraj.Infrastructure.Storage;

public class ImageStorage : IImageStorage
{
    private readonly IFileStorage _fileStorage;
    private readonly IImageCompression _imageCompression;

    public ImageStorage(IFileStorage fileStorage, IImageCompression imageCompression)
    {
        _fileStorage = fileStorage;
        _imageCompression = imageCompression;
    }

    public async Task<Stream?> FindImage(string name)
        => await _fileStorage.FindFile(FullPath(name));

    public async Task<bool> UploadImage(string name, Stream content)
    {
        var extension = Path.GetExtension(name);
        var contentType = extension switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => throw new ArgumentException($"{name} is not an allowed file extension")
        };

        var compressedImage = _imageCompression.CompressImage(content);

        return await _fileStorage.UploadFile(FullPath(name), compressedImage, contentType);
    }

    private static string FullPath(string name) => $"cases/{name}";
}