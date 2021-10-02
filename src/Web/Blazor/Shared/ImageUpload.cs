using Microsoft.AspNetCore.Components;
using Tewr.Blazor.FileReader;

namespace Commentor.GivEtPraj.Blazor.Shared;

public class ImageUpload
{
    private readonly IFileReaderService _fileReaderService;

    public ImageUpload(IFileReaderService fileReaderService)
    {
        _fileReaderService = fileReaderService;
    }

    public async Task<IList<string>> GetFilesUploaded(ElementReference input)
    {
        var content = new List<string>();

        foreach (var file in await _fileReaderService.CreateReference(input).EnumerateFilesAsync())
        {
            if (file == null) continue;

            var fileInfo = await file.ReadFileInfoAsync();
            var ms = await file.CreateMemoryStreamAsync(4 * 1024);
            content.Add(Convert.ToBase64String(ms.ToArray()));
        }

        return content;
    }
}