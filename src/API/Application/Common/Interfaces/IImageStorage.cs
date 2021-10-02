namespace Commentor.GivEtPraj.Application.Common.Interfaces;

public interface IImageStorage
{
    Task<Stream?> FindImage(string name);
    Task<bool> UploadImage(string name, Stream content);
}