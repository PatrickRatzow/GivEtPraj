using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tewr.Blazor.FileReader;

namespace Blazor.Shared
{
    public class ImageUpload
    {
        private readonly IFileReaderService _fileReaderService;

        public ImageUpload(IFileReaderService fileReaderService)
        {
            _fileReaderService = fileReaderService;
        }

        public async Task<MultipartFormDataContent> GetFilesUploaded(ElementReference input)
        {
            var content = new MultipartFormDataContent();

            foreach (var file in await _fileReaderService.CreateReference(input).EnumerateFilesAsync())
            {
                if (file == null) continue;
                
                var fileInfo = await file.ReadFileInfoAsync();
                var ms = await file.CreateMemoryStreamAsync(4 * 1024);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), "image", fileInfo.Name);
            }

            return content;
        }
    }
}