using Kawkaba.Core.Entity.Files;
using Microsoft.AspNetCore.Http;

namespace Kawkaba.BusinessLayer.Interfaces;

public interface IFileHandling
{
    public Task<string> UploadFile(IFormFile file, Paths paths, string oldFilePath = null);
    public Task<string> UpdateFile(IFormFile file, Paths paths, string imageId);
    public Task<string> DefaultProfile(Paths paths);
    public Task<string> GetFile(string imageId);
}