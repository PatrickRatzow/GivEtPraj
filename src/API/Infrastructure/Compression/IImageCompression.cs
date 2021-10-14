using System.IO;

namespace Commentor.GivEtPraj.Infrastructure.Compression;

public interface IImageCompression
{
    MemoryStream CompressImage(Stream content, int level = 30);
}