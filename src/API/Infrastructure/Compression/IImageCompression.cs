using System.IO;

namespace Commentor.GivEtPraj.Infrastructure.Compression;

public interface IImageCompression
{
    MemoryStream CompressImage(string base64String, int level = 30);
}