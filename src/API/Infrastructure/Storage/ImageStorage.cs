using System;
using System.IO;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Common.Interfaces;

namespace Commentor.GivEtPraj.Infrastructure.Storage;

public class ImageStorage : IImageStorage
{
    private readonly IFileStorage _fileStorage;

    public ImageStorage(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<Stream?> FindImage(string name)
        => await _fileStorage.FindFile(FullPath(name));

    public async Task<bool> UploadImage(string name, Stream content)
    {
        var extension = Path.GetExtension(name);
        var contentType = extension switch
        {
            "jpg" => "image/jpeg;base64",
            "jpeg" => "image/jpeg;base64",
            "png" => "image/png;base64",
            "txt" => "image/jpeg;base64",
            _ => throw new ArgumentException($"{name} is not an allowed file extension")
        };
        
        return await _fileStorage.UploadFile(FullPath(name), content, contentType);
    }

    private static string FullPath(string name) => $"cases/{name}";
}