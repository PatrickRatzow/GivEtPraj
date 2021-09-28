using System.IO;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Application.Common.Interfaces
{
    public interface IFileStorage
    {
        Task<Stream?> FindFile(string path);
        Task<bool> UploadFile(string path, Stream content, string? contentType = null);
    }
}